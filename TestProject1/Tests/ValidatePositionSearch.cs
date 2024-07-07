using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Pages;

public class ValidatePositionSearch
{

    public IWebDriver driver;
    WebDriverWait elementlWait;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize(); 

        elementlWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30))
        {
            PollingInterval = TimeSpan.FromSeconds(0.25),
            Message = "Search panel has not been found"
        };
    }

    [TestCase(".NET", "Georgia")]
    [TestCase("Python", "Kazakhstan")]
    [TestCase("JavaScript", "All Locations")]

    [Test]
    public void ValidatePositionSearchTest(string keyWord, string country)
    {
        var carriersPage = new CarriersPage();
        //carriersPage.OpenIndexPage();
        carriersPage.OpenCareers();
        carriersPage.PositionSearch(keyWord, country);

        var searchResultItems = driver.FindElements(By.CssSelector(".search-result__item")).Count;
        if (searchResultItems == 20)
        {
            //To see all the results we need to scroll down the page
            driver.FindElement(By.TagName("body")).SendKeys(Keys.End);
            elementlWait.Until(driver => driver.FindElements(By.CssSelector(".search-result__item")).Count > 20); //A corner case is possible when there are only 20 results, then this waiting will fail. It might be better to just wait 10 seconds (without checking for quantity)
        }
        
        var lastResult = driver.FindElement(By.CssSelector(".search-result__item:last-child"));
        var viewButton = lastResult.FindElement(By.CssSelector(".search-result__item-controls a"));
        elementlWait.Until(driver => viewButton.Displayed);
        viewButton.Click();
        
        var resultPage = elementlWait.Until(driver => driver.FindElement(By.TagName("article")));
        Assert.That(resultPage.Text, Does.Contain(keyWord)); //Assert is case sensitive

    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit(); // quit the driver and clsoe the windows
        driver.Dispose(); // freeing resources
    }
}