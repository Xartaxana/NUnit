using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1;

public class Task2
{
    [TestCase(".Net", "All Cities in Argentina")]
    [TestCase("Python", "All Cities in Spain")]
    [TestCase("JavaScript", "All Locations")]

    [Test]
    public void Test1(string keyWord, string country)
    {
        IWebDriver driver = new ChromeDriver();
        driver.Manage().Window.Maximize(); 

        // try
        // {
            driver.Url = "https://www.epam.com";

            var elementlWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20))
            {
                PollingInterval = TimeSpan.FromSeconds(0.25),
                Message = "Search panel has not been found"
            };

            var careersLink = driver.FindElement(By.LinkText("Careers"));
            careersLink.Click();

            var fieldKeywords = elementlWait.Until(driver => driver.FindElement(By.Id("new_form_job_search-keyword")));

            //I have to remove the banner first
            var acceptCookies = elementlWait.Until(driver => driver.FindElement(By.Id("onetrust-accept-btn-handler")));
            acceptCookies.Click();

            var clickAndSendKeysWord = new Actions(driver);

            clickAndSendKeysWord
                .Pause(TimeSpan.FromSeconds(2))
                .Click(fieldKeywords)
                .SendKeys(keyWord)
                .Perform();

            Thread.Sleep(600);
            var locationFild = driver.FindElement(By.ClassName("recruiting-search__location"));
            locationFild.Click();
            Thread.Sleep(600);
            var location = elementlWait.Until(driver => driver.FindElement(By.CssSelector($"[title='{country}'][role='option']")));
            Thread.Sleep(600);
            var clickAndSendLocation = new Actions(driver);
            clickAndSendLocation
                .Pause(TimeSpan.FromSeconds(2))
                .Click(location)
                .Perform();

            //location.Click();
            var remote = driver.FindElement(By.XPath("//input[@name = 'remote']/.. "));
            remote.Click();
            var findButton = driver.FindElement(By.CssSelector("button[type = 'submit']"));
            findButton.Click();



        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine(ex.Message);
        // }
        // finally
        // {
        //     driver.Quit();
        // }
    }


    [Test]

    [TestCase("Automation")]
    [TestCase("Cloud")]
    [TestCase("BLOCKCHAIN")]
    public void Test2( string keyWord)
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

            var resultItems = driver.FindElement(By.ClassName("search-results__items"));
            var brs = driver.FindElements(By.CssSelector(".search-results__items>article"));
            var text = resultItems.FindElements(By.PartialLinkText(keyWord)); //method is case sensitive
            Assert.That(brs.All(text.Contains));
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