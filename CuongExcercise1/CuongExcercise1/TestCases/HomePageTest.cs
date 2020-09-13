using NUnit.Framework;
using CuongExcercise1.PageObjects;
using System.Threading;
using System.Configuration;

namespace CuongExcercise1.TestCases
{
    class HomePageTest : TestBase
    {
        [Test]
        public void VerifyBookTitleInDetailPageDisplayCorrectly()
        {
            InitBrowser("Chrome");
            NavigateToURL(ConfigurationManager.AppSettings["URL"]);

            Page.Home.SearchBook("Selenium");
            Thread.Sleep(5000);
            Page.Home.ClickFirstResultBook();
            string JsonTitle = GetValueFromJsonFile("..Books[2].value");
            string UI_Title = Page.Detail.ProductTitle.Text;
            Assert.AreEqual(JsonTitle, UI_Title);

            QuitBrowser();
        }
    }
}