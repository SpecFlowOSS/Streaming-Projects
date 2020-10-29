using OpenQA.Selenium;

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

        public IWebElement ValueWebElement
        {
            get
            {
                try
                {
                    return _parentDiv.FindElement(By.ClassName("form-control"));
                }
                catch (NoSuchElementException)
                {
                    return _parentDiv.FindElement(By.ClassName("form-check-input"));
                }
            }
        }
    }
}