using CuongExcercise1.PageObjects;
using NUnit.Framework;
using System.Configuration;

namespace CuongExcercise1.TestCases
{
    class HomePageTest : TestBase
    {
        [Test]
        public void VerifyBookTitleInDetailPageDisplayCorrectly()
        {
            InitBrowser(ConfigurationManager.AppSettings["BROWSER"]);
            NavigateToURL(ConfigurationManager.AppSettings["URL"]);

            Page.Home.SearchBook("Selenium");
            ValidateElementlIsClickable(Page.Home.FirstResultBook);
            Page.Home.ClickFirstResultBook();
            string JsonTitle = GetValueFromJsonFile("..Books[2].value");
            string DetailTitle = Page.Detail.ProductTitle.Text;
            Assert.AreEqual(JsonTitle, DetailTitle);

            QuitBrowser();
        }
    }
}