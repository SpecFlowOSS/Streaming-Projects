using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Specs.Drivers;
using CommunityContentSubmissionPage.Specs.Pages;
using CommunityContentSubmissionPage.Specs.Support;
using FluentAssertions;
using PlaywrightSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CommunityContentSubmissionPage.Specs.Steps
{
    [Binding]
    public class SubmissionSteps
    {
        private readonly SubmissionDriver _submissionDriver;
        private readonly IPage _page;

        public SubmissionSteps(SubmissionDriver submissionDriver, IPage page)
        {
            _submissionDriver = submissionDriver;
            _page = page;
        }

        [Then(@"it is possible to enter a '(.*)' with label '(.*)'")]
        public async Task ThenItIsPossibleToEnterAWithLabel(string inputType, string expectedLabel)
        {
            string inputFieldLocator;
            string labelLocator;

            switch (inputType.ToUpper())
            {
                case "URL":
                    inputFieldLocator = SubmissionPage.UrlInputField;
                    labelLocator = SubmissionPage.UrlLabel;
                    break;
                case "TYPE":
                    inputFieldLocator = SubmissionPage.TypeSelect;
                    labelLocator = SubmissionPage.TypeLabel;
                    break;
                case "EMAIL":
                    inputFieldLocator = SubmissionPage.EmailInputField;
                    labelLocator = SubmissionPage.EmailLabel;
                    break;
                case "DESCRIPTION":
                    inputFieldLocator = SubmissionPage.DescriptionInputField;
                    labelLocator = SubmissionPage.DescriptionLabel;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (await _page.IsVisibleAsync(inputFieldLocator))
            {
                var element = await _page.QuerySelectorAsync(labelLocator);

                if (element != null)
                {
                    var actualLabel = await element.GetTextContentAsync();
                    actualLabel.Should().Be(expectedLabel);
                }
                else
                {
                    throw new Exception($"Element {labelLocator} wasn't found");
                }
            }
            else
            {
                throw new Exception($"Element {inputFieldLocator} wasn't found");
            }


            //_actor.AttemptsTo(Wait.Until(Appearance.Of(inputFieldLocator), IsEqualTo.True()));
            //_actor.AskingFor(Text.Of(labelLocator)).Should().Be(expectedLabel);


        }

        [Given(@"the filled out submission entry form")]
        public void GivenTheFilledOutSubmissionEntryForm(Table table)
        {
            var rows = table.CreateSet<SubmissionEntryFormRowObject>();

            //_actor.AttemptsTo(FillOutSubmissionForm.With(rows));
        }

        [When(@"the submission entry form is submitted")]
        public void WhenTheSubmissionEntryFormIsSubmitted()
        {
            //_actor.AttemptsTo(Click.On(SubmissionPage.SubmitButton));
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

            //_actor.AsksFor(CurrentUrl.FromBrowser()).Should().EndWith("Success", "because the success page should be displayed");
        }

        [Then(@"the submitting of data was not possible")]
        public void ThenTheSubmittingOfDataWasNotPossible()
        {
            //_actor.AsksFor(CurrentUrl.FromBrowser()).Should().NotEndWith("Success", "the input form page should be displayed again");
        }


        [Then(@"there is a submission entry stored with the following data:")]
        public void ThenThereIsASubmissionEntryStoredWithTheFollowingData(Table table)
        {
            var expectedSubmissionContentEntry = table.CreateInstance<ExpectedSubmissionContentEntry>();

            _submissionDriver.AssertSubmissionEntryData(expectedSubmissionContentEntry);
        }

        [Then(@"you can choose from the following Types:")]
        public async Task ThenYouCanChooseFromTheFollowingTypes(Table table)
        {
            var expectedTypenameEntries = table.CreateSet<TypenameEntry>();

            var typeSelector = await _page.QuerySelectorAsync(SubmissionPage.TypeSelect);

            if (typeSelector == null)
            {
                throw new Exception("Type selector not found");
            }

            var options = await typeSelector.QuerySelectorAllAsync("option");

            var possibleOptions = new List<TypenameEntry>();
            foreach (var option in options)
            {
                var text = await option.GetTextContentAsync();
                possibleOptions.Add(new TypenameEntry(text));
            }

            possibleOptions.Should().BeEquivalentTo(expectedTypenameEntries);

        }

        [Given(@"the submission entry form is filled")]
        public void GivenTheSubmissionEntryFormIsFilled()
        {
            var submissionEntryFormRowObjects = new List<SubmissionEntryFormRowObject>
            {
                new SubmissionEntryFormRowObject("Url", "https://example.org"),
                new SubmissionEntryFormRowObject("Type", "Blog Posts"),
                new SubmissionEntryFormRowObject("Email", "someone@example.org"),
                new SubmissionEntryFormRowObject("Description", "something really cool")
            };

            //_actor.AttemptsTo(FillOutSubmissionForm.With(submissionEntryFormRowObjects));
        }

        [Given(@"the privacy policy is not accepted")]
        public void GivenThePrivacyPolicyIsNotAccepted()
        {
            //var privacyPolicyIsChecked = _actor.AskingFor(SelectedState.Of(SubmissionPage.PrivacyPolicy));
            //if (privacyPolicyIsChecked)
            //{
            //    _actor.AttemptsTo(Click.On(SubmissionPage.PrivacyPolicy));
            //}
        }

        [Given(@"the privacy policy is accepted")]
        public void GivenThePrivacyPolicyIsAccepted()
        {
            //_actor.AttemptsTo(Click.On(SubmissionPage.PrivacyPolicy));
        }

        [When(@"the form is reset")]
        public void WhenTheFormIsReset()
        {
            //_actor.AttemptsTo(Click.On(SubmissionPage.CancelButton));
        }

        [Then(@"every input is set to its default values")]
        public void ThenEveryInputIsSetToItsDefaultValues()
        {
            //_actor.AsksFor(Text.Of(SubmissionPage.UrlInputField)).Should().BeEmpty();
            //_actor.AsksFor(SelectedOptionText.Of(SubmissionPage.TypeSelect)).Should().Be("Blog Posts");
            //_actor.AsksFor(Text.Of(SubmissionPage.EmailInputField)).Should().BeEmpty();
            //_actor.AsksFor(Text.Of(SubmissionPage.DescriptionInputField)).Should().BeEmpty();
            //_actor.AsksFor(SelectedState.Of(SubmissionPage.PrivacyPolicy)).Should().BeFalse();
        }
    }
}