using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BabyAgeApp.Specs.Drivers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace BabyAgeApp.Specs.Steps
{
    [Binding]
    public class BabySteps
    {
        private readonly AppiumDriver _appiumDriver;
        private readonly BackdoorDriver _backdoorDriver;

        public BabySteps(AppiumDriver appiumDriver, BackdoorDriver backdoorDriver)
        {
            _appiumDriver = appiumDriver;
            _backdoorDriver = backdoorDriver;
        }

        [Given(@"the baby is born on '(.*)'")]
        public void GivenTheBabyIsBornOn(DateTime babyBirthday)
        {
            _backdoorDriver.SetBirthday(babyBirthday);
        }

        [When(@"it is currently '(.*)'")]
        public void WhenItIsCurrently(DateTime now)
        {
            _backdoorDriver.SetNow(now);
        }


        [Then(@"the baby is '(.*)' days old")]
        public void ThenTheBabyIsDaysOld(int daysOld)
        {
            var ageInDaysElement = _appiumDriver.Driver.FindElementByAccessibilityId("ageInDays");

            var actualDaysOld = int.Parse(ageInDaysElement.Text);
            actualDaysOld.Should().Be(daysOld);
        }

    }
}
