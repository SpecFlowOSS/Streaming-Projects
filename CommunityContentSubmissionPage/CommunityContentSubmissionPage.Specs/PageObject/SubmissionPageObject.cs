using System;
using System.Collections.Generic;
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

        private IWebElement UrlElement
        {
            get
            {
                return webDriverDriver.WebDriver.FindElement(By.Id("url"));
            }
        }

        public IWebElement UrlWebElement
        {
            get
            {
                try
                {
                    return UrlElement.FindElement(By.Id("txtUrl"));
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }
        }
        public string Url
        {
            get { return UrlWebElement.Text; }
        }

        public IWebElement UrlLabelWebElement
        {
            get
            {
                try
                {
                    return UrlElement.FindElement(By.Id("label"));
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }
        }

        public string UrlLabel
        {
            get
            {
                return UrlLabelWebElement.Text;
            }
        }
    }
}