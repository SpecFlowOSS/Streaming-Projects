using System;
using System.Collections.Generic;
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
    public class SubmitSteps
    {
        private readonly WebServerDriver _webServerDriver;
        private readonly SubmissionRequest _submissionRequest = new SubmissionRequest();
        private IRestResponse _submitFormResponse;

        public SubmitSteps(WebServerDriver webServerDriver)
        {
            _webServerDriver = webServerDriver;
        }

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


        [Given(@"the submission entry is complete")]
        public void GivenTheSubmissionEntryIsComplete()
        {
            _submissionRequest.Url = "https://www.example.org";
            _submissionRequest.Type = "Blog Posts";
            _submissionRequest.Email = "someone@example.org";
            _submissionRequest.Description = "a description";
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
    }
}
