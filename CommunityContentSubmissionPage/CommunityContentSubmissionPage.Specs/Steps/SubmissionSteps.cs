using System;
using System.Collections.Generic;
using System.Text;
using CommunityContentSubmissionPage.Specs.Drivers;
using CommunityContentSubmissionPage.Specs.PageObject;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.Specs.Steps
{
    [Binding]
    public class SubmissionSteps
    {
        private readonly SubmissionPageDriver submissionPageDriver;

        public SubmissionSteps(SubmissionPageDriver submissionPageDriver)
        {
            this.submissionPageDriver = submissionPageDriver;
        }

        [Then(@"it is possible to enter a '(.*)' with label '(.*)'")]
        public void ThenItIsPossibleToEnterAWithLabel(string inputType, string expectedLabel)
        {
            submissionPageDriver.CheckExistanceOfInput(inputType, expectedLabel);
        }

    }
}
