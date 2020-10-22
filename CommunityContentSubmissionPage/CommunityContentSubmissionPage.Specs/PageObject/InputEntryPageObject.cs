using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace CommunityContentSubmissionPage.Specs.PageObject
{
    public class InputEntryPageObject
    {
        private readonly IWebElement parentDiv;

        public InputEntryPageObject(IWebElement parentDiv)
        {
            this.parentDiv = parentDiv;
        }

        public string Id => parentDiv.GetAttribute("id");

        public IWebElement LabelWebElement => parentDiv.FindElement(By.TagName("label"));
        public IWebElement ValueWebElement => parentDiv.FindElement(By.ClassName("form-control"));
    }
}
