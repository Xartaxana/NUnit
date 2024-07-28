using log4net;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Core;

namespace SpecFlowProject1.Hooks
{
    [Binding]
    internal class MyHooks
    {
        public static ILog logger;
        public static IWebDriver driver;

        [BeforeFeature]
        public static void SetUpBeforeAllTests()
        {
            logger = MyLogger.Logger;
            BrowserFactory.InitBrowser("Chrome");
            driver = BrowserFactory.Driver;
        }

        [BeforeScenario]
        public static void SetUpForAllTests()
        {
            driver.Url = "https://www.epam.com";
            logger.Info("Test " + TestContext.CurrentContext.Test.Name + " was started");
        }

        [AfterScenario]
        public static void TearDownForAllTests()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                logger.Error("Test failed");
                ScreenshotMaker.TakeBrowserScreenshot(BrowserFactory.Driver);
            }
            else
            {
                logger.Info("Test successful complete");
            }
        }

        [AfterFeature]
        public static void TearDownAfterAllTests()
        {
            BrowserFactory.CloseAllDrivers();
        }
    }
}
