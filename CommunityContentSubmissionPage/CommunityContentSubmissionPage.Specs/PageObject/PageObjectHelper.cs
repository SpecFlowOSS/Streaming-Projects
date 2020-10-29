using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace CommunityContentSubmissionPage.Specs.PageObject
{
    public class PageObjectHelper
    {
        public static InputEntryPageObject TryGetInputEntry(IEnumerable<InputEntryPageObject> inputEntryPageObjects,
            string id)
        {
            var inputEntryPageObject = inputEntryPageObjects.SingleOrDefault(i => i.Id == id);

            if (inputEntryPageObject is null)
            {
                throw new NotFoundException($"Input Entry for '{id}' was not found");
            }

            return inputEntryPageObject;
        }
    }
}