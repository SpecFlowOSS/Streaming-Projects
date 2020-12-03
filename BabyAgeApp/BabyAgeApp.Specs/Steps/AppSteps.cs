using System.Collections.Generic;
using BabyAgeApp.Specs.Drivers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace BabyAgeApp.Specs.Steps
{
    [Binding]
    public class AppSteps
    {
        private readonly AppiumDriver _appiumDriver;

        public AppSteps(AppiumDriver appiumDriver)
        {
            _appiumDriver = appiumDriver;
        }

        [Then(@"the app was started")]
        public void ThenTheAppWasStarted()
        {
            var titleElement = _appiumDriver.Driver.FindElementByAccessibilityId("appTitle");
            titleElement.Text.Should().Be("Baby Age");
        }
    }
}