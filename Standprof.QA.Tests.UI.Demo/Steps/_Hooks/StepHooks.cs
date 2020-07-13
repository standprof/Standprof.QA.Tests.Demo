using Microsoft.VisualStudio.TestTools.UnitTesting;
using Standprof.QA.Common;
using TechTalk.SpecFlow;

namespace Standprof.QA.Tests.UI.Demo.Steps._Hooks
{
    [Binding]
    internal class StepHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestContext _testContext;
        private string _currentStep;

        public StepHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            _testContext = _scenarioContext.ScenarioContainer.Resolve<TestContext>();

            //_testContext.Logger().Trace("Step Hooks");
        }

        [Scope(Tag = "ui")]
        [BeforeStep]
        public void SaveStepText()
        {
            

            // Replace the step definition with "And" if it is the same as the previous one
            var currentStepDefinitionType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var previousStepDefinitionType = _scenarioContext["PreviousStepDefinitionType"].ToString();

            if (string.IsNullOrEmpty(previousStepDefinitionType))
            {
                CustomTestLoggerWrapper.CustomTestLogger(_testContext).Section("Steps:");
            }

            var stepDefinitionType = currentStepDefinitionType == previousStepDefinitionType
                ? "And"
                : currentStepDefinitionType;

            _currentStep = string.Concat(stepDefinitionType, " ", _scenarioContext.StepContext.StepInfo.Text);

            _scenarioContext["StepsText"] = string.Join("\r\n", _scenarioContext["StepsText"],
                _currentStep);

            _testContext.CustomTestLogger().Step(_currentStep);

            // Save the current step definition for the next step
            _scenarioContext["PreviousStepDefinitionType"] =
                _scenarioContext.StepContext.StepInfo.StepDefinitionType;
        }

        [Scope(Tag = "ui")]
        [AfterStep]
        public void MarkStepAsFailed()
        {
            if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
                _testContext.CustomTestLogger()
                    .Trace(
                        "Failed at step: '{0}' with the exception: {1}", _currentStep,
                        _scenarioContext.TestError.Message);
        }
    }
}