﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityContentSubmissionPage.Specs.Drivers;
using OpenQA.Selenium;

namespace CommunityContentSubmissionPage.Specs.PageObject
{
    public class SubmissionPageObject
    {
        private WebDriverDriver webDriverDriver;

        public SubmissionPageObject(WebDriverDriver webDriverDriver)
        {
            this.webDriverDriver = webDriverDriver;
        }

        protected IWebElement Form => webDriverDriver.WebDriver.FindElement(By.TagName("form"));

        protected IWebElement SubmitButton => Form.FindElement(By.ClassName("btn-primary"));

        protected IEnumerable<InputEntryPageObject> InputEntries => Form.FindElements(By.ClassName("form-group")).Select(i => new InputEntryPageObject(i));

        public InputEntryPageObject UrlInputEntry => TryGetInputEntry("url");

        public InputEntryPageObject TypeInputEntry => TryGetInputEntry("type");

        public InputEntryPageObject EmailInputEntry => TryGetInputEntry("email");
        public InputEntryPageObject DescriptionInputEntry => TryGetInputEntry("description");

        public IWebElement UrlWebElement => UrlInputEntry.ValueWebElement;

        public IWebElement TypeWebElement => TypeInputEntry.ValueWebElement;

        public IWebElement EmailWebElement => EmailInputEntry.ValueWebElement;

        public IWebElement DescriptionWebElement => DescriptionInputEntry.ValueWebElement;

        public string EmailLabel => EmailInputEntry.LabelWebElement.Text;

        public string UrlLabel => UrlInputEntry.LabelWebElement.Text;

        public string TypeLabel => TypeInputEntry.LabelWebElement.Text;
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
                return TypeInputEntry.ValueWebElement.Text;
            }
            set
            {
                TypeInputEntry.ValueWebElement.Clear();
                TypeInputEntry.ValueWebElement.SendKeys(value);
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
    }
}