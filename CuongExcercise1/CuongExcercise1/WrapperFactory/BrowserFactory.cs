using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;

namespace CuongExcercise1.WrapperFactory
{
    class BrowserFactory
    {
        public static void InitBrowser(string browser)
        {
			string browserName = browser;
			IWebDriver driver = null;

			switch (browserName.ToUpper())
			{
				case "CHROME":
					new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
					driver = new ChromeDriver();
					break;
				case "FIREFOX":
					new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
					driver = new FirefoxDriver();
					break;
			}

			driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);
		}
    }
}
