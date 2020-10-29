using System;
using System.Collections.Generic;
using System.Text;
using CommunityContentSubmissionPage.Specs.Drivers;
using CommunityContentSubmissionPage.Specs.PageObject;
using CommunityContentSubmissionPage.Specs.Support;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CommunityContentSubmissionPage.Specs.Steps
{
    [Binding]
    public class SubmissionSteps
    {
        private readonly SubmissionPageDriver _submissionPageDriver;
        private readonly SubmissionDriver _submissionDriver;
        private readonly BrowserDriver _browserDriver;

        public SubmissionSteps(SubmissionPageDriver submissionPageDriver, SubmissionDriver submissionDriver, BrowserDriver browserDriver)
        {
            _submissionPageDriver = submissionPageDriver;
            _submissionDriver = submissionDriver;
            _browserDriver = browserDriver;
        }

        [Then(@"it is possible to enter a '(.*)' with label '(.*)'")]
        public void ThenItIsPossibleToEnterAWithLabel(string inputType, string expectedLabel)
        {
            _submissionPageDriver.CheckExistenceOfInputElement(inputType, expectedLabel);
        }

        [Given(@"the filled out submission entry form")]
        public void GivenTheFilledOutSubmissionEntryForm(Table table)
        {
            var rows = table.CreateSet<SubmissionEntryFormRowObject>();

            _submissionPageDriver.InputForm(rows);
        }

        [When(@"the submission entry form is submitted")]
        public void WhenTheSubmissionEntryFormIsSubmitted()
        {
            _submissionPageDriver.SubmitForm();
        }

        [Then(@"there is one submission entry stored")]
        public void ThenThereIsOneSubmissionEntryStored()
        {
            _submissionDriver.AssertOneSubmissionEntryExists();
        }

        [Then(@"there is '(.*)' submission entry stored")]
        public void ThenThereIsSubmissionEntryStored(int expectedCountOfStoredEntries)
        {
            _submissionDriver.AssertNumberOfEntriesStored(expectedCountOfStoredEntries);
        }

        [Then(@"the submitting of data was possible")]
        public void ThenTheSubmittingOfDataWasPossible()
        {
            _browserDriver.Url.Should().EndWith("Success", "because the success page should be displayed");
        }

        [Then(@"the submitting of data was not possible")]
        public void ThenTheSubmittingOfDataWasNotPossible()
        {
            _browserDriver.Url.Should().NotEndWith("Success", "the input form page should be displayed again");
        }



        [Then(@"there is a submission entry stored with the following data:")]
        public void ThenThereIsASubmissionEntryStoredWithTheFollowingData(Table table)
        {
            var expectedSubmissionContentEntry = table.CreateInstance<ExpectedSubmissionContentEntry>();

            _submissionDriver.AssertSubmissionEntryData(expectedSubmissionContentEntry);
        }

        [Then(@"you can choose from the following Types:")]
        public void ThenYouCanChooseFromTheFollowingTypes(Table table)
        {
            var expectedTypenameEntries = table.CreateSet<TypenameEntry>();
            _submissionPageDriver.CheckTypeEntries(expectedTypenameEntries);
        }

        [Given(@"the submission entry form is filled")]
        public void GivenTheSubmissionEntryFormIsFilled()
        {
            _submissionPageDriver.InputForm(new List<SubmissionEntryFormRowObject>()
            {
                new SubmissionEntryFormRowObject(){ Label = "Url", Value = "https://example.org"},
                new SubmissionEntryFormRowObject(){ Label = "Type", Value = "Blog Posts"},
                new SubmissionEntryFormRowObject(){ Label = "Email", Value = "someone@example.org"},
                new SubmissionEntryFormRowObject(){ Label = "Description", Value = "something really cool"},
            });
        }

        [Given(@"the privacy policy is not accepted")]
        public void GivenThePrivacyPolicyIsNotAccepted()
        {
            _submissionPageDriver.DoNotAcceptPrivacyPolicy();
        }

        [Given(@"the privacy policy is accepted")]
        public void GivenThePrivacyPolicyIsAccepted()
        {
            _submissionPageDriver.AcceptPrivacyPolicy();
        }


    }

    public class TypenameEntry
    {
        public string Typename { get; set; } = String.Empty;
    }

    public class ExpectedSubmissionContentEntry 
    {
        public string? Type { get; set; }
        public string? Url { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
    }
}
