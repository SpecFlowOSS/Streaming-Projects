using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.Specs.Drivers
{
    public class DBNameProvider
    {
        private readonly ScenarioContext _scenarioContext;

        public DBNameProvider(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public string GetDBName()
        {
            return _scenarioContext.ScenarioInfo.Title.Replace(" ", "");
        }
    }
}