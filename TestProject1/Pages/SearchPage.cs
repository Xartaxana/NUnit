using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace TestProject1.Pages;

public class SearchPage(IWebDriver driver) : BasicPage(driver)
{
    private readonly By resultItemsLocator = By.CssSelector(".search-results__items>article");
    private readonly By resultListLocator = By.ClassName("search-results__items");
    

    public ReadOnlyCollection<IWebElement> GetResultItems() 
    {
        //To see all the results we need to scroll down the page
        driver.FindElement(By.TagName("body")).SendKeys(Keys.End); 
        Thread.Sleep(2000);
        return driver.FindElements(resultItemsLocator);
    }
    public ReadOnlyCollection<IWebElement> GetResultItemsWithText(string keyWord) 
    {
        //To see all the results we need to scroll down the page
        driver.FindElement(By.TagName("body")).SendKeys(Keys.End); 
        Thread.Sleep(2000);
        var resultList = driver.FindElement(resultListLocator);
        var itemsWithText = resultList.FindElements(By.PartialLinkText(keyWord));
        return itemsWithText;
    }
}