using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Core;

namespace TestProject1.Pages;

public class BasicPage
{
    public CarriersPage OpenCareers()
    {
        var careersLink = BrowserFactory.Driver.FindElement(By.LinkText("Careers"));
        careersLink.Click();
        return  new CarriersPage();
    }

    public InsightsPage OpenInsights()
    {
        var insightsLinc = BrowserFactory.Driver.FindElement(By.LinkText("Insights"));
        insightsLinc.Click();
        return  new InsightsPage();
    }

    public AboutPage OpenAbout()
    {
        var aboutLink = BrowserFactory.Driver.FindElement(By.LinkText("About"));
        aboutLink.Click();
        return  new AboutPage();
    }

    public void Search(string keyWord)
    {
        var elementlWait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(30))
        {
            PollingInterval = TimeSpan.FromSeconds(0.25),
            Message = "Search panel has not been found"
        };
        
        var searchIcon = BrowserFactory.Driver.FindElement(By.CssSelector("button.header-search__button"));

        searchIcon.Click();

        var searchPanel = elementlWait.Until(d => d.FindElement(By.ClassName("header-search__panel")));
        var searchInput = searchPanel.FindElement(By.Name("q"));

        var clickAndSendKeysActions = new Actions(BrowserFactory.Driver);

        clickAndSendKeysActions.Click(searchInput)
            .Pause(TimeSpan.FromSeconds(3))
            .SendKeys(keyWord)
            .Pause(TimeSpan.FromSeconds(2))
            .Perform();

        var findButton = searchPanel.FindElement(By.XPath("//*[@class = 'search-results__action-section']/button"));
        findButton.Click();
        var searchPage = elementlWait.Until(d => d.FindElement(By.ClassName("text")));
        Assert.That(searchPage.Text, Is.EqualTo("Search"));

        //To see all the results we need to scroll down the page
        BrowserFactory.Driver.FindElement(By.TagName("body")).SendKeys(Keys.End); 

    }
    
}