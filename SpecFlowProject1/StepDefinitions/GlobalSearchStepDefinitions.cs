using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Core;
using TestProject1.Pages;
using TestProject1.Steps;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class GlobalSearchStepDefinitions
    {
        private SearchPage? searchPage;
        private IWebDriver driver = BrowserFactory.Driver;

        private SearchPage OnSearchPage()
        {
            searchPage ??= new SearchPage(driver);
            return searchPage;
        }

        [Given("I search in global search (.*)")]
        public void GivenSearchInGlobalSearch(string keyWord)
        {
            OnSearchPage().OpenSearchPanel();
            OnSearchPage().EnterSearchValue(keyWord);
            OnSearchPage().ClickFindButton();
        }

        [Then("All links in a list contain the word (.*) in the text")]
        public void ThenAllLinksContainKeyWordInTheText(string keyWord)
        {
            var resultItems = OnSearchPage().GetResultItems();
            var itemsWithText = OnSearchPage().GetResultItemsWithText(keyWord);
            Assert.That(resultItems.All(itemsWithText.Contains), $"Total search results: {resultItems.Count}, results containing keyword: {itemsWithText.Count}");
        }
    }
}
