using FluentAssertions;
using Standprof.QA.Tests.UI.Demo.PageObjects;
using Standprof.QA.Tests.UI.Demo.Steps._BaseSteps;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Standprof.QA.Tests.UI.Demo.Steps
{
    [Binding]
    public class SendEmailSteps:UiBaseSteps
    {
        public HomePage HomePage => TheScenarioContext.Get<HomePage>();

        [When(@"I send an email with the following info:")]
        public void WhenISendAnEmailWithTheFollowingInfo(Table table)
        {
            var contactUsForm = table.CreateInstance<ContactUsForm>();
            HomePage.SendEmail(contactUsForm.Name,
                contactUsForm.EmailAddress,
                contactUsForm.Details);
        }
        
        [Then(@"the page should show the ""(.*)""")]
        public void ThenThePageShouldShowThe(string message)
        {
            HomePage.ContactFormMessage.Should().Be(message);
        }

        public SendEmailSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }
    }

    public class ContactUsForm
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Details { get; set; }
    }
}
