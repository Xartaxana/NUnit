using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1.Pages;

public class BasicPage
{
    public readonly IWebDriver driver;
    private static string Url { get; } = "https://www.epam.com";

    public BasicPage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentException(nameof(driver));

    public BasicPage OpenIndexPage()
    {
        driver.Url = Url;
        return this;
    }

    public BasicPage OpenCareers()
    {
        var careersLink = driver.FindElement(By.LinkText("Careers"));
        careersLink.Click();
        return this;
    }

    public BasicPage OpenInsights()
    {
        var careersLink = driver.FindElement(By.LinkText("Insights"));
        careersLink.Click();
        return this;
    }

    public BasicPage OpenAbout()
    {
        var careersLink = driver.FindElement(By.LinkText("About"));
        careersLink.Click();
        return this;
    }

    public IWebElement? Search(string keyWord)
    {
        var elementlWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30))
        {
            PollingInterval = TimeSpan.FromSeconds(0.25),
            Message = "Search panel has not been found"
        };
        
        var searchIcon = driver.FindElement(By.CssSelector("button.header-search__button"));

        searchIcon.Click();

        var searchPanel = elementlWait.Until(driver => driver.FindElement(By.ClassName("header-search__panel")));
        var searchInput = searchPanel.FindElement(By.Name("q"));

        var clickAndSendKeysActions = new Actions(driver);

        clickAndSendKeysActions.Click(searchInput)
            .Pause(TimeSpan.FromSeconds(1))
            .SendKeys(keyWord)
            .Perform();

        var findButton = searchPanel.FindElement(By.XPath("//*[@class = 'search-results__action-section']/button"));
        findButton.Click();
        var searchPage = elementlWait.Until(driver => driver.FindElement(By.ClassName("text")));
        Assert.That(searchPage.Text, Is.EqualTo("Search"));

        //To see all the results we need to scroll down the page
        driver.FindElement(By.TagName("body")).SendKeys(Keys.End); 
        //var lastResult = elementlWait.Until(driver => driver.FindElement(By.CssSelector("article:nth-child(20)")));

        var resultList = driver.FindElement(By.ClassName("search-results__items"));
        return resultList;
    }
    
}