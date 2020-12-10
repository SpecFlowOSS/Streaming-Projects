using System;
using BabyAgeApp.Specs.Drivers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace BabyAgeApp.Specs.Steps
{
    

    [Binding]
    public class BabySteps
    {
        private readonly BackdoorDriver _backdoorDriver;
        private readonly UIStateDriver _uiStateDriver;
        private readonly RefreshDriver _refreshDriver;

        public BabySteps(BackdoorDriver backdoorDriver, UIStateDriver uiStateDriver, RefreshDriver refreshDriver)
        {
            _backdoorDriver = backdoorDriver;
            _uiStateDriver = uiStateDriver;
            _refreshDriver = refreshDriver;
        }

        [Given(@"the baby is born on '(.*)'")]
        public void GivenTheBabyIsBornOn(DateTime babyBirthday)
        {
            _backdoorDriver.SetBirthday(babyBirthday);
        }

        [Given(@"it is currently '(.*)'")]
        [When(@"it is currently '(.*)'")]
        public void WhenItIsCurrently(DateTime now)
        {
            _backdoorDriver.SetNow(now);
        }

        [Then(@"the baby is '(.*)' days old")]
        public void ThenTheBabyIsDaysOld(int daysOld)
        {
            _refreshDriver.PressRefreshUIButton();
            
            _uiStateDriver.Update();
            _uiStateDriver.Current.Days.Should().Be(daysOld);
        }

        [Then(@"the baby is '(.*)' weeks old")]
        public void ThenTheBabyIsWeeksOld(int weeksOld)
        {
            _refreshDriver.PressRefreshUIButton();
            
            _uiStateDriver.Update();
            _uiStateDriver.Current.Weeks.Should().Be(weeksOld);
        }

        [Then(@"the baby is '(.*)' months old")]
        public void ThenTheBabyIsMonthsOld(int monthsOld)
        {
            _refreshDriver.PressRefreshUIButton();

            _uiStateDriver.Update();
            _uiStateDriver.Current.Months.Should().Be(monthsOld);
        }
    }
}
