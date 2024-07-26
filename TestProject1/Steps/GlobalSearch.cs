using System.Collections.ObjectModel;
using OpenQA.Selenium;
using TestProject1.Pages;

namespace TestProject1.Steps;

public class GlobalSearch(IWebDriver driver)
{
    private SearchPage? searchPage;
    private IWebDriver driver = driver;

    private SearchPage OnSearchPage() {
        searchPage ??= new SearchPage(driver);
        return searchPage;
    }

    public void Search(string keyWord)
    {
        OnSearchPage().OpenSearchPanel();
        OnSearchPage().EnterSearchValue(keyWord);
        OnSearchPage().ClickFindButton();
    }

    public ReadOnlyCollection<IWebElement> GetSearchResults()
    {
        return OnSearchPage().GetResultItems();
    }

    public ReadOnlyCollection<IWebElement> GetSearchResultsWithKeyWord(string keyWord)
    {
        return OnSearchPage().GetResultItemsWithText(keyWord);
    }

}