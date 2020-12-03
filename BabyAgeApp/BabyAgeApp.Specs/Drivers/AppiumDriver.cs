using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using TechTalk.SpecRun;

namespace BabyAgeApp.Specs.Drivers
{
    public class AppiumDriver
    {
        private readonly TestRunContext _testRunContext;
        public AndroidDriver<AndroidElement> Driver { get; private set; }

        public AppiumDriver(TestRunContext testRunContext)
        {
            _testRunContext = testRunContext;
        }

        public void StartApp()
        {
            var driverOptions = new AppiumOptions();
            driverOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9.0");
            var apkPath = Path.Combine(_testRunContext.TestDirectory, "..\\..\\..\\..\\BabyAgeApp\\BabyAgeApp.Android\\bin\\Release\\com.companyname.babyageapp.apk");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.App, apkPath);

            Driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), driverOptions, TimeSpan.FromSeconds(180));
        }

        public void StopApp()
        {
            Driver.Quit();
        }
    }
}
