using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using TechTalk.SpecRun;

namespace BabyAgeApp.Specs.Hooks
{
    [Binding]
    public class AppiumHooks
    {
        private readonly TestRunContext _testRunContext;

        public AppiumHooks(TestRunContext testRunContext)
        {
            _testRunContext = testRunContext;
        }

        private AndroidDriver<AndroidElement> _driver;

        [BeforeScenario()]
        public void StartAndroidApp()
        {
            var driverOptions = new AppiumOptions();
            driverOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9.0");
            //var apkPath = Path.Combine(_testRunContext.TestDirectory, "..\\..\\..\\..\\BabyAgeApp\\BabyAgeApp.Android\\bin\\debug\\com.companyname.babyageapp-signed.apk");
            //driverOptions.AddAdditionalCapability(MobileCapabilityType.App, apkPath);
            
            driverOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.companyname.babyageapp");
            driverOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "crc64dc582b0601849455.MainActivity");

            _driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), driverOptions, TimeSpan.FromSeconds(180));
            
        }

        [AfterScenario()]
        public void ShutdownAndroidApp()
        {
            _driver.Quit();
        }
    }
}
