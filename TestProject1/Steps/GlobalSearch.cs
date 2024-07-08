using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Core;
using TestProject1.Pages;

namespace TestProject1.Steps;

public class GlobalSearch
{

    readonly SearchPage searchPage = new();
    public void Search(string keyWord)
    {
        searchPage.OpenSearchPanel();
        searchPage.EnterSearchValue(keyWord);
        searchPage.ClickFindButton();
    }

    public ReadOnlyCollection<IWebElement> GetSearchResults()
    {
        return searchPage.GetResultItems();
    }

    public ReadOnlyCollection<IWebElement> GetSearchResultsWithKeyWord(string keyWord)
    {
        return searchPage.GetResultItemsWithText(keyWord);
    }

}