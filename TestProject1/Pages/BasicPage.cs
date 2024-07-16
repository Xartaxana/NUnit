using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Core;

namespace TestProject1.Pages;

public class BasicPage
{
    private readonly By searchPanelLocator = By.ClassName("header-search__panel");
    public ILog logger;

    public BasicPage()
    {
     logger =  MyLogger.Logger;  
    }
    
    protected readonly WebDriverWait elementlWait = new(BrowserFactory.Driver, TimeSpan.FromSeconds(30))
        {
            PollingInterval = TimeSpan.FromSeconds(0.25),
            Message = "Element has not been found"
        };
    
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

    public void RemoveCookiesBanner()
    {
        var acceptCookies = elementlWait.Until(d => d.FindElement(By.Id("onetrust-accept-btn-handler")));
        acceptCookies.Click();
        elementlWait.Until(driver => !acceptCookies.Displayed);
    }

    public void OpenSearchPanel()
    {
        logger.Info("Opening search panel");
        var searchIcon = BrowserFactory.Driver.FindElement(By.CssSelector("button.header-search__button"));
        searchIcon.Click();
        var searchPanel = elementlWait.Until(d => d.FindElement(searchPanelLocator));
    }

    public void EnterSearchValue(string keyWord)
    {
        logger.Info("Entering search value");
        var searchPanel = BrowserFactory.Driver.FindElement(searchPanelLocator);
        var searchInput = searchPanel.FindElement(By.Name("q"));
        var clickAndSendKeysActions = new Actions(BrowserFactory.Driver);
        clickAndSendKeysActions.Click(searchInput)
            .Pause(TimeSpan.FromSeconds(3))
            .SendKeys(keyWord)
            .Pause(TimeSpan.FromSeconds(2))
            .Perform();

    }
    public void ClickFindButton()
    {
        logger.Info("Click find button");
        var searchPanel = BrowserFactory.Driver.FindElement(searchPanelLocator);
        var findButton = searchPanel.FindElement(By.XPath("//*[@class = 'search-results__action-section']/button"));
        findButton.Click();
        var searchPage = elementlWait.Until(d => d.FindElement(By.ClassName("text"))).Text.Equals("Search");

    }
    
}