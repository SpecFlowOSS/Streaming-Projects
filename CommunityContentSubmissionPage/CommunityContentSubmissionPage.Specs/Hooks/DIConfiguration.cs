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

        public DIConfiguration(ScenarioContext scenarioContext, DBNameProvider dbNameProvider)
        {
            _scenarioContext = scenarioContext;
            _dbNameProvider = dbNameProvider;
        }

        [BeforeScenario(Order = 0)]
        public async Task RegisterDI()
        {
            _scenarioContext.ScenarioContainer.RegisterInstanceAs<IDatabaseContext>(new DatabaseContext(_dbNameProvider.GetDBName()));

            var playwright = await Playwright.CreateAsync();

            

            var browser = await playwright.Chromium.LaunchAsync(headless:false);
            
            IChromiumBrowserContext? chromiumBrowserContext = await browser.NewContextAsync(new BrowserContextOptions());

            var page = await chromiumBrowserContext.NewPageAsync();


            _scenarioContext.ScenarioContainer.RegisterInstanceAs(playwright);
            _scenarioContext.ScenarioContainer.RegisterInstanceAs(browser);
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

            var browser = _scenarioContext.ScenarioContainer.Resolve<IChromiumBrowser>();
            await browser.DisposeAsync();

            var playwright = _scenarioContext.ScenarioContainer.Resolve<IPlaywright>();
            playwright.Dispose();

            var databaseContext = _scenarioContext.ScenarioContainer.Resolve<IDatabaseContext>();
            databaseContext.Database.EnsureDeleted();
        }
    }
}
