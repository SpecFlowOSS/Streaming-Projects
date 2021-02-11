using CommunityContentSubmissionPage.Specs.Drivers;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.Specs.Steps
{
    [Binding]
    public class WebPageSteps
    {
        private readonly BrowserDriver _browserDriver;
        private readonly WebDriverDriver _webDriverDriver;
        private readonly WebServerDriver _webServerDriver;

        public WebPageSteps(WebServerDriver webServerDriver, BrowserDriver browserDriver, WebDriverDriver webDriverDriver)
        {
            _webServerDriver = webServerDriver;
            _browserDriver = browserDriver;
            _webDriverDriver = webDriverDriver;
        }

        [Given(@"the submission page is open")]
        [When(@"the submission page is open")]
        public void WhenTheSubmissionPageIsOpen()
        {
            _webDriverDriver.WebDriver.Url = _webServerDriver.Hostname;
        }

        [Then(@"the title of the page is '(.*)'")]
        public void ThenTheTitleOfThePageIs(string expectedPageTitle)
        {
            _browserDriver.AssertTitle(expectedPageTitle);
        }
    }
}