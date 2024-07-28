using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Core;
using TestProject1.Pages;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class AboutStepDefinitions
    {
        private IWebDriver driver = BrowserFactory.Driver;

        [Given("I download company overview file")]
        public void GivenDownloadCompanyOverviewFile ()
        {
            AboutPage aboutPage = new AboutPage(driver);
            aboutPage.OpenAbout();
            aboutPage.DownloadCompanyOverviewFile();
        }

        [Then("File with name '(.*)' downloaded")]
        public void ThenFileWasDownloaded(string fileName)
        {
            string downloadedFilePath = Path.Combine(BrowserFactory.downloadDirectory, fileName);
            Assert.IsTrue(File.Exists(downloadedFilePath), "File was not downloaded successfully.");
        }
    }
}
