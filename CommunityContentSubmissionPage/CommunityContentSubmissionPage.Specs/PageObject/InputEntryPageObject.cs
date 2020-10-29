using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CommunityContentSubmissionPage.Specs.PageObject
{
    public class InputEntryPageObject
    {
        private readonly IWebElement _parentDiv;

        public InputEntryPageObject(IWebElement parentDiv)
        {
            _parentDiv = parentDiv;
        }

        public string Id => _parentDiv.GetAttribute("id");

        public IWebElement LabelWebElement => _parentDiv.FindElement(By.TagName("label"));

        public IWebElement? ValueWebElement
        {
            get
            {
                try
                {
                    return _parentDiv.FindElement(By.ClassName("form-control"));
                }
                catch (NoSuchElementException)
                {
                    try
                    {
                        return _parentDiv.FindElement(By.ClassName("form-check-input"));
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                }
            }
        }

        public string Label => LabelWebElement.Text;

        public string Value
        {
            get
            {
                if (ValueWebElement?.TagName == "select")
                {
                    var selectElement = new SelectElement(ValueWebElement);
                    return selectElement.SelectedOption.Text;
                }
                else
                {
                    return ValueWebElement?.Text ?? String.Empty;
                }
            }
            set
            {
                if (ValueWebElement?.TagName == "select")
                {
                    var selectElement = new SelectElement(ValueWebElement);
                    selectElement.SelectByText(value);
                }
                else
                {
                    ValueWebElement?.Clear();
                    ValueWebElement?.SendKeys(value);
                }
            }
        }
    }
}