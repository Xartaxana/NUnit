using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Core;

namespace TestProject1.Pages;

public class AboutPage:BasicPage
{

public void DownloadCompanyOverviewFile()
    {
        var GlanceSection = BrowserFactory.Driver.FindElement(By.XPath("//section[contains(., 'EPAM at')]"));
        var DownloadButton = GlanceSection.FindElement(By.CssSelector(".button__inner"));


        new Actions(BrowserFactory.Driver)
            .Pause(TimeSpan.FromSeconds(1))
            .ScrollToElement(GlanceSection)
            .Pause(TimeSpan.FromSeconds(1))
            .Click(DownloadButton)
            .Perform();
    }
}