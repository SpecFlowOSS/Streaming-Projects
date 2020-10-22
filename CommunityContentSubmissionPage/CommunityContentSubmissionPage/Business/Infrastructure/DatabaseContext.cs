using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Business.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CommunityContentSubmissionPage.Business.Infrastructure
{
    public interface IDatabaseContext
    {
        DbSet<SubmissionEntry> SubmissionEntries { get; set; }

        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default);
    }

    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly IDatabaseNameProvider _databaseNameProvider;

        public DatabaseContext(IDatabaseNameProvider databaseNameProvider)
        {
            _databaseNameProvider = databaseNameProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_databaseNameProvider.GetDatabaseName());

            Database.EnsureCreated();
        }

        public DbSet<SubmissionEntry> SubmissionEntries { get; set; }
        
    }

    public interface IDatabaseNameProvider
    {
        string GetDatabaseName();
    }

    class DatabaseNameProvider : IDatabaseNameProvider
    {
        public string GetDatabaseName()
        {
            return "CommunitySubmissions";
        }
    }
}
