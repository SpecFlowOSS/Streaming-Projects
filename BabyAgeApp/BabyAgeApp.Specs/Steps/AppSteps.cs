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
        private readonly UIStateDriver _uiStateDriver;
        private readonly RefreshDriver _refreshDriver;

        public AppSteps(AppiumDriver appiumDriver, UIStateDriver uiStateDriver, RefreshDriver refreshDriver)
        {
            _appiumDriver = appiumDriver;
            _uiStateDriver = uiStateDriver;
            _refreshDriver = refreshDriver;
        }

        [Then(@"the app was started")]
        public void ThenTheAppWasStarted()
        {
            var titleElement = _appiumDriver.Driver.FindElementByAccessibilityId("appTitle");
            titleElement.Text.Should().Be("Baby Age");
        }

        [When(@"a refresh happens")]
        public void WhenARefreshHappens()
        {
            _uiStateDriver.Update();
            _refreshDriver.PressRefreshUIButton();
        }

        [Then(@"the UI shows updated values")]
        public void ThenTheUIShowsUpdatedValues()
        {
            _uiStateDriver.Update();
            _uiStateDriver.ShouldHaveChanged();

        }
    }
}