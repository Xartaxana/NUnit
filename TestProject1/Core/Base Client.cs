using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace TestProject1.Core
{
    internal class Base_Client
    {
        public class ApiClient
        {
            private readonly RestClient _client;

            public ApiClient(string baseUrl)
            {
                var options = new RestClientOptions(baseUrl)
                {
                    MaxTimeout = 1000
                };

                _client = new RestClient(options);

            }

            public RestRequest CreateRequest(string resource, Method method)
            {
                var request = new RestRequest(resource, method);
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Content-Type", "application/json");
                return request;
            }

            public RestResponse Execute(RestRequest request)
            {
                var response = _client.Execute(request);
                return response;
            }

            public void Dispose()
            {
                var response = _client.Dispose;

            }
        }
    }
}
