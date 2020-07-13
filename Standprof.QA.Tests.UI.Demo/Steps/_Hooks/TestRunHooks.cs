using System;
using System.IO;
using Standprof.QA.Common;
using Standprof.QA.Common.Selenium;
using TechTalk.SpecFlow;

namespace Standprof.QA.Tests.UI.Demo.Steps._Hooks
{
    [Binding]
    internal static class TestRunHooks
    {

        [BeforeTestRun(Order = 1)]
        public static void SetGlobalProperties()
        {
            TestRunLogger.Section("BeforeTestRun:");

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            TestConfig.InitializeGlobalSettings();
            TestConfigBrowser.InitializeSeleniumSettings();
            TestConfig.CreateLogsFolder();
        }

        [AfterTestRun(Order = 1)]
        public static void AfterTestRun()
        {
            TestRunLogger.Section("AfterTestRun:");

            TestRunLogger.Trace($"Test Environment = {TestConfig.TestEnvironment}");
            TestRunLogger.Trace($"Test Web Host = {TestConfigBrowser.SiteUrl}");
            TestRunLogger.Trace($"Test Browser = {TestConfigBrowser.Browser}");

            WebDriverWrapper.KillHangingBrowsers();

        }
    }
}