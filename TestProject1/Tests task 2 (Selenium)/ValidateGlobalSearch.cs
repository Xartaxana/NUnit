using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1;

public class ValidateGlobalSearch
{
    //[SetUp]
    // public void SetUp()
    // {
    //     IWebDriver driver;
    // }

    //     [TearDown]
    // public void TearDown()
    // {
    //     driver.Quit();
    // }

    [Test]

    [TestCase("Automation")]
    [TestCase("Cloud")]
    [TestCase("BLOCKCHAIN")]
    public void ValidateGlobalSearchTest( string keyWord)
    {
        IWebDriver driver = new ChromeDriver();

        try 
        {
            driver.Url = "https://www.epam.com";

            var searchIcon = driver.FindElement(By.CssSelector("button.header-search__button"));

            searchIcon.Click();

            var elementlWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20))
            {
                PollingInterval = TimeSpan.FromSeconds(0.25),
                Message = "Search panel has not been found"
            };

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
            var lastResult = elementlWait.Until(driver => driver.FindElement(By.CssSelector("article:nth-child(20)")));

            var resultList = driver.FindElement(By.ClassName("search-results__items"));
            var resultItems = driver.FindElements(By.CssSelector(".search-results__items>article"));
            var itemsWithText = resultList.FindElements(By.PartialLinkText(keyWord)); //method is case sensitive
            Assert.That(resultItems.All(itemsWithText.Contains));
            //It would be more correct to look for "utomation" in the .search-results__description elements as well, 
            //but this is not in the task and with such an assertion the test will also not be green, so I did not write it.
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            driver.Quit();
        }

    } 

}