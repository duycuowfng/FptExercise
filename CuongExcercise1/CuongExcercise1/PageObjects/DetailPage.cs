using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CuongExcercise1.PageObjects
{
    class DetailPage
    {
        private IWebDriver driver;

        public DetailPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public string GetProductTitleInDetailPage()
        {
            string title = driver.FindElement(By.CssSelector("[id=productTitle]")).Text;
            return title;
        }
    }
}