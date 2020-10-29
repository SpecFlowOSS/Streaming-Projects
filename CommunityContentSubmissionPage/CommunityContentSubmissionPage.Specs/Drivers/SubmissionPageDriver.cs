using System;
using System.Collections.Generic;
using System.Linq;
using CommunityContentSubmissionPage.Specs.PageObject;
using CommunityContentSubmissionPage.Specs.Steps;
using CommunityContentSubmissionPage.Specs.Support;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;

namespace CommunityContentSubmissionPage.Specs.Drivers
{
    public class SubmissionPageDriver
    {
        private readonly WebDriverDriver _webDriverDriver;

        public SubmissionPageDriver(WebDriverDriver webDriverDriver)
        {
            _webDriverDriver = webDriverDriver;
        }

        public void CheckExistenceOfInputElement(string inputType, string expectedLabel)
        {
            var submissionPageObject = new SubmissionPageObject(_webDriverDriver);

            switch (inputType.ToUpper())
            {
                case "URL":
                    submissionPageObject.UrlInputEntry.ValueWebElement.Should().NotBeNull();
                    submissionPageObject.UrlInputEntry.Label.Should().Be(expectedLabel);
                    break;
                case "TYPE":
                    submissionPageObject.TypeSelectEntry.ValueWebElement.Should().NotBeNull();
                    submissionPageObject.TypeSelectEntry.Label.Should().Be(expectedLabel);
                    break;
                case "EMAIL":
                    submissionPageObject.EmailInputEntry.ValueWebElement.Should().NotBeNull();
                    submissionPageObject.EmailInputEntry.Label.Should().Be(expectedLabel);
                    break;
                case "DESCRIPTION":
                    submissionPageObject.DescriptionInputEntry.ValueWebElement.Should().NotBeNull();
                    submissionPageObject.DescriptionInputEntry.Label.Should().Be(expectedLabel);
                    break;
                default:
                    throw new NotImplementedException($"{inputType} not implemented");
            }
        }

        public void InputForm(IEnumerable<SubmissionEntryFormRowObject> rows)
        {
            var submissionPageObject = new SubmissionPageObject(_webDriverDriver);

            foreach (var row in rows)
                switch (row.Label.ToUpper())
                {
                    case "URL":
                        submissionPageObject.UrlInputEntry.Value = row.Value;
                        break;
                    case "TYPE":
                        submissionPageObject.TypeSelectEntry.Value = row.Value;
                        break;
                    case "EMAIL":
                        submissionPageObject.EmailInputEntry.Value = row.Value;
                        break;
                    case "DESCRIPTION":
                        submissionPageObject.DescriptionInputEntry.Value = row.Value;
                        break;
                    default:
                        throw new NotImplementedException($"{row.Label} not implemented");
                }
        }

        public void SubmitForm()
        {
            var submissionPageObject = new SubmissionPageObject(_webDriverDriver);
            submissionPageObject.ClickSubmitButton();
        }

        public void CheckTypeEntries(IEnumerable<TypenameEntry> expectedTypenameEntries)
        {
            var submissionPageObject = new SubmissionPageObject(_webDriverDriver);

            var typeSelectElement = new SelectElement(submissionPageObject.TypeSelectEntry.ValueWebElement);
            var webElements = typeSelectElement.Options.ToList();
            var actualTypenameEntries = webElements.Select(i => new TypenameEntry {Typename = i.Text}).ToList();

            actualTypenameEntries.Should().BeEquivalentTo(expectedTypenameEntries);
        }

        public void AcceptPrivacyPolicy()
        {
            var submissionPageObject = new SubmissionPageObject(_webDriverDriver);

            if (submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement is null)
            {
                throw new Exception("Can't accept privacy policy, because the checkbox is missing");
            }

            submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement.Click();
        }

        public void DoNotAcceptPrivacyPolicy()
        {
            var submissionPageObject = new SubmissionPageObject(_webDriverDriver);

            if (submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement is null)
            {
                throw new Exception("Can't decline privacy policy, because the checkbox is missing");
            }

            if (submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement.Selected)
                submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement.Click();
        }

        public void ResetForm()
        {
            var submissionPageObject = new SubmissionPageObject(_webDriverDriver);
            submissionPageObject.ClickResetButton();
        }

        public void CheckDefaultValues()
        {
            var submissionPageObject = new SubmissionPageObject(_webDriverDriver);

            submissionPageObject.UrlInputEntry.Value.Should().BeEmpty();
            submissionPageObject.TypeSelectEntry.Value.Should().Be("Blog Posts");
            submissionPageObject.EmailInputEntry.Value.Should().BeEmpty();
            submissionPageObject.DescriptionInputEntry.Value.Should().BeEmpty();
            
            if (submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement is null)
            {
                throw new Exception("Can't accept privacy policy, because the checkbox is missing");
            }

            submissionPageObject.PrivacyPolicyInputEntry.ValueWebElement.Selected.Should().BeFalse();
        }
    }
}