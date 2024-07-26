using OpenQA.Selenium;

namespace TestProject1.Pages;

public class InsightsPage(IWebDriver driver) : BasicPage(driver)
{
    private readonly By ArticleNameOnPageLocator = By.CssSelector("#main .museo-sans-light");
    

    public string GetArticleNameOnPage() 
    {
        return driver.FindElement(ArticleNameOnPageLocator).GetAttribute("innerText");
    }
    public void SwipeFirstCarousel (int counter)
    {
        //I have to remove the banner first
        RemoveCookiesBanner();

        var CarouselButton = driver.FindElement(By.CssSelector(".slider__right-arrow.slider-navigation-arrow"));
        for (int i = 0; i < counter; i++)
        {
            CarouselButton.Click();    
        }
    }

    public string OpenArticleFromFirstCarousel ()
    {

        var ArticleNameInCarousel = driver.FindElement(By.CssSelector(".owl-item.active .text .museo-sans-light")).GetAttribute("innerText");
        var ReadMoreArrow = driver.FindElement(By.CssSelector(".owl-item.active .svg-link-arrow"));
        elementlWait.Until(driver => ReadMoreArrow.Displayed);
        Thread.Sleep(2000);
        ReadMoreArrow.Click();
        return ArticleNameInCarousel;
    }
}