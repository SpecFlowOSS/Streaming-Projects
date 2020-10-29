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

        public InputEntryPageObject UrlInputEntry => PageObjectHelper.TryGetInputEntry(InputEntries, "url");

        public InputEntryPageObject TypeSelectEntry => PageObjectHelper.TryGetInputEntry(InputEntries, "type");

        public InputEntryPageObject EmailInputEntry => PageObjectHelper.TryGetInputEntry(InputEntries, "email");
        public InputEntryPageObject DescriptionInputEntry => PageObjectHelper.TryGetInputEntry(InputEntries, "description");

        public InputEntryPageObject PrivacyPolicyInputEntry => PageObjectHelper.TryGetInputEntry(InputEntries, "privacypolicy");

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
            get => UrlInputEntry.ValueWebElement.Text;
            set => PageObjectHelper.SetTextInput(UrlInputEntry, value);
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
            get => EmailInputEntry.ValueWebElement.Text;
            set => PageObjectHelper.SetTextInput(EmailInputEntry, value);
        }

        
        public string Description
        {
            get => DescriptionInputEntry.ValueWebElement.Text;
            set => PageObjectHelper.SetTextInput(DescriptionInputEntry, value);
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