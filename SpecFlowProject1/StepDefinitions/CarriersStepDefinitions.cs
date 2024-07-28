using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow.BindingSkeletons;
using TestProject1.Core;
using TestProject1.Pages;
using TestProject1.Steps;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class CarriersStepDefinitions ()
    {
        private CarriersPage? carriersPage;
        private IWebDriver driver = BrowserFactory.Driver;

        private CarriersPage OnCarriersPage()
        {
            carriersPage ??= new CarriersPage(driver);
            return carriersPage;
        }

        [Given("I search position for (.*) in (.*) with remote checkbox")]
        public void GivenTheKeyWordAndLocation(string keyWord, string country)
        {
            OnCarriersPage().OpenCareers();
            OnCarriersPage().EnterSearchKeyWord(keyWord);
            OnCarriersPage().EnterLocationValue(country);
            OnCarriersPage().SetRemoteParameter();
            OnCarriersPage().ClickCarriersFindButton();
        }


        [When("I open last search result")]
        public void WhenLastSearchResultOpened()
        {
            OnCarriersPage().OpenLastSearchResult();
        }

        [Then("(.*) mentioned on a page")]
        public void ThenKeyWordMentionedOnPage(string keyWord)
        {
            var resultPage = OnCarriersPage().GetResultPageName();
            Assert.That(resultPage.Text, Does.Contain(keyWord));
        }
    }
}
