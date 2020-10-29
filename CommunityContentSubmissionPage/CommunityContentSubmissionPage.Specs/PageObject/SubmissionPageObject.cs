using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityContentSubmissionPage.Specs.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        public InputEntryPageObject UrlInputEntry => TryGetInputEntry("url");

        public InputEntryPageObject TypeSelectEntry => TryGetInputEntry("type");

        public InputEntryPageObject EmailInputEntry => TryGetInputEntry("email");
        public InputEntryPageObject DescriptionInputEntry => TryGetInputEntry("description");

        public InputEntryPageObject PrivacyPolicyInputEntry => TryGetInputEntry("privacypolicy");

        public IWebElement UrlWebElement => UrlInputEntry.ValueWebElement;

        public IWebElement TypeWebElement => TypeSelectEntry.ValueWebElement;

        public IWebElement EmailWebElement => EmailInputEntry.ValueWebElement;

        public IWebElement DescriptionWebElement => DescriptionInputEntry.ValueWebElement;

        public string EmailLabel => EmailInputEntry.LabelWebElement.Text;

        public string UrlLabel => UrlInputEntry.LabelWebElement.Text;

        public string TypeLabel => TypeSelectEntry.LabelWebElement.Text;
        public string DescriptionLabel => DescriptionInputEntry.LabelWebElement.Text;

        public string Url
        {
            get
            {
                return UrlInputEntry.ValueWebElement.Text;
            }
            set
            {
                UrlInputEntry.ValueWebElement.Clear();
                UrlInputEntry.ValueWebElement.SendKeys(value);
            }
        }

        public string Type
        {
            get
            {
                var selectElement = new SelectElement(TypeSelectEntry.ValueWebElement);

                return selectElement.SelectedOption.Text;
            }
            set
            {
                var selectElement = new SelectElement(TypeSelectEntry.ValueWebElement);
                selectElement.SelectByText(value);
            }
        }

        public string Email {
            get
            {
                return EmailInputEntry.ValueWebElement.Text;
            }
            set
            {
                EmailInputEntry.ValueWebElement.Clear();
                EmailInputEntry.ValueWebElement.SendKeys(value);
            }
        }

        public string Description
        {
            get
            {
                return DescriptionInputEntry.ValueWebElement.Text;
            }
            set
            {
                DescriptionInputEntry.ValueWebElement.Clear();
                DescriptionInputEntry.ValueWebElement.SendKeys(value);
            }
        }


        private InputEntryPageObject TryGetInputEntry(string id)
        {
            var inputEntryPageObject = InputEntries.Where(i => i.Id == id).SingleOrDefault();

            if (inputEntryPageObject is null)
            {
                throw new NotFoundException($"Input Entry for '{id}' was not found");
            }

            return inputEntryPageObject;
        }

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