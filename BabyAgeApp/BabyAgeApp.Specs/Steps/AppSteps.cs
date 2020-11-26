using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyAgeApp.Specs.Drivers;
using FluentAssertions;
using OpenQA.Selenium;
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
