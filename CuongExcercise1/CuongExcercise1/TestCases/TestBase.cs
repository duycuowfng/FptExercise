using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;
using System;
using System.IO;

namespace CuongExcercise1.TestCases
{
    class TestBase
    {
        protected IWebDriver driver;
        protected void InitBrowser(string browserName)
        {
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
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);
        }

        protected void QuitBrowser()
        {
            driver.Quit();
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
                driver.FindElement(by);
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