using CommunityContentSubmissionPage.Specs.Drivers;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.Specs.Steps
{
    [Binding]
    public class WebPageSteps
    {
        private readonly BrowserDriver _browserDriver;
        private readonly WebServerDriver _webServerDriver;

        public WebPageSteps(WebServerDriver webServerDriver, BrowserDriver browserDriver)
        {
            _webServerDriver = webServerDriver;
            _browserDriver = browserDriver;
        }

        [Given(@"the submission page is open")]
        [When(@"the submission page is open")]
        public void WhenTheSubmissionPageIsOpen()
        {
            _browserDriver.GoToUrl(_webServerDriver.Hostname);
        }

        [Then(@"the title of the page is '(.*)'")]
        public void ThenTheTitleOfThePageIs(string expectedPageTitle)
        {
            _browserDriver.AssertTitle(expectedPageTitle);
        }
    }
}