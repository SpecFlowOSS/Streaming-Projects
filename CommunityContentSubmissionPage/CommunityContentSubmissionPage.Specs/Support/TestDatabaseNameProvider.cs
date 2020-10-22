using System;
using CommunityContentSubmissionPage.Business.Infrastructure;

namespace CommunityContentSubmissionPage.Specs.Support
{
    class TestDatabaseNameProvider : IDatabaseNameProvider
    {
        private readonly Guid _databaseId;

        public TestDatabaseNameProvider()
        {
            _databaseId = Guid.NewGuid();
        }

        public string GetDatabaseName()
        {
            return $"CommunityContributions{_databaseId}";
        }
    }
}
