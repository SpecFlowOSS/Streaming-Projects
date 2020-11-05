using System;
using System.Collections.Generic;
using System.Text;
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using CommunityContentSubmissionPage.Business.Infrastructure;
using OpenQA.Selenium.Chrome;
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
        public void RegisterDI()
        {
            _scenarioContext.ScenarioContainer.RegisterTypeAs<DatabaseContext, IDatabaseContext>();

            var actor = new Actor("Chrome", new ConsoleLogger());
            actor.Can(BrowseTheWeb.With(new ChromeDriver()));

            _scenarioContext.ScenarioContainer.RegisterInstanceAs(actor);
        }

        [AfterScenario()]
        public void Cleanup()
        {
            var actor = _scenarioContext.ScenarioContainer.Resolve<Actor>();
            actor.AttemptsTo(QuitWebDriver.ForBrowser());

            var databaseContext = _scenarioContext.ScenarioContainer.Resolve<IDatabaseContext>();
            databaseContext.Database.EnsureDeleted();
        }
    }
}
