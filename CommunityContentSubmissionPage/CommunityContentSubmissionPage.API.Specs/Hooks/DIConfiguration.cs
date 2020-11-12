using CommunityContentSubmissionPage.Business.Infrastructure;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.API.Specs.Hooks
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
        }

        [AfterScenario()]
        public void Cleanup()
        {
            var databaseContext = _scenarioContext.ScenarioContainer.Resolve<IDatabaseContext>();
            databaseContext.Database.EnsureDeleted();
        }
    }
}
