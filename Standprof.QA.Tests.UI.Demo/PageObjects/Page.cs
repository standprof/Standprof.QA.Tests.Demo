using OpenQA.Selenium;
using Standprof.QA.Common.Selenium;

namespace Standprof.QA.Tests.UI.Demo.PageObjects
{
    public class Page:BasePage
    {
        public Page(WebDriverWrapper browser) : base(browser)
        {
        }

        public static By HeaderLocator = By.TagName("h1");

        public string GetHeader()
        {
            return Browser.Driver.FindElement(HeaderLocator).Text;
        }
    }
}
