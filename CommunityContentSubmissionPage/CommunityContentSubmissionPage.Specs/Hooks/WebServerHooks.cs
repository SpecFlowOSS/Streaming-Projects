using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Specs.Drivers;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.Specs.Hooks
{
    [Binding]
    public class WebServerHooks
    {
        private readonly WebServerDriver webServerDriver;

        public WebServerHooks(WebServerDriver webServerDriver)
        {
            this.webServerDriver = webServerDriver;
        }

        [BeforeScenario]
        public void StartWebserver()
        {
            this.webServerDriver.Start();
        }

        [AfterScenario]
        public async Task StopWebserver()
        {
            await webServerDriver.Stop();
        }
    }
}
