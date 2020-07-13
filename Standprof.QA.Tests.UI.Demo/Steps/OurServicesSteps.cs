using Standprof.QA.Tests.UI.Demo.PageObjects;
using Standprof.QA.Tests.UI.Demo.Steps._BaseSteps;
using TechTalk.SpecFlow;

namespace Standprof.QA.Tests.UI.Demo.Steps
{
    [Binding]
    public class OurServicesSteps: UiBaseSteps
    {
        public OurServicesPage OurServicesPage => TheScenarioContext.Get<OurServicesPage>();

        [Then(@"the Our Services page should open")]
        public void ThenTheOurServicesPageShouldOpen()
        {
            OurServicesPage.WaitUntilPageIsOpened();
        }

        public OurServicesSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }
    }
}
