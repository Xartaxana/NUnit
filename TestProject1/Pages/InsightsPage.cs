using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Core;

namespace TestProject1.Pages;

public class InsightsPage:BasicPage
{

public string SwipeCarouselAndOpenArticle (int counter)
    {
        //I have to remove the banner first
        var acceptCookies = elementlWait.Until(driver => driver.FindElement(By.Id("onetrust-accept-btn-handler")));
        acceptCookies.Click();

        var CarouselButton = BrowserFactory.Driver.FindElement(By.CssSelector(".slider__right-arrow.slider-navigation-arrow"));
        elementlWait.Until(driver => !acceptCookies.Displayed);
        for (int i = 0; i < counter; i++)
        {
            CarouselButton.Click();    
        }

        var ArticleNameInCarousel = BrowserFactory.Driver.FindElement(By.CssSelector(".owl-item.active .text .museo-sans-light")).GetAttribute("innerText");
        var ReadMoreArrow = BrowserFactory.Driver.FindElement(By.CssSelector(".owl-item.active .svg-link-arrow"));
        elementlWait.Until(driver => ReadMoreArrow.Displayed);
        Thread.Sleep(2000);
        ReadMoreArrow.Click();
        return ArticleNameInCarousel;
    }
}