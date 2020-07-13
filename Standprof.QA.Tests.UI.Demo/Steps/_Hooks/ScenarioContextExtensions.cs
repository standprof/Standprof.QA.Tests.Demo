using TechTalk.SpecFlow;

namespace Standprof.QA.Tests.UI.Demo.Steps._Hooks
{
    internal static class ScenarioContextExtensions
    {
        public static void ReAdd(this ScenarioContext scenarioContext,  string key, object value)
        {
            if (scenarioContext.ContainsKey(key))
            {
                scenarioContext.Remove(key);
            }

            scenarioContext.Add(key, value);
        }
    }
}
