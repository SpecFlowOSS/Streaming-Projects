using CommunityContentSubmissionPage.API.Specs.Drivers;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.API.Specs.Steps
{
    [Binding]
    public class WebPageSteps
    {
        private readonly WebServerDriver _webServerDriver;

        public WebPageSteps(WebServerDriver webServerDriver)
        {
            _webServerDriver = webServerDriver;
        }

    }
}