using TestProject1.Core;
using TestProject1.Pages;

namespace TestProject1.Tests;

public class ValidateDownloadFunction:BaseTest
{

    [TestCase("EPAM_Corporate_Overview_Q4_EOY.pdf")]
    [TestCase("EPAM_Corporate_Overview_Q3.pdf")]
    [TestCase("EPAM_Corporate_Overview_Q2.pdf")]
    [TestCase("EPAM_Corporate_Overview_Q1.pdf")]
    public void ValidateDownloadFunctionTest(string fileName)
    {
        var aboutPage = new AboutPage(driver);
        aboutPage.OpenAbout();
        aboutPage.DownloadCompanyOverviewFile();

        string downloadedFilePath = Path.Combine(BrowserFactory.downloadDirectory, fileName);
        Assert.IsTrue(File.Exists(downloadedFilePath), "File was not downloaded successfully.");
    } 

}