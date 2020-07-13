using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using Standprof.QA.Common.Selenium;

namespace Standprof.QA.Tests.UI.Demo.PageObjects
{
    public class OurServicesPage : BasePage
    {
        public OurServicesPage(WebDriverWrapper browser) : base(browser)
        {
        }

        public void WaitUntilPageIsOpened()
        {
            var element = Browser.Driver.FindElements(By.TagName("h2"))
                .FirstOrDefault(e => e.Text == "OUR SERVICES" && e.Displayed);
            Debug.Assert(element != null, nameof(element) + " != null");
        }
    }
}