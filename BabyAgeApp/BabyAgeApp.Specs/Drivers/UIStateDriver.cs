using FluentAssertions;

namespace BabyAgeApp.Specs.Drivers
{
    public class UIStateDriver
    {
        private readonly AppiumDriver _appiumDriver;

        public UIStateDriver(AppiumDriver appiumDriver)
        {
            _appiumDriver = appiumDriver;
        }

        public UIState Current { get; private set; }
        public UIState Before { get; private set; }


        public void Update()
        {
            Before = Current;
            Current = GetCurrentUIState();
        }

        public UIState GetCurrentUIState()
        {
            return new UIState(GetValueOfElement("ageInSeconds"),
                GetValueOfElement("ageInMinutes"),
                GetValueOfElement("ageInHours"),
                GetValueOfElement("ageInDays"),
                GetValueOfElement("ageInWeeks"),
                GetValueOfElement("ageInMonths"));
        }

        private int GetValueOfElement(string accessibilityId)
        {
            var textElement = _appiumDriver.Driver.FindElementByAccessibilityId(accessibilityId);

            var actualValue = int.Parse(textElement.Text);
            return actualValue;
        }

        public void ShouldHaveChanged()
        {
            Before.Should().NotBe(Current);
        }
    }
}
