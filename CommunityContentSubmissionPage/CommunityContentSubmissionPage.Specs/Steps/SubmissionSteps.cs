using System;
using System.Collections.Generic;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using CommunityContentSubmissionPage.Specs.Drivers;
using CommunityContentSubmissionPage.Specs.Pages;
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
        private readonly BrowserDriver _browserDriver;
        private readonly Actor _actor;
        private readonly SubmissionDriver _submissionDriver;
        private readonly SubmissionPageDriver _submissionPageDriver;

        public SubmissionSteps(SubmissionPageDriver submissionPageDriver, SubmissionDriver submissionDriver,
            BrowserDriver browserDriver, Actor actor)
        {
            _submissionPageDriver = submissionPageDriver;
            _submissionDriver = submissionDriver;
            _browserDriver = browserDriver;
            _actor = actor;
        }

        [Then(@"it is possible to enter a '(.*)' with label '(.*)'")]
        public void ThenItIsPossibleToEnterAWithLabel(string inputType, string expectedLabel)
        {
            IWebLocator inputFieldLocator;
            IWebLocator labelLocator;

            switch (inputType.ToUpper())
            {
                case "URL":
                    inputFieldLocator = SubmissionPage.UrlInputField;
                    labelLocator = SubmissionPage.UrlLabel;
                    break;
                default: 
                    throw new NotImplementedException();
            }

            _actor.AttemptsTo(Wait.Until(Appearance.Of(inputFieldLocator), IsEqualTo.True()));
            _actor.AskingFor(Text.Of(labelLocator)).Should().Be(expectedLabel);
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
            _submissionPageDriver.InputForm(new List<SubmissionEntryFormRowObject>
            {
                new SubmissionEntryFormRowObject {Label = "Url", Value = "https://example.org"},
                new SubmissionEntryFormRowObject {Label = "Type", Value = "Blog Posts"},
                new SubmissionEntryFormRowObject {Label = "Email", Value = "someone@example.org"},
                new SubmissionEntryFormRowObject {Label = "Description", Value = "something really cool"}
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

        [When(@"the form is reset")]
        public void WhenTheFormIsReset()
        {
            _submissionPageDriver.ResetForm();
        }

        [Then(@"every input is set to its default values")]
        public void ThenEveryInputIsSetToItsDefaultValues()
        {
            _submissionPageDriver.CheckDefaultValues();
        }
    }
}