using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1;

public class ValidateCarouselArticle
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

    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [Test]
    public void ValidateGlobalSearchTest(int counter)
    {


        driver.Url = "https://www.epam.com";


        var careersLink = driver.FindElement(By.LinkText("Insights"));
        careersLink.Click();

        //I have to remove the banner first
        var acceptCookies = elementlWait.Until(driver => driver.FindElement(By.Id("onetrust-accept-btn-handler")));
        acceptCookies.Click();

        var CarouselButton = driver.FindElement(By.CssSelector(".slider__right-arrow.slider-navigation-arrow"));
        elementlWait.Until(driver => !acceptCookies.Displayed);
        for (int i = 0; i < counter; i++)
        {
            CarouselButton.Click();    
        }

        var ArticleNameInCarousel = driver.FindElement(By.CssSelector(".owl-item.active .text .museo-sans-light")).GetAttribute("innerText");
        var ReadMoreArrow = driver.FindElement(By.CssSelector(".owl-item.active .svg-link-arrow"));
        elementlWait.Until(driver => ReadMoreArrow.Displayed);
        Thread.Sleep(2000);
        ReadMoreArrow.Click();
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