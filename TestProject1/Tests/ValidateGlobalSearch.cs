using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Pages;

namespace TestProject1.Tests;

public class ValidateGlobalSearch
{
    public IWebDriver driver;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
    }

    [Test]
    [TestCase("Automation")]
    [TestCase("Cloud")]
    [TestCase("BLOCKCHAIN")]
    public void ValidateGlobalSearchTest( string keyWord)
    {
        var basicPage = new BasicPage(driver);
        basicPage.OpenIndexPage();
        var resultList = basicPage.Search(keyWord);
        if (resultList == null)
        {
            throw new ArgumentNullException("resultList", "No results were found");
        }
        Thread.Sleep(2000);
        var resultItems = driver.FindElements(By.CssSelector(".search-results__items>article"));
        var itemsWithText = resultList.FindElements(By.PartialLinkText(keyWord)); //method is case sensitive
        Assert.That(resultItems.All(itemsWithText.Contains));
    } 

    [TearDown]
    public void TearDown()
    {
        //driver.Close(); // close the windows
        driver.Quit(); // quit the driver and clsoe the windows
        driver.Dispose(); // freeing resources
    }

}