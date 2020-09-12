using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CuongExcercise1.TestCases
{
    class TestBase
    {
        String test_url = ConfigurationManager.AppSettings["URL"];
        protected IWebDriver driver;

        [SetUp]
        protected void SetUp()
        {
            driver = new ChromeDriver();
            driver.Url = test_url;
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        protected void TearDown()
        {
            driver.Quit();
        }
        protected void SetText(IWebElement e, string text)
        {
            e.SendKeys(text);
        }

        protected void ClickToElement(IWebElement e)
        {
            e.Click();
        }
        protected void GoToURL(String url)
        {
            driver.Navigate().GoToUrl(url);
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
        protected void WaitElementVisible(IWebElement e)
        {
            int retry = 0;
            do
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Element is displayed : {e.Displayed} : {retry}");
                retry++;
            } while (!e.Displayed && retry < 3);
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