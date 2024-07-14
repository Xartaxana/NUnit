using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace TestProject1.Core
{
    class BrowserFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver? driver;
        public static string downloadDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads");

        public static IWebDriver Driver
        {
            get
            {
                if(driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        public static void InitBrowser(string browserName)
        {          
            switch (browserName)
            {
                case "Firefox":
                    if (driver == null)
                    {
                        driver = new FirefoxDriver();
                        Drivers.Add("Firefox", Driver);
                    }
                    break;

                case "IE":
                    if (driver == null)
                    {
                        driver = new InternetExplorerDriver();
                        Drivers.Add("IE", Driver);
                    }
                    break;

                case "Chrome":
                    if (driver == null)
                    {
						Directory.CreateDirectory(downloadDirectory);
						var options = new ChromeOptions();
						options.AddUserProfilePreference("download.default_directory", downloadDirectory);
						options.AddUserProfilePreference("download.prompt_for_download", false);
						options.AddUserProfilePreference("download.directory_upgrade", true);
						options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
						//options.AddArgument("--headless=new");
						options.AddArgument("--start-maximized");
						driver = new ChromeDriver(options);
                        Drivers.Add("Chrome", Driver);
                    }
                    break;
            }           
        }

        public static void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Quit();
				Drivers[key].Dispose();
            }
        }
    }
}