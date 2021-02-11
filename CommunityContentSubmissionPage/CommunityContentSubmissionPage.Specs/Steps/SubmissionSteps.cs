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
            var (inputFieldLocator, labelLocator) = GetInputFieldLocator(inputType);

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
        }

        private static (string inputFieldLocator, string labelLocator) GetInputFieldLocator(string inputType)
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

            return (inputFieldLocator, labelLocator);
        }

        [Given(@"the filled out submission entry form")]
        public async Task GivenTheFilledOutSubmissionEntryForm(Table table)
        {
            var rows = table.CreateSet<SubmissionEntryFormRowObject>();

            foreach (var row in rows)
            {
                await FillEntryElement(row.Label, row.Value);
            }
        }

        [When(@"the submission entry form is submitted")]
        public async Task WhenTheSubmissionEntryFormIsSubmitted()
        {
            await _page.ClickAsync(SubmissionPage.SubmitButton);
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
            _page.Url.Should().EndWith("Success", "because the success page should be displayed");
        }

        [Then(@"the submitting of data was not possible")]
        public void ThenTheSubmittingOfDataWasNotPossible()
        {
            _page.Url.Should().NotEndWith("Success", "the input form page should be displayed again");
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
        public async Task GivenTheSubmissionEntryFormIsFilled()
        {
            var submissionEntryFormRowObjects = new List<SubmissionEntryFormRowObject>
            {
                new SubmissionEntryFormRowObject("Url", "https://example.org"),
                new SubmissionEntryFormRowObject("Type", "Blog Posts"),
                new SubmissionEntryFormRowObject("Email", "someone@example.org"),
                new SubmissionEntryFormRowObject("Description", "something really cool")
            };

            foreach (var submissionEntryFormRowObject in submissionEntryFormRowObjects)
            {
                await FillEntryElement(submissionEntryFormRowObject.Label, submissionEntryFormRowObject.Value);
            }
        }

        private async Task FillEntryElement(string label, string value)
        {
            var (inputFieldLocator, labelLocator) = GetInputFieldLocator(label);

            await _page.TypeAsync(inputFieldLocator, value);
        }

        [Given(@"the privacy policy is not accepted")]
        public async Task GivenThePrivacyPolicyIsNotAcceptedAsync()
        {
            if (await _page.IsCheckedAsync(SubmissionPage.PrivacyPolicy))
            {
                await _page.CheckAsync(SubmissionPage.PrivacyPolicy);
            }
        }

        [Given(@"the privacy policy is accepted")]
        public async Task GivenThePrivacyPolicyIsAccepted()
        {
            await _page.CheckAsync(SubmissionPage.PrivacyPolicy);
        }

        [When(@"the form is reset")]
        public async Task WhenTheFormIsReset()
        {
            await _page.ClickAsync(SubmissionPage.CancelButton);
        }

        [Then(@"every input is set to its default values")]
        public async Task ThenEveryInputIsSetToItsDefaultValues()
        {
            (await _page.GetTextContentAsync(SubmissionPage.UrlInputField)).Should().BeEmpty();

            //var typeElement = await _page.QuerySelectorAsync(SubmissionPage.TypeSelect);
            //var innerHtml = await typeElement.GetInnerHtmlAsync();
            //var props = await typeElement.GetPropertiesAsync();

            //foreach (var prop in props)
            //{
            //    var propProps = await prop.Value.GetPropertiesAsync();
            //}

            //(await _page.GetTextContentAsync(SubmissionPage.TypeSelect)).Should().Be("Blog Posts");
            

            (await _page.GetTextContentAsync(SubmissionPage.EmailInputField)).Should().BeEmpty();
            (await _page.GetTextContentAsync(SubmissionPage.DescriptionInputField)).Should().BeEmpty();

            (await _page.IsCheckedAsync(SubmissionPage.PrivacyPolicy)).Should().BeFalse();
        }
    }
}