using log4net;
using log4net.Config;
using NUnit.Framework.Interfaces;
using TestProject1.Core;

namespace TestProject1.Tests;

public class BaseTest
{
    public ILog logger;

    [SetUp]
    public void SetUpForAllTests()
    {
        XmlConfigurator.Configure(new FileInfo("Log.config"));
        logger =  LogManager.GetLogger(GetType());
        BrowserFactory.InitBrowser("Chrome");
        BrowserFactory.Driver.Url = "https://www.epam.com";

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