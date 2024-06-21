using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1;

public class ValidateDownloadFunction
{
    public IWebDriver driver;
    public string downloadDirectory;
    WebDriverWait elementlWait;

    [SetUp]
    public void SetUp()
    {
        downloadDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads");
        Directory.CreateDirectory(downloadDirectory);
        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.default_directory", downloadDirectory);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("download.directory_upgrade", true);
        options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

        driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize(); 

        elementlWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30))
        {
            PollingInterval = TimeSpan.FromSeconds(0.25),
            Message = "Search panel has not been found"
        };
    }

    [TestCase("EPAM_Corporate_Overview_Q4_EOY.pdf")]
    [TestCase("EPAM_Corporate_Overview_Q3.pdf")]
    [TestCase("EPAM_Corporate_Overview_Q2.pdf")]
    [TestCase("EPAM_Corporate_Overview_Q1.pdf")]
    [Test]
    public void ValidateGlobalSearchTest(string fileName)
    {
        driver.Url = "https://www.epam.com";


        var careersLink = driver.FindElement(By.LinkText("About"));
        careersLink.Click();

        var GlanceSection = driver.FindElement(By.XPath("//section[contains(., 'EPAM at')]"));
        var DownloadButton = GlanceSection.FindElement(By.CssSelector(".button__inner"));


        new Actions(driver)
            .Pause(TimeSpan.FromSeconds(1))
            .ScrollToElement(GlanceSection)
            .Pause(TimeSpan.FromSeconds(1))
            .Click(DownloadButton)
            .Perform();

        while (Directory.GetFiles(downloadDirectory).Count(i => i.EndsWith(".crdownload")) > 0)
        {
            Thread.Sleep(2000);
        }

        string downloadedFilePath = Path.Combine(downloadDirectory, fileName);
        Assert.IsTrue(File.Exists(downloadedFilePath), "File was not downloaded successfully.");

    } 

    [TearDown]
    public void TearDown()
    {
        driver.Quit(); // quit the driver and clsoe the windows
        driver.Dispose(); // freeing resources
    }

}