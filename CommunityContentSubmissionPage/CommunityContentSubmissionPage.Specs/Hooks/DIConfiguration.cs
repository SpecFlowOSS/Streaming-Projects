using System;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Business.Infrastructure;
using CommunityContentSubmissionPage.Specs.Drivers;
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
        private static IBrowser? _browser;

        public DIConfiguration(ScenarioContext scenarioContext, DBNameProvider dbNameProvider)
        {
            _scenarioContext = scenarioContext;
            _dbNameProvider = dbNameProvider;
        }

        [BeforeTestRun]
        public static async Task BeforeTestRun(TargetDriver targetDriver)
        {
            _playwright = await Playwright.CreateAsync();

            var usedBrowser = targetDriver.GetCurrentTarget().KeyValues["Browser"];

            switch (usedBrowser)
            {
                case "Chrome":
                    _browser = await _playwright.Chromium.LaunchAsync(headless: false);
                    break;
                case "Edge":
                    _browser = await _playwright.Chromium.LaunchAsync(headless: false, executablePath: "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe");
                    break;
                case "Firefox":
                    _browser = await _playwright.Firefox.LaunchAsync(headless: false);
                    break;
                default:
                    throw new NotSupportedException($"The browser {usedBrowser} isn't supported");
                    break;
                
            }

            
        }

        [BeforeScenario(Order = 0)]
        public async Task RegisterDI()
        {
            _scenarioContext.ScenarioContainer.RegisterInstanceAs<IDatabaseContext>(new DatabaseContext(_dbNameProvider.GetDBName()));

            IBrowserContext? browserContext = await _browser.NewContextAsync(new BrowserContextOptions());

            var page = await browserContext.NewPageAsync();

            _scenarioContext.ScenarioContainer.RegisterInstanceAs(browserContext);
            _scenarioContext.ScenarioContainer.RegisterInstanceAs(page);
        }

        [AfterScenario()]
        public async Task Cleanup()
        {
            var page = _scenarioContext.ScenarioContainer.Resolve<IPage>();
            await page.CloseAsync();

            var context = _scenarioContext.ScenarioContainer.Resolve<IBrowserContext>();
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
