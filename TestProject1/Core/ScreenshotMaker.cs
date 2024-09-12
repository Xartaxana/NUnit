using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Pages;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using OpenQA.Selenium.Support.Extensions;
using TestProject1.Core;

namespace TestProject1.Core;
     
public class ScreenshotMaker
{
    private static string NewScreenshotName
    {
        get { return "_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff")+ "." + "jpeg"; }
    }

    public static string TakeBrowserScreenshot(IWebDriver driver)
    {
        var screenshotPath = Path.Combine(Environment.CurrentDirectory, "Display" + NewScreenshotName);
        // var size = BrowserFactory.Driver.FindElement(By.Id("main")).Size;
        // driver.Manage().Window.Size = new Size(size.Width, size.Height);
        //((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(screenshotPath);
        var image = driver.TakeScreenshot();
        image.SaveAsFile(screenshotPath);
        TestContext.AddTestAttachment(screenshotPath);
        return screenshotPath;
    }
} 