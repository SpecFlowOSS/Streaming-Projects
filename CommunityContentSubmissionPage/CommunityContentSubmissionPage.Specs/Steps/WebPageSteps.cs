using System;
using System.Collections.Generic;
using System.Text;
using CommunityContentSubmissionPage.Specs.Drivers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.Specs.Steps
{
    [Binding]
    public class WebPageSteps
    {
        private readonly WebDriverDriver webDriverDriver;
        private readonly WebServerDriver webServerDriver;
        private readonly BrowserDriver browserDriver;

        public WebPageSteps(WebDriverDriver webDriverDriver, WebServerDriver webServerDriver,BrowserDriver browserDriver)
        {
            this.webDriverDriver = webDriverDriver;
            this.webServerDriver = webServerDriver;
            this.browserDriver = browserDriver;
        }
        
        [When(@"the submission page is open")]
        public void WhenTheSubmissionPageIsOpen()
        {
            browserDriver.GoToUrl(this.webServerDriver.Hostname);
        }

        [Then(@"the title of the page is '(.*)'")]
        public void ThenTheTitleOfThePageIs(string expectedPageTitle)
        {
            browserDriver.AssertTitle(expectedPageTitle);
        }

    }
}
