using System;
using System.Collections.Generic;
using System.Text;
using CommunityContentSubmissionPage.Business.Infrastructure;
using CommunityContentSubmissionPage.Specs.Support;
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
            _scenarioContext.ScenarioContainer.RegisterTypeAs<TestDatabaseNameProvider, IDatabaseNameProvider>();
            _scenarioContext.ScenarioContainer.RegisterTypeAs<DatabaseContext, IDatabaseContext>();
        }

        [AfterScenario()]
        public void Cleanup()
        {
            var databaseContext = _scenarioContext.ScenarioContainer.Resolve<IDatabaseContext>();
            databaseContext.Database.EnsureDeleted();
        }
    }
}
