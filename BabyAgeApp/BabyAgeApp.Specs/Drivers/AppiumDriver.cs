using System;
using System.Collections.Generic;
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
            //var apkPath = Path.Combine(_testRunContext.TestDirectory, "..\\..\\..\\..\\BabyAgeApp\\BabyAgeApp.Android\\bin\\debug\\com.companyname.babyageapp-signed.apk");
            //driverOptions.AddAdditionalCapability(MobileCapabilityType.App, apkPath);

            driverOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.companyname.babyageapp");
            driverOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "crc64dc582b0601849455.MainActivity");

            Driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), driverOptions, TimeSpan.FromSeconds(180));
        }

        public void StopApp()
        {
            Driver.Quit();
        }
    }
}
