using FluentAssertions;
using Standprof.QA.Tests.UI.Demo.PageObjects;
using Standprof.QA.Tests.UI.Demo.Steps._BaseSteps;
using TechTalk.SpecFlow;

namespace Standprof.QA.Tests.UI.Demo.Steps
{
    [Binding]
    public class HomePageSteps: UiBaseSteps
    {
        private HomePage HomePage => TheScenarioContext.Get<HomePage>();
     
        [When(@"I navigate to the Standprof web site")]
        [Given(@"I have opened the company Home page")]
        public void WhenINavigateToTheStandprofWebSite()
        {
            var homePage = new HomePage(Browser).GoTo();
            TheScenarioContext.Set(homePage);
        }
        
        [Then(@"the Home page should show a section with the title: ""(.*)""")]
        public void ThenTheHomePageShouldShowASectionWithTheTitle(string title)
        {
            HomePage.IsHeaderDisplayed(title).Should().BeTrue();
        }

        [When(@"I click Our Services")]
        public void WhenIClickOurServices()
        {
            var ourServicesPage = HomePage.ClickOurServicesButton();
            TheScenarioContext.Set(ourServicesPage);
        }

        public HomePageSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }
    }
}
