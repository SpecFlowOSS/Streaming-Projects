using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyAgeApp.Specs.Drivers;
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
        private readonly AppiumDriver _appiumDriver;

        public AppiumHooks(AppiumDriver appiumDriver)
        {
            _appiumDriver = appiumDriver;
        }
        
        [BeforeScenario()]
        public void StartAndroidApp()
        {
            _appiumDriver.StartApp();
        }

        [AfterScenario()]
        public void ShutdownAndroidApp()
        {
            _appiumDriver.StopApp();            
        }
    }
}
