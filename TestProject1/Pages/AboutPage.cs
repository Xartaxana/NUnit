using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1.Pages;

public class AboutPage:BasicPage
{
    public AboutPage (IWebDriver driver) : base(driver) {}

public void DownloadCompanyOverviewFile()
    {
        var GlanceSection = driver.FindElement(By.XPath("//section[contains(., 'EPAM at')]"));
        var DownloadButton = GlanceSection.FindElement(By.CssSelector(".button__inner"));


        new Actions(driver)
            .Pause(TimeSpan.FromSeconds(1))
            .ScrollToElement(GlanceSection)
            .Pause(TimeSpan.FromSeconds(1))
            .Click(DownloadButton)
            .Perform();
    }
}