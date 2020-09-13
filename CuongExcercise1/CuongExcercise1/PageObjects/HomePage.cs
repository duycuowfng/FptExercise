using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace CuongExcercise1.PageObjects
{
    public class HomePage
    {
        [FindsBy(How = How.CssSelector, Using = "[id=searchDropdownBox]")]
        [CacheLookup]
        private IWebElement SearchDropdownBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[id=twotabsearchtextbox]")]
        [CacheLookup]
        private IWebElement SearchBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[value=Go]")]
        [CacheLookup]
        private IWebElement BtnSearch { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@data-index='0' and not(following-sibling::div[@class='threepsl-creative'])]//a[@class='a-link-normal a-text-normal']/*")]
        [CacheLookup]
        public IWebElement FirstResultBook { get; set; }

        public void SearchBook(string keyword)
        {
            var selectElement = new SelectElement(SearchDropdownBox);
            selectElement.SelectByText("Books");
            SearchBox.SendKeys(keyword);
            BtnSearch.Click();
        }

        public void ClickFirstResultBook()
        {
            FirstResultBook.Click();
        }
    }
}