using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Core;

namespace TestProject1.Pages;

public class SearchPage:BasicPage
{ 
    private readonly By resultItemsLocator = By.CssSelector(".search-results__items>article");
    private readonly By resultListLocator = By.ClassName("search-results__items");
    

    public ReadOnlyCollection<IWebElement> GetResultItems() 
    {
        return BrowserFactory.Driver.FindElements(resultItemsLocator);
    }
    public ReadOnlyCollection<IWebElement> GetResultItemsWithText(string keyWord) 
    {
        var resultList = BrowserFactory.Driver.FindElement(resultListLocator);
        var itemsWithText = resultList.FindElements(By.PartialLinkText(keyWord));
        return itemsWithText;
    }
}