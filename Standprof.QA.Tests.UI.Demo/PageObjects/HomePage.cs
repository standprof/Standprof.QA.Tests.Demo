using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Standprof.QA.Common.Selenium;

namespace Standprof.QA.Tests.UI.Demo.PageObjects
{
    public class HomePage:BasePage
    {
        private static readonly By ContactFormResponseLocator = By.Id("contactFormResponseContainer");
        public string ContactFormMessage => Browser.Driver.FindElement(ContactFormResponseLocator).Text;

        public HomePage GoTo()
        {
            Browser.Driver.Navigate().GoToUrl(TestConfigBrowser.SiteUrl);
            return this;
        }

        public HomePage(WebDriverWrapper browser) : base(browser)
        {
        }

        public void SendEmail(string name, string emailAddress, string details)
        {
            Browser.Driver.FindElement(By.CssSelector("[type = 'text']")).JavaScriptSendValue(name);
            Browser.Driver.FindElement(By.CssSelector("[type = 'email']")).JavaScriptSendValue(emailAddress);
            Browser.Driver.FindElement(By.Name("Details")).JavaScriptSendValue(details);
            Browser.Driver.FindElement(By.ClassName("contact-form-submit-btn")).JavaScriptClick();

            var wait = new WebDriverWait(Browser.Driver, TimeSpan.FromSeconds(3));
            wait.Message = "Failed to send an email to the company";
            wait.Until(d => d.FindElement(ContactFormResponseLocator).Displayed);


        }


        public bool IsHeaderDisplayed(string title)
        {
            var headerElement = Browser.Driver.FindElements(By.TagName("h2"))
                .FirstOrDefault(e => string.Equals(e.Text, title, StringComparison.InvariantCultureIgnoreCase));
            
            return headerElement.Displayed;
        }

        public OurServicesPage ClickOurServicesButton()
        {
            Browser.Driver.FindElement(By.LinkText("VIEW OUR SERVICES")).Click();
            return new OurServicesPage(Browser);
        }
    }
}