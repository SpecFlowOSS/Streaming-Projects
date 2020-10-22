using System;
using System.Collections.Generic;
using System.Text;
using CommunityContentSubmissionPage.Specs.PageObject;
using CommunityContentSubmissionPage.Specs.Steps;
using CommunityContentSubmissionPage.Specs.Support;
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
        public void CheckExistenceOfInputElement(string inputType, string expectedLabel)
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
                case "EMAIL":
                    submissionPageObject.EmailWebElement.Should().NotBeNull();
                    submissionPageObject.EmailLabel.Should().Be(expectedLabel);
                    break;
                case "DESCRIPTION":
                    submissionPageObject.DescriptionWebElement.Should().NotBeNull();
                    submissionPageObject.DescriptionLabel.Should().Be(expectedLabel);
                    break;
                default:
                    throw new NotImplementedException($"{inputType} not implemented");
            }
        }

        public void InputForm(IEnumerable<SubmissionEntryFormRowObject> rows)
        {
            var submissionPageObject = new SubmissionPageObject(webDriverDriver);

            foreach (var row in rows)
            {
                switch (row.Label.ToUpper())
                {
                    case "URL":
                        submissionPageObject.Url = row.Value;
                        break;
                    case "TYPE":
                        submissionPageObject.Type = row.Value;
                        break;
                    default:
                        throw new NotImplementedException($"{row.Label} not implemented");
                }
            }
        }

        public void SubmitForm()
        {
            var submissionPageObject = new SubmissionPageObject(webDriverDriver);
            submissionPageObject.ClickSubmitButton();
        }
    }
}
