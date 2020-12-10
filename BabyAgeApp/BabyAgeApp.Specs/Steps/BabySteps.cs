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
            ShouldBeNumberOf_Old("ageInDays", daysOld);
        }

        [Then(@"the baby is '(.*)' weeks old")]
        public void ThenTheBabyIsWeeksOld(int weeksOld)
        {
            ShouldBeNumberOf_Old("ageInWeeks", weeksOld);
        }

        [Then(@"the baby is '(.*)' months old")]
        public void ThenTheBabyIsMonthsOld(int monthsOld)
        {
            ShouldBeNumberOf_Old("ageInMonths", monthsOld);
        }


        private void ShouldBeNumberOf_Old(string accessibilityId, int expectedValue)
        {
            var textElement = _appiumDriver.Driver.FindElementByAccessibilityId(accessibilityId);

            var actualValue = int.Parse(textElement.Text);
            actualValue.Should().Be(expectedValue);
        }
    }
}
