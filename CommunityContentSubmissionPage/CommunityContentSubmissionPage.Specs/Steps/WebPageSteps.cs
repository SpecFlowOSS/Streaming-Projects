using System.Threading.Tasks;
using CommunityContentSubmissionPage.Specs.Drivers;
using FluentAssertions;
using PlaywrightSharp;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.Specs.Steps
{
    [Binding]
    public class WebPageSteps
    {
        private readonly WebServerDriver _webServerDriver;
        private readonly IPage _page;

        public WebPageSteps(WebServerDriver webServerDriver, IPage page)
        {
            _webServerDriver = webServerDriver;
            _page = page;
        }

        [Given(@"the submission page is open")]
        [When(@"the submission page is open")]
        public async Task WhenTheSubmissionPageIsOpen()
        {
            await _page.GoToAsync(_webServerDriver.Hostname);
        }

        [Then(@"the title of the page is '(.*)'")]
        public async Task ThenTheTitleOfThePageIs(string expectedPageTitle)
        {
            var title = await _page.GetTitleAsync();
            title.Should().Be(expectedPageTitle);
        }
    }
}