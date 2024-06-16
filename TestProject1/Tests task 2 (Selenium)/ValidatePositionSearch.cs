using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1;
public class ValidatePositionSearch
{
    [TestCase(".NET", "Georgia")]
    [TestCase("Python", "Kazakhstan")]
    [TestCase("JavaScript", "All Locations")]

    [Test]
    public void ValidatePositionSearchTest(string keyWord, string country)
    {
        IWebDriver driver = new ChromeDriver();
        driver.Manage().Window.Maximize(); 
        try
        {

            driver.Url = "https://www.epam.com";

            var elementlWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30))
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

            var locationFild = driver.FindElement(By.ClassName("recruiting-search__location"));
            locationFild.Click();

            if (country == "All Locations")
            {
                var location = elementlWait.Until(driver => driver.FindElement(By.CssSelector($"[title='{country}'][role='option']")));
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

            var remote = driver.FindElement(By.XPath("//input[@name = 'remote']/.. "));
            remote.Click();
            var findButton = driver.FindElement(By.CssSelector("button[type = 'submit']"));
            findButton.Click();

            elementlWait.Until(driver => driver.FindElement(By.CssSelector(".search-result__item")).Displayed); //If the search does not produce results, then the test will fail here, it must be handled separately
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