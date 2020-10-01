using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.Specs.Steps
{
    [Binding]
    public class WebPageSteps
    {
        [When(@"the submission page is open")]
        public void WhenTheSubmissionPageIsOpen()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the title of the page is '(.*)'")]
        public void ThenTheTitleOfThePageIs(string expectedPageTitle)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
