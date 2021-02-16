using System;
using System.Threading;
using FluentAssertions;

namespace CommunityContentSubmissionPage.Specs.Drivers
{
    public class BrowserDriver
    {
        private readonly WebDriverDriver _webDriverDriver;

        public BrowserDriver(WebDriverDriver webDriverDriver)
        {
            _webDriverDriver = webDriverDriver;
        }

        public string Title => _webDriverDriver.WebDriver.Title;

        public string Url => _webDriverDriver.WebDriver.Url;

        public void AssertTitle(string expectedTitle)
        {
            Title.Should().Be(expectedTitle);
        }

        internal void GoToUrl(string url)
        {
            _webDriverDriver.WebDriver.Navigate().GoToUrl(url);
        }
    }
}