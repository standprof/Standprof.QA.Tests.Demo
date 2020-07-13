using System;
using System.Linq;
using Standprof.QA.Common.Selenium;
using TechTalk.SpecFlow;

namespace Standprof.QA.Tests.UI.Demo.Steps._BaseSteps
{
    public abstract class UiBaseSteps
    {
        public ScenarioContext TheScenarioContext;

        protected UiBaseSteps(ScenarioContext scenarioContext)
        {
            TheScenarioContext = scenarioContext ?? throw new ArgumentNullException("scenarioContext");

            Browser = (WebDriverWrapper)TheScenarioContext["Browser"];
            //CustomTestLogger = Browser.Log;
        }

        public WebDriverWrapper Browser { get; set; }
        //public CustomTestLogger CustomTestLogger { get; private set; }

        public void SaveToScenarioContext(string key, object value)
        {
            if (TheScenarioContext.Keys.All(i => i != key))
            {
                TheScenarioContext.Add(key, value);
            }
            else
            {
                TheScenarioContext[key] = value;
            }
        }
    }
}
