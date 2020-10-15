using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityContentSubmissionPage.Business.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;

namespace CommunityContentSubmissionPage.Specs.Drivers
{
    public class SubmissionDriver
    {
        private readonly IDatabaseContext _databaseContext;

        public SubmissionDriver(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AssertOneSubmissionEntryExists()
        {
            _databaseContext.SubmissionEntries.Count().Should().BeGreaterThan(0);
        }

        public void AssertNumberOfEntriesStored(int expectedCountOfStoredEntries)
        {
            _databaseContext.SubmissionEntries.Count().Should().Be(expectedCountOfStoredEntries);
        }
    }
}
