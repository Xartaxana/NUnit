using log4net;
using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Core;
using static TestProject1.APIBusiness.Users;
using static TestProject1.Core.Base_Client;

namespace TestProject1.API_tests
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class APITests
    {
        private ApiClient _client;
        public ILog logger =  MyLogger.Logger; 

        [SetUp]
        public void Setup()
        {
            _client = new ApiClient("https://jsonplaceholder.typicode.com/");
            logger.Info("Test " + TestContext.CurrentContext.Test.Name + " was started");
        }

        [Test]
        [Category("API")]
        public void ValidateListOfUsersReceivedSuccessfully()
        {
            var request = _client.CreateRequest("users", Method.Get);
            var response = _client.Execute(request);
            logger.Info("Sending GET request to users API");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var users = JsonConvert.DeserializeObject<List<User>>(response.Content);
            Assert.IsNotNull(users);
            var propList = users[0].GetType().GetProperties().ToList();
            Assert.Multiple(() =>
            {
                users.ForEach(user => propList.ForEach(
                    property => Assert.NotNull(property.GetValue(user), $"{property.Name} is null")));
            });

        }

        [Test]
        [Category("API")]
        public void ValidateResponseHeaderForListOfUsers()
        {
            var request = _client.CreateRequest("users", Method.Get);
            var response = _client.Execute(request);
            logger.Info("Sending GET request to users API");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.ContentHeaders.Any(header => header.Name == "Content-Type" && header.Value.ToString() == "application/json; charset=utf-8"));
        }

        [Test]
        [Category("API")]
        public void ValidateContentOfResponseBodyForListOfUsers()
        {
            var request = _client.CreateRequest("users", Method.Get);
            var response = _client.Execute(request);
            logger.Info("Sending GET request to users API");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var users = JsonConvert.DeserializeObject<List<User>>(response.Content);
            Assert.IsNotNull(users);
            Assert.AreEqual(10, users.Count);
            Assert.IsTrue(users.Select(user => user.Id).Distinct().Count() == 10);
            Assert.IsTrue(users.All(user => !string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Username)));
            Assert.IsTrue(users.All(user => user.Company != null && !string.IsNullOrEmpty(user.Company.Name)));
        }

        [Test]
        [Category("API")]
        public void ValidateResourceNotFound()
        {
            var request = _client.CreateRequest("invalidendpoint", Method.Get);
            var response = _client.Execute(request);
            logger.Info("Sending GET request to invalid endpoint");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        [Category("API")]
        public void ValidateUserCanBeCreated()
        {
            var userBuilder = new UserBuilder()
                .WithName("Mariia Lobashova")
                .WithUsername("Mary");

            var user = userBuilder.Build();
            logger.Info("Building new user");

            var request = _client.CreateRequest("users", Method.Post);
            request.AddJsonBody(user);
            var response = _client.Execute(request);
            logger.Info("Sending POST request to users API");

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var createdUser = JsonConvert.DeserializeObject<User>(response.Content);
            Assert.IsNotNull(createdUser);
            Assert.IsTrue(createdUser.Id > 10);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                logger.Error("Test failed");
            }
            else
            {
                logger.Info("Test successful complete");
            }
            _client.Dispose();
        }
    }
}
