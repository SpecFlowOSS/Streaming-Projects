using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CommunityContentSubmissionPage.Specs.Drivers
{
    public class WebDriverDriver : IDisposable
    {
        private Lazy<IWebDriver> webDriver;

        public WebDriverDriver()
        {
            webDriver = new Lazy<IWebDriver>(() => CreateWebDriver());
        }

        private IWebDriver CreateWebDriver()
        {
            return new ChromeDriver()
            {
            };
        }

        public void Dispose()
        {
            webDriver.Value.Quit();
        }

        public IWebDriver WebDriver
        {
            get
            {
                return webDriver.Value;
            }
        }
    }
}
