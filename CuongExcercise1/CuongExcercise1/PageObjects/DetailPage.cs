using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CuongExcercise1.PageObjects
{
    public class DetailPage
    {
        [FindsBy(How = How.CssSelector, Using = "[id=productTitle]")]
        [CacheLookup]
        public IWebElement ProductTitle { get; set; }
    }
}