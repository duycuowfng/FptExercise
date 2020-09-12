using NUnit.Framework;
using CuongExcercise1.PageObjects;
using System.Threading;

namespace CuongExcercise1.TestCases
{
    class HomePageTest : TestBase
    {
        [Test]
        public void VerifyBookTitleDisplayCorrectlyInDetailPage()
        {
            InitBrowser("Chrome");

            var homePage = new HomePage(driver);
            homePage.SearchBook("Selenium");
            Thread.Sleep(5000);
            homePage.ClickFirstResultBook();

            string JsonTitle = GetValueFromJsonFile("..Books[2].value");
            var detailPage = new DetailPage(driver);
            string UI_Title = detailPage.GetProductTitleInDetailPage();
            Assert.AreEqual(JsonTitle, UI_Title);

            QuitBrowser();
        }
    }
}