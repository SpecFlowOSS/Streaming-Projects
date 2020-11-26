using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public BabySteps(AppiumDriver appiumDriver)
        {
            _appiumDriver = appiumDriver;
        }

        [Given(@"the baby is born on '(.*)'")]
        public void GivenTheBabyIsBornOn(string babyBirthday)
        {
            
        }

        [When(@"today is '(.*)'")]
        public void WhenTodayIs(string today)
        {
            
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
