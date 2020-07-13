using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Standprof.QA.Common.Selenium;

namespace Standprof.QA.Tests.UI.Demo.PageObjects
{
    public class BasePage
    {
        protected internal static readonly By PopupLocator = By.CssSelector(".modal.modal-open");
        
        public BasePage(WebDriverWrapper browser)
        {
            Browser = browser;

            Browser.CheckForPageErrors();
        }

        public WebDriverWrapper Browser { get; set; }

        public class ConfirmationPopup
        {
            private readonly IWebElement _element;

            public ConfirmationPopup(IWebElement element)
            {
                _element = element;
            }

            public void ClickOk()
            {
                var buttonsElements = _element.FindElements(By.CssSelector(".btn.btn-main"));
                buttonsElements.First(e => e.Text.Contains("OK")).Click();
            }
        }

        public virtual void RefreshPage()
        {
            Browser.Driver.Navigate().Refresh();
            Browser.Driver.WaitUntilLoadingCompleted();
        }

        public void WaitUntilPageIsOpened(By locator, string failureMessage, int waitInSeconds = 10)
        {
            Browser.Driver.WaitUntilLoadingCompleted();

            var wait = new WebDriverWait(Browser.Driver, TimeSpan.FromSeconds(waitInSeconds)) {Message = failureMessage};
            wait.Until(d =>
            {
                if (IsErrorFound(out var errorMessage))
                {
                    Browser.Log.Error(errorMessage);
                }

                return d.FindElement(locator).Displayed;
            });
        }

        public void CheckPageIsNotEmpty(Type pageObject)
        {
            if (Browser.Driver.FindElement(By.Id("content")).Text.Length == 0)
            {
                Browser.Log.Error($"{pageObject.Name} is empty");
            }
        }

        public bool IsErrorFound(out string errorText)
        {
            errorText = string.Empty;

            var errors = Browser.Driver.FindElements(By.ClassName("error-code"));

            if (errors.Count > 0)
            {
                errorText = errors.FirstOrDefault().Text;
            }

            return errors.Count > 0;
        }





    }
}