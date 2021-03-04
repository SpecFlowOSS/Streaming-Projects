using System;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.Specs.Drivers
{
    public class DBNameProvider
    {
        private Guid _databaseGuid;

        public DBNameProvider()
        {
            _databaseGuid = Guid.NewGuid();
        }

        public string GetDBName()
        {
            return _databaseGuid.ToString("N");
        }
    }
}