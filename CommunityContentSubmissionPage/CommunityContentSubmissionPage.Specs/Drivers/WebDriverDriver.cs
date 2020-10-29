using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CommunityContentSubmissionPage.Specs.Drivers
{
    public class WebDriverDriver : IDisposable
    {
        private readonly Lazy<IWebDriver> _webDriver;

        public WebDriverDriver()
        {
            _webDriver = new Lazy<IWebDriver>(() => CreateWebDriver());
        }

        public IWebDriver WebDriver => _webDriver.Value;

        public void Dispose()
        {
            _webDriver.Value.Quit();
        }

        private IWebDriver CreateWebDriver()
        {
            return new ChromeDriver();
        }
    }
}