using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1.Pages;

public class InsightsPage:BasicPage
{
    public InsightsPage (IWebDriver driver) : base(driver) {}

public string SwipeCarouselAndOpenArticle (int counter)
    {
        var elementlWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30))
        {
            PollingInterval = TimeSpan.FromSeconds(0.25),
            Message = "Search panel has not been found"
        };

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
        return ArticleNameInCarousel;
    }
}