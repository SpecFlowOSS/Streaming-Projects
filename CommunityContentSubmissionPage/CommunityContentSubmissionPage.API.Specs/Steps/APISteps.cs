using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityContentSubmissionPage.API.Specs.Drivers;
using CommunityContentSubmissionPage.API.Specs.Support;
using FluentAssertions;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CommunityContentSubmissionPage.API.Specs.Steps
{
    [Binding]
    public class APISteps
    {
        private readonly WebServerDriver _webServerDriver;

        public APISteps(WebServerDriver webServerDriver)
        {
            _webServerDriver = webServerDriver;
        }

        [Then(@"you can choose from the following Types:")]
        public void ThenYouCanChooseFromTheFollowingTypes(Table table)
        {
            var typenameEntries = table.CreateSet<TypenameEntry>();

            var restClient = new RestClient(_webServerDriver.Hostname);
            var restRequest = new RestRequest("api/AvailableTypes", DataFormat.Json);
            var restResponse = restClient.Get<AvailableTypesResponse>(restRequest);

            restResponse.IsSuccessful.Should().BeTrue();

            var actualTypes = restResponse.Data.Types;
            var expectedTypes = typenameEntries.Select(i => i.Typename);

            actualTypes.Should().BeEquivalentTo(expectedTypes);
        }

        private readonly SubmissionRequest _submissionRequest = new SubmissionRequest();
        private IRestResponse _submitFormResponse;

        [Given(@"the following submission entry")]
        public void GivenTheFollowingSubmissionEntry(Table table)
        {
            var submissionEntryFormRowObjects = table.CreateSet<SubmissionEntryFormRowObject>();

            foreach (var rowObject in submissionEntryFormRowObjects)
            {
                switch (rowObject.Label.ToUpper())
                {
                    case "URL":
                        _submissionRequest.Url = rowObject.Value;
                        break;
                    case "TYPE":
                        _submissionRequest.Type = rowObject.Value;
                        break;
                    case "EMAIL":
                        _submissionRequest.Email = rowObject.Value;
                        break;
                    case "DESCRIPTION":
                        _submissionRequest.Description = rowObject.Value;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        [Given(@"the privacy policy is accepted")]
        public void GivenThePrivacyPolicyIsAccepted()
        {
            _submissionRequest.AcceptPrivacyPolicy = true;
        }

        [Given(@"the privacy policy is not accepted")]
        public void GivenThePrivacyPolicyIsNotAccepted()
        {
            _submissionRequest.AcceptPrivacyPolicy = false;
        }


        [When(@"the submission entry is submitted")]
        public void WhenTheSubmissionEntryIsSubmitted()
        {
            var restClient = new RestClient(_webServerDriver.Hostname);
            var restRequest = new JsonRequest<SubmissionRequest, string>("api/Submit", _submissionRequest);

            _submitFormResponse = restClient.Post(restRequest);
        }

        [Then(@"the submitting of data was possible")]
        public void ThenTheSubmittingOfDataWasPossible()
        {
            _submitFormResponse.IsSuccessful.Should().BeTrue();
        }

        [Then(@"the submitting of data was not possible")]
        public void ThenTheSubmittingOfDataWasNotPossible()
        {
            _submitFormResponse.IsSuccessful.Should().BeFalse();
        }

        [Given(@"the submission entry is complete")]
        public void GivenTheSubmissionEntryIsComplete()
        {
            _submissionRequest.Url = "https://www.example.org";
            _submissionRequest.Type = "Blog Posts";
            _submissionRequest.Email = "someone@example.org";
            _submissionRequest.Description = "a description";
        }



        public class AvailableTypesResponse
        {
            public List<string> Types { get; set; } = new List<string>();
            public string SelectedType { get; set; } = String.Empty;
        }

        public class SubmissionRequest
        {
            public string Url { get; set; }

            public string Type { get; set; }

            public string Email { get; set; }

            public string Description { get; set; }

            public bool AcceptPrivacyPolicy { get; set; }
        }
    }
}
