using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyAgeApp.Specs.Drivers
{
    public class RefreshDriver
    {
        private readonly AppiumDriver _appiumDriver;

        public RefreshDriver(AppiumDriver appiumDriver)
        {
            _appiumDriver = appiumDriver;
        }

        public void PressRefreshUIButton()
        {
            var androidElement = _appiumDriver.Driver.FindElementByAccessibilityId("refresh");
            androidElement.Click();
        }
    }
}
