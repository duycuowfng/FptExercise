using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;
using System;
using System.IO;

namespace CuongExcercise1.TestCases
{
    class TestBase
    {
        private static IWebDriver Driver { get; set; }

        public static IWebDriver GetDriver()
        {
            if (Driver == null)
                throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
            return Driver;
        }

        protected void InitBrowser(string browserName)
        {
            switch (browserName.ToUpper())
            {
                case "CHROME":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    Driver = new ChromeDriver();
                    break;
                case "FIREFOX":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    Driver = new FirefoxDriver();
                    break;
            }
            Driver.Manage().Window.Maximize();
        }

        protected void QuitBrowser()
        {
            Driver.Quit();
        }

        protected void NavigateToURL(string Url)
        {
            Driver.Navigate().GoToUrl(Url);
        }

        protected Func<IWebDriver, IWebElement> ValidateElementlIsClickable(By locator)
        {
            return driver =>
            {
                var element = driver.FindElement(locator);
                return (element != null && element.Displayed && element.Enabled) ? element : null;
            };
        }

        private bool ValidateElementIsPresent(By by)
        {
            try
            {
                Driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string GetValueFromJsonFile(string condition)
        {
            StreamReader r = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "../../TestDataAccess/TestData.json");
            string paragraph = r.ReadToEnd();
            JObject json2 = JObject.Parse(paragraph);
            string name = (string)json2.SelectToken(condition);
            Console.WriteLine(name);
            return name;
        }

    }
}