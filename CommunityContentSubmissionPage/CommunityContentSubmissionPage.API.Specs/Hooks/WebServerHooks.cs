using System.Threading.Tasks;
using CommunityContentSubmissionPage.API.Specs.Drivers;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.API.Specs.Hooks
{
    [Binding]
    public class WebServerHooks
    {
        private readonly WebServerDriver _webServerDriver;

        public WebServerHooks(WebServerDriver webServerDriver)
        {
            _webServerDriver = webServerDriver;
        }

        [BeforeScenario(Order = 1000)]
        public void StartWebserver()
        {
            _webServerDriver.Start();
        }

        [AfterScenario]
        public async Task StopWebserver()
        {
            await _webServerDriver.Stop();
        }
    }
}
