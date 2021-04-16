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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("CommunityContent_ConnectionString"));
            //optionsBuilder.UseInMemoryDatabase("CommunitySubmissions");
        }

        public DbSet<SubmissionEntry> SubmissionEntries { get; set; }
        
    }
}
