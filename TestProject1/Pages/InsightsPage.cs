using OpenQA.Selenium;
using TestProject1.Core;

namespace TestProject1.Pages;

public class InsightsPage:BasicPage
{
    private readonly By ArticleNameOnPageLocator = By.CssSelector("#main .museo-sans-light");
    

    public string GetArticleNameOnPage() 
    {
        return BrowserFactory.Driver.FindElement(ArticleNameOnPageLocator).GetAttribute("innerText");
    }
    public void SwipeFirstCarousel (int counter)
    {
        //I have to remove the banner first
        RemoveCookiesBanner();

        var CarouselButton = BrowserFactory.Driver.FindElement(By.CssSelector(".slider__right-arrow.slider-navigation-arrow"));
        for (int i = 0; i < counter; i++)
        {
            CarouselButton.Click();    
        }
    }

    public string OpenArticleFromFirstCarousel ()
    {

        var ArticleNameInCarousel = BrowserFactory.Driver.FindElement(By.CssSelector(".owl-item.active .text .museo-sans-light")).GetAttribute("innerText");
        var ReadMoreArrow = BrowserFactory.Driver.FindElement(By.CssSelector(".owl-item.active .svg-link-arrow"));
        elementlWait.Until(driver => ReadMoreArrow.Displayed);
        Thread.Sleep(2000);
        ReadMoreArrow.Click();
        return ArticleNameInCarousel;
    }
}