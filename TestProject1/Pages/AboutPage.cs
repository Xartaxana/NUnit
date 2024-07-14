using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestProject1.Core;

namespace TestProject1.Pages;

public class AboutPage:BasicPage
{

    public void DownloadCompanyOverviewFile()
    {
        logger.Info("Downloading company overview file");
        var GlanceSection = BrowserFactory.Driver.FindElement(By.XPath("//section[contains(., 'EPAM at')]"));
        var DownloadButton = GlanceSection.FindElement(By.CssSelector(".button__inner"));


        new Actions(BrowserFactory.Driver)
            .Pause(TimeSpan.FromSeconds(1))
            .ScrollToElement(GlanceSection)
            .Pause(TimeSpan.FromSeconds(1))
            .Click(DownloadButton)
            .Perform();
        
        while (Directory.GetFiles(BrowserFactory.downloadDirectory).Any(i => i.EndsWith(".crdownload")))
        {
            Thread.Sleep(2000);
        }

    }
}