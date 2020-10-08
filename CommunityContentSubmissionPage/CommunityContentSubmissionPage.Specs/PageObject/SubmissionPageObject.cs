using System;
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

        public IWebElement Form => webDriverDriver.WebDriver.FindElement(By.TagName("form"));

        public IEnumerable<InputEntryPageObject> InputEntries => Form.FindElements(By.ClassName("form-group")).Select(i => new InputEntryPageObject(i));

        public InputEntryPageObject UrlInputEntry => TryGetInputEntry("url");

        public InputEntryPageObject TypeInputEntry => TryGetInputEntry("type");

        public IWebElement UrlWebElement => UrlInputEntry.ValueWebElement;

        public IWebElement UrlLabelWebElement => UrlInputEntry.LabelWebElement;

        public IWebElement TypeWebElement => TypeInputEntry.ValueWebElement;

        public IWebElement TypeLabelWebElement => TypeInputEntry.LabelWebElement;

        public string UrlLabel => UrlLabelWebElement.Text;

        public string TypeLabel => TypeLabelWebElement.Text;

        private InputEntryPageObject TryGetInputEntry(string id)
        {
            var inputEntryPageObject = InputEntries.Where(i => i.Id == id).SingleOrDefault();

            if (inputEntryPageObject is null)
            {
                throw new NotFoundException($"Input Entry for '{id}' was not found");
            }

            return inputEntryPageObject;
        }
    }
}