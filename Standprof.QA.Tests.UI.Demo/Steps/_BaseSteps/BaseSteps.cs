using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Standprof.QA.Common;
using TechTalk.SpecFlow;

namespace Standprof.QA.Tests.UI.Demo.Steps._BaseSteps
{
    public abstract class BaseSteps
    {
        public ScenarioContext TheScenarioContext;

        protected BaseSteps(ScenarioContext scenarioContext)
        {
            this.TheScenarioContext = scenarioContext ?? throw new ArgumentNullException("scenarioContext");
            var testContext = scenarioContext.ScenarioContainer.Resolve<TestContext>();
            CustomTestLogger = new CustomTestLogger(testContext);
        }

        public CustomTestLogger CustomTestLogger { get; private set; }

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
