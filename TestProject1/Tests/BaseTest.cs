using log4net;
using log4net.Config;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject1.Pages;
using TestProject1.Core;

namespace TestProject1.Tests;

public class BaseTest
{
    public MyLogger logger;

    [SetUp]
    public void SetUpForAllTests()
    {
        XmlConfigurator.Configure(new FileInfo("Log.config"));
        logger = new MyLogger();
        BrowserFactory.InitBrowser("Chrome");
        BrowserFactory.Driver.Url = "https://www.epam.com";

    }

    [TearDown]
    public void TearDownForAllTests()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            logger.Log.Error("Test failed");
            ScreenshotMaker.TakeBrowserScreenshot(BrowserFactory.Driver);
        } else
        {
            logger.Log.Info("Test successful complete");
        }
        
    }

    [OneTimeTearDown]
    public void TearDownAfterAllTests()
    {
        BrowserFactory.CloseAllDrivers();
    }
}    