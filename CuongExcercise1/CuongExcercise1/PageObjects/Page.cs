using CuongExcercise1.TestCases;
using SeleniumExtras.PageObjects;

namespace CuongExcercise1.PageObjects
{
    public static class Page
    {
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(TestBase.GetDriver(), page);
            return page;
        }

        public static HomePage Home
        {
            get { return GetPage<HomePage>(); }
        }

        public static DetailPage Detail
        {
            get { return GetPage<DetailPage>(); }
        }
    }
}