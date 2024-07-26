using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TestProject1.Pages;

public class CarriersPage(IWebDriver driver) : BasicPage(driver)
{
    private readonly By resultPageNameLocator = By.TagName("article");
    

    public IWebElement GetResultPageName() 
    {
        return driver.FindElement(resultPageNameLocator);
    }
    
    public void EnterSearchKeyWord(string keyWord)
    {
        logger.Info("Entering search value");
        var fieldKeywords = elementlWait.Until(d => d.FindElement(By.Id("new_form_job_search-keyword")));
        //I have to remove the banner first
        RemoveCookiesBanner();
        var clickAndSendKeysWord = new Actions(driver);
        clickAndSendKeysWord
            .Pause(TimeSpan.FromSeconds(2))
            .Click(fieldKeywords)
            .SendKeys(keyWord)
            .Perform();
    }

    public void EnterLocationValue(string country)
    {
        logger.Info("Entering location value");
        var locationFild = driver.FindElement(By.ClassName("recruiting-search__location"));
        locationFild.Click();

        if (country == "All Locations")
        {
            var location = elementlWait.Until(driver => driver.FindElement(By.CssSelector($"[title='{country}'][role='option']")));
            elementlWait.Until(driver => location.Displayed); 
            location.Click();
        }
        else
        {
            var countryInput = driver.FindElement(By.CssSelector($"[aria-label = '{country}']"));
            elementlWait.Until(driver => countryInput.Displayed); //If the country's drop down has already been opened, it will close (as for Spain), this case must be handled separately
            countryInput.Click();
            var cityInput = driver.FindElement(By.CssSelector($"[title= 'All Cities in {country}'][role='option']"));
            elementlWait.Until(driver => cityInput.Displayed);
            cityInput.Click();
        }
    }
    
    public void SetRemoteParameter()
    {
        logger.Info("Setting  Remote check-box");
        var remote = driver.FindElement(By.XPath("//input[@name = 'remote']/.. "));
        remote.Click();
    }
    
    public void ClickCarriersFindButton()
    {
        logger.Info("Click find button");
        var findButton = driver.FindElement(By.CssSelector("button[type = 'submit']"));
        findButton.Click();
        var result = driver.FindElement(By.CssSelector(".search-result__list"));
        elementlWait.Until(driver => result.Displayed);
    }
    public void OpenLastSearchResult()
    {
        logger.Info("Opening last search result");
        var searchResultItems = driver.FindElements(By.CssSelector(".search-result__item")).Count;
        if (searchResultItems == 20)
        {
            //To see all the results we need to scroll down the page
            driver.FindElement(By.TagName("body")).SendKeys(Keys.End);
            elementlWait.Until(d => d.FindElements(By.CssSelector(".search-result__item")).Count > 20); //A corner case is possible when there are only 20 results, then this waiting will fail. It might be better to just wait 10 seconds (without checking for quantity)
        }
        
        var lastResult = driver.FindElement(By.CssSelector(".search-result__item:last-child"));
        var viewButton = lastResult.FindElement(By.CssSelector(".search-result__item-controls a"));
        elementlWait.Until(driver => viewButton.Displayed);
        viewButton.Click();
        Thread.Sleep(2000);
    }

}