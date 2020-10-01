using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace CommunityContentSubmissionPage.Specs.Drivers
{
    public class BrowserDriver
    {
        private readonly WebDriverDriver webDriverDriver;

        public BrowserDriver(WebDriverDriver webDriverDriver)
        {
            this.webDriverDriver = webDriverDriver;
        }

        public string Title
        {
            get { return webDriverDriver.WebDriver.Title; }
        }

        public void AssertTitle(string expectedTitle)
        {
            Title.Should().Be(expectedTitle);
        }

        internal void GoToUrl(string url)
        {
            webDriverDriver.WebDriver.Url = url;
        }
    }
}
