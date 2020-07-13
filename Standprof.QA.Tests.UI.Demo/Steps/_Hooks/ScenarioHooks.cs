using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Standprof.QA.Common;
using Standprof.QA.Common.Selenium;
using TechTalk.SpecFlow;
using CustomTestLoggerWrapper = Standprof.QA.Common.CustomTestLoggerWrapper;

namespace Standprof.QA.Tests.UI.Demo.Steps._Hooks
{
#pragma warning disable 618
    [Binding]
    public class ScenarioHooks
    {
        public static object Padlock = new object();

        public static readonly Dictionary<int, WebDriverWrapper> OpenedBrowsers =
            new Dictionary<int, WebDriverWrapper>();

        private readonly FeatureContext _featureContext;
        private readonly TestContext _testContext;
        private ScenarioContext _scenarioContext;


        public ScenarioHooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;

            _testContext = _scenarioContext.ScenarioContainer.Resolve<TestContext>();
        }


        [Scope(Tag = "ui")]
        [BeforeScenario(Order = 1)]
        internal void StartLocalBrowser()
        {
            CustomTestLoggerWrapper.CustomTestLogger(_testContext).Section("BeforeScenario:");

            StartLocalBrowserInstance();
        }


        [Scope(Tag = "ui")]
        [AfterScenario(Order = 1)]
        internal void CloseLocalBrowser()
        {
            CustomTestLoggerWrapper.CustomTestLogger(_testContext).Section("AfterScenario:");
            CustomTestLoggerWrapper.CustomTestLogger(_testContext).Trace("Test Environment: " + TestConfig.TestEnvironment.ToUpper());

            CloseBrowserLocally();
        }


        private void StartLocalBrowserInstance()
        {
            lock (Padlock)
            {
                CustomTestLoggerWrapper.CustomTestLogger(_testContext).Trace($"Start new Browser at {DateTime.Now}");
                var newBrowserInstance = new WebDriverWrapper(_testContext);
                CustomTestLoggerWrapper.CustomTestLogger(_testContext)
                    .Trace($"Opened new browser instance, hashcode='{newBrowserInstance.GetHashCode()}'");

                CustomTestLoggerWrapper.CustomTestLogger(_testContext)
                    .Trace($"Browser version='{newBrowserInstance.GetBrowserNameVersion()}'");

                OpenedBrowsers.Add(newBrowserInstance.GetHashCode(), newBrowserInstance);

                TryToAddToScenarioContext(ref _scenarioContext, ScenarioKeys.Browser, newBrowserInstance);
                TryToAddToScenarioContext(ref _scenarioContext, ScenarioKeys.BrowserHashCode,
                    newBrowserInstance.GetHashCode());

                _scenarioContext.Add("StepsText", "");
                _scenarioContext.Add("PreviousStepDefinitionType", "");
                _scenarioContext.Add("BrowserVersion", newBrowserInstance.GetBrowserNameVersion());
            }
        }

        private void CloseBrowserLocally()
        {
            lock (Padlock)
            {
                if (!_scenarioContext.ContainsKey(ScenarioKeys.BrowserHashCode)) return;

                var scenarioLogging = new ScenarioLogging(_scenarioContext, _featureContext);

                #region Close browser

                var browserHashCode = (int) _scenarioContext[ScenarioKeys.BrowserHashCode];
                try
                {
                    if (!OpenedBrowsers.ContainsKey(browserHashCode))
                    {
                        CustomTestLoggerWrapper.CustomTestLogger(_testContext)
                            .Error($"The browser with hash code {browserHashCode} has been closed earlier.");
                    }
                    else
                    {
                        var browserToClose = OpenedBrowsers[browserHashCode];

                        scenarioLogging.LogScenarioInformation();

                        var browserLogger = new BrowserLogger(_testContext, browserToClose);
                        browserLogger.LastOpenedPageUrl();
                        if (_scenarioContext.TestError != null)
                        {
                            
                            browserLogger.Screenshot();
                            browserLogger.LastPageContent();
                        }

                        browserToClose.Driver.Quit();

                        OpenedBrowsers.Remove(browserHashCode);
                    }

                    CustomTestLoggerWrapper.CustomTestLogger(_testContext).Trace($"Browser with hash code: {browserHashCode} is closed.");
                }
                catch (Exception)
                {
                    CustomTestLoggerWrapper.CustomTestLogger(_testContext)
                        .Trace($"Failed to close browser with hash code: {browserHashCode}.");
                }

                #endregion

                scenarioLogging.SaveTestResultToAutomatedTestingDatabase();
            }
        }

        internal static void AddScreenshotToTestLog(IWebDriver driver)
        {
            var ss = ((ITakesScreenshot) driver).GetScreenshot();
            ss.SaveAsFile("screenshot.png");
        }

        private void TryToAddToScenarioContext(ref ScenarioContext context, string key, object value)
        {
            if (context.ContainsKey(key))
                CustomTestLoggerWrapper.CustomTestLogger(_testContext)
                    .Error($"Context for '{context.ScenarioInfo.Title}' already has the key '{key}'");
            else
                context.Add(key, value);
        }

        public struct ScenarioKeys
        {
            public static string BrowserHashCode = "BrowserHashCode";
            public static string Browser = "Browser";
        }
    }
}
#pragma warning restore 618