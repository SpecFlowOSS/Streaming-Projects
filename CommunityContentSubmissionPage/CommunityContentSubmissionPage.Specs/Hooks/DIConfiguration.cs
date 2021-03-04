using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Business.Infrastructure;
using CommunityContentSubmissionPage.Specs.Drivers;
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
        private readonly DBNameProvider _dbNameProvider;
        
        private static IPlaywright? _playwright;
        private static IChromiumBrowser? _browser;

        public DIConfiguration(ScenarioContext scenarioContext, DBNameProvider dbNameProvider)
        {
            _scenarioContext = scenarioContext;
            _dbNameProvider = dbNameProvider;
        }

        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(headless: false);
        }

        [BeforeScenario(Order = 0)]
        public async Task RegisterDI()
        {
            _scenarioContext.ScenarioContainer.RegisterInstanceAs<IDatabaseContext>(new DatabaseContext(_dbNameProvider.GetDBName()));

            IChromiumBrowserContext? chromiumBrowserContext = await _browser.NewContextAsync(new BrowserContextOptions());

            var page = await chromiumBrowserContext.NewPageAsync();

            _scenarioContext.ScenarioContainer.RegisterInstanceAs(chromiumBrowserContext);
            _scenarioContext.ScenarioContainer.RegisterInstanceAs(page);
        }

        [AfterScenario()]
        public async Task Cleanup()
        {
            var page = _scenarioContext.ScenarioContainer.Resolve<IPage>();
            await page.CloseAsync();

            var context = _scenarioContext.ScenarioContainer.Resolve<IChromiumBrowserContext>();
            await context.DisposeAsync();

            
            var databaseContext = _scenarioContext.ScenarioContainer.Resolve<IDatabaseContext>();
            databaseContext.Database.EnsureDeleted();
        }

        [AfterTestRun]
        public static async Task AfterTestRun()
        {
            await _browser.DisposeAsync();

            _playwright.Dispose();
        }
    }
}
