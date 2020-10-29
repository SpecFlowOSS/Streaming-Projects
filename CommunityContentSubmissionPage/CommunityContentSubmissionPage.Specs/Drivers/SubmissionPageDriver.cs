using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityContentSubmissionPage.Specs.PageObject;
using CommunityContentSubmissionPage.Specs.Steps;
using CommunityContentSubmissionPage.Specs.Support;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;

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
                    case "EMAIL":
                        submissionPageObject.Email = row.Value;
                        break;
                    case "DESCRIPTION":
                        submissionPageObject.Description = row.Value;
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

        public void CheckTypeEntries(IEnumerable<TypenameEntry> expectedTypenameEntries)
        {
            var submissionPageObject = new SubmissionPageObject(webDriverDriver);

            var typeSelectElement = new SelectElement(submissionPageObject.TypeWebElement);
            var webElements = typeSelectElement.Options.ToList();
            var actualTypenameEntries = webElements.Select(i => new TypenameEntry() {Typename = i.Text}).ToList();

            actualTypenameEntries.Should().BeEquivalentTo(expectedTypenameEntries);
        }

        public void AcceptPrivacyPolicy()
        {
            var submissionPageObject = new SubmissionPageObject(webDriverDriver);

            submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement.Click();
        }

        public void DoNotAcceptPrivacyPolicy()
        {
            var submissionPageObject = new SubmissionPageObject(webDriverDriver);

            if (submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement.Selected)
            {
                submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement.Click();
            }
        }

        public void ResetForm()
        {
            var submissionPageObject = new SubmissionPageObject(webDriverDriver);
            submissionPageObject.ClickResetButton();
        }

        public void CheckDefaultValues()
        {
            var submissionPageObject = new SubmissionPageObject(webDriverDriver);

            submissionPageObject.Url.Should().BeEmpty();
            submissionPageObject.Type.Should().Be("Blog Posts");
            submissionPageObject.Email.Should().BeEmpty();
            submissionPageObject.Description.Should().BeEmpty();
            submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement.Selected.Should().BeFalse();
        }
    }
}
