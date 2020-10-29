using System.Collections.Generic;
using System.Linq;
using CommunityContentSubmissionPage.Specs.Drivers;
using OpenQA.Selenium;

namespace CommunityContentSubmissionPage.Specs.PageObject
{
    public class SubmissionPageObject
    {
        private readonly WebDriverDriver _webDriverDriver;

        public SubmissionPageObject(WebDriverDriver webDriverDriver)
        {
            _webDriverDriver = webDriverDriver;
        }

        protected IWebElement Form => _webDriverDriver.WebDriver.FindElement(By.TagName("form"));

        protected IWebElement SubmitButton => Form.FindElement(By.ClassName("btn-primary"));
        protected IWebElement ResetButton => Form.FindElement(By.ClassName("btn-secondary"));

        protected IEnumerable<InputEntryPageObject> InputEntries => Form.FindElements(By.ClassName("form-group")).Select(i => new InputEntryPageObject(i));

        public InputEntryPageObject UrlInputEntry => PageObjectHelper.TryGetInputEntry(InputEntries, "url");

        public InputEntryPageObject TypeSelectEntry => PageObjectHelper.TryGetInputEntry(InputEntries, "type");

        public InputEntryPageObject EmailInputEntry => PageObjectHelper.TryGetInputEntry(InputEntries, "email");

        public InputEntryPageObject DescriptionInputEntry => PageObjectHelper.TryGetInputEntry(InputEntries, "description");

        public InputEntryPageObject PrivacyPolicyInputEntry => PageObjectHelper.TryGetInputEntry(InputEntries, "privacypolicy");

        public void ClickSubmitButton()
        {
            SubmitButton.Click();
        }

        public void ClickResetButton()
        {
            ResetButton.Click();
        }
    }
}