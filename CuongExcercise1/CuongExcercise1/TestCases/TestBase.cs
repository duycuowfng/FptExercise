using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using WebDriverManager.DriverConfigs.Impl;

namespace CuongExcercise1.TestCases
{
    [TestFixture]
    class TestBase
    {
        private static IWebDriver Driver { get; set; }

        public static ExtentTest test;
        public static ExtentReports extent;

        public static IWebDriver GetDriver()
        {
            if (Driver == null)
                throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
            return Driver;
        }

        protected void InitBrowser(string browserName)
        {
            test = null;
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

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

        [OneTimeSetUp]
        public void ExtentStart()
        {
            extent = new ExtentReports();
            var htmlreporter = new ExtentHtmlReporter(System.AppDomain.CurrentDomain.BaseDirectory + "../../TestReport/Report" + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".html");
            extent.AttachReporter(htmlreporter);
        }

        [OneTimeTearDown]
        public void ExtentClose()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    DateTime time = DateTime.Now;
                    string fileName = "Screenshot_" + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".png";
                    string screenShotPath = Capture(TestBase.GetDriver(), fileName);
                    Console.WriteLine($"Screenshot has been captured at {screenShotPath}");
                    test.Log(Status.Fail, "Fail");
                    test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(System.AppDomain.CurrentDomain.BaseDirectory + "../../TestReport" + fileName));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            extent.Flush();
        }

        public void QuitBrowser()
        {
            Driver.Quit();
        }

        private string Capture(IWebDriver driver, String fileName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var path = System.AppDomain.CurrentDomain.BaseDirectory + "../../TestReport/" + fileName;
            screenshot.SaveAsFile(System.AppDomain.CurrentDomain.BaseDirectory + "../../TestReport/", ScreenshotImageFormat.Png);
            Console.WriteLine(path);
            return path;
        }

        protected void NavigateToURL(string Url)
        {
            Driver.Navigate().GoToUrl(Url);
        }

        protected void ValidateElementlIsClickable(IWebElement e)
        {
            WebDriverWait wait = new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(e));
        }

        protected string GetValueFromJsonFile(string condition)
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