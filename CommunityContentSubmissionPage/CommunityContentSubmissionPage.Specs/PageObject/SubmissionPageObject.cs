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

        private IWebElement UrlElement => webDriverDriver.WebDriver.FindElement(By.Id("url"));
        private IWebElement TypeElement => webDriverDriver.WebDriver.FindElement(By.Id("type"));

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

        public IWebElement TypeWebElement
        {
            get
            {
                try
                {
                    return TypeElement.FindElement(By.Id("txtType"));
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }
        }

        public IWebElement TypeLabelWebElement
        {
            get
            {
                try
                {
                    return TypeElement.FindElement(By.Id("label"));
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }
        }

        public string Url => UrlWebElement.Text;
        public string UrlLabel => UrlLabelWebElement.Text;

        public string TypeLabel => TypeLabelWebElement.Text;


    }
}