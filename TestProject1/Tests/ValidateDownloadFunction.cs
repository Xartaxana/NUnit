using TestProject1.Core;
using TestProject1.Pages;

namespace TestProject1.Tests;

public class ValidateDownloadFunction:BaseTest
{

    [TestCase("EPAM_Corporate_Overview_Q4_EOY.pdf")]
    [TestCase("EPAM_Corporate_Overview_Q3.pdf")]
    [TestCase("EPAM_Corporate_Overview_Q2.pdf")]
    [TestCase("EPAM_Corporate_Overview_Q1.pdf")]
    [Test]
    public void ValidateGlobalSearchTest(string fileName)
    {
        var aboutPage = new AboutPage();
        Thread.Sleep(2000);//Without this wait, 2 log files are created
        logger.Info("Test " + TestContext.CurrentContext.Test.Name + " was started");
        aboutPage.OpenAbout();
        aboutPage.DownloadCompanyOverviewFile();

        string downloadedFilePath = Path.Combine(BrowserFactory.downloadDirectory, fileName);
        Assert.IsTrue(File.Exists(downloadedFilePath), "File was not downloaded successfully.");

    } 

}