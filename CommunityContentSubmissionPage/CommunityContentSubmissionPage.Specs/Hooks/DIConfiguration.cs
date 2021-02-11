using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Business.Infrastructure;
using Esprima.Ast;
using PlaywrightSharp;
using PlaywrightSharp.Chromium;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.Specs.Hooks
{
    [Binding]
    public class DIConfiguration
    {
        private readonly ScenarioContext _scenarioContext;

        public DIConfiguration(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario(Order = 0)]
        public async Task RegisterDI()
        {
            _scenarioContext.ScenarioContainer.RegisterTypeAs<DatabaseContext, IDatabaseContext>();

            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(headless:false);
            var page = await browser.NewPageAsync();

            _scenarioContext.ScenarioContainer.RegisterInstanceAs(playwright);
            _scenarioContext.ScenarioContainer.RegisterInstanceAs(browser);
            _scenarioContext.ScenarioContainer.RegisterInstanceAs(page);
        }

        [AfterScenario()]
        public async Task Cleanup()
        {
            var page = _scenarioContext.ScenarioContainer.Resolve<IPage>();
            await page.CloseAsync();

            var browser = _scenarioContext.ScenarioContainer.Resolve<IChromiumBrowser>();
            await browser.DisposeAsync();

            var playwright = _scenarioContext.ScenarioContainer.Resolve<IPlaywright>();
            playwright.Dispose();

            var databaseContext = _scenarioContext.ScenarioContainer.Resolve<IDatabaseContext>();
            databaseContext.Database.EnsureDeleted();
        }
    }
}
