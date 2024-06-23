using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject1.Pages;

namespace TestProject1.Tests;

public class ValidateCarouselArticle
{
    public IWebDriver driver;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize(); 
    }

    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [Test]
    public void ValidateGlobalSearchTest(int counter)
    {


        var insightsPage = new InsightsPage(driver);
        insightsPage.OpenIndexPage();
        insightsPage.OpenInsights();
        var ArticleNameInCarousel = insightsPage.SwipeCarouselAndOpenArticle(counter);

        var ArticleNameOnPage = driver.FindElement(By.CssSelector("#main .museo-sans-light")).GetAttribute("innerText");
        StringAssert.Contains(ArticleNameInCarousel.Remove(ArticleNameInCarousel.Length - 1), ArticleNameOnPage);

    } 

    [TearDown]
    public void TearDown()
    {
        driver.Quit(); // quit the driver and clsoe the windows
        driver.Dispose(); // freeing resources
    }

}