using log4net;
using NUnit.Framework.Interfaces;
using TestProject1.Core;

namespace TestProject1.Tests;

public class BaseTest
{
    public ILog logger;

    [OneTimeSetUp]
    public void SetUpBeforeAllTests()
    {

        logger =  MyLogger.Logger; 
        BrowserFactory.InitBrowser("Chrome");

    }

    [SetUp]
    public void SetUpForAllTests()
    {
        BrowserFactory.Driver.Url = "https://www.epam.com";
        logger.Info("Test " + TestContext.CurrentContext.Test.Name + " was started");
    }

    [TearDown]
    public void TearDownForAllTests()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            logger.Error("Test failed");
            ScreenshotMaker.TakeBrowserScreenshot(BrowserFactory.Driver);
        } else
        {
            logger.Info("Test successful complete");
        }
        
    }

    [OneTimeTearDown]
    public void TearDownAfterAllTests()
    {
        BrowserFactory.CloseAllDrivers();
    }
}    