using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Business.Infrastructure;
using CommunityContentSubmissionPage.Business.Model;

namespace CommunityContentSubmissionPage.Business.Logic
{
    public interface ISubmissionSaver
    {
        void Save(SubmissionEntry submissionEntry);
    }

    public class SubmissionSaver : ISubmissionSaver
    {
        private readonly IDatabaseContext _databaseContext;

        public SubmissionSaver(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Save(SubmissionEntry submissionEntry)
        {
            _databaseContext.SubmissionEntries.Add(submissionEntry);
            _databaseContext.SaveChanges();
        }
    }
}
