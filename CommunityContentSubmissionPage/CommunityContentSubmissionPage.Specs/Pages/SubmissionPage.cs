using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace CommunityContentSubmissionPage.Specs.Pages
{
    public static class SubmissionPage
    {
        public static IWebLocator UrlInputField => L("URL Input field", By.Id("txtUrl"));
        public static IWebLocator UrlLabel => L("URL Label", By.CssSelector("#url label"));
    }
}
