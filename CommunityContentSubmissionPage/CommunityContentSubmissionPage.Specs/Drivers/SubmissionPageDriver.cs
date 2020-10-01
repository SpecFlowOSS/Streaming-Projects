using System;
using System.Collections.Generic;
using System.Text;
using CommunityContentSubmissionPage.Specs.PageObject;
using FluentAssertions;

namespace CommunityContentSubmissionPage.Specs.Drivers
{
    public class SubmissionPageDriver
    {
        private readonly WebDriverDriver webDriverDriver;

        public SubmissionPageDriver(WebDriverDriver webDriverDriver)
        {
            this.webDriverDriver = webDriverDriver;
        }
        public void CheckExistanceOfInput(string inputType, string expectedLabel)
        {
            var submissionPageObject = new SubmissionPageObject(webDriverDriver);

            switch (inputType.ToUpper())
            {
                case "URL":
                    submissionPageObject.UrlWebElement.Should().NotBeNull();
                    submissionPageObject.UrlLabel.Should().Be(expectedLabel);
                    break;
                case "TYPE":
                    submissionPageObject.TypeWebElement.Should().NotBeNull();
                    submissionPageObject.TypeLabel.Should().Be(expectedLabel);
                    break;
                default:
                    throw new NotImplementedException($"{inputType} not implemented");
            }
        }
    }
}
