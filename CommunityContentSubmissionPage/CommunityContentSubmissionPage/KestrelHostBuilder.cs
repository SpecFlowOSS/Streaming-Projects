using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityContentSubmissionPage.Business.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CommunityContentSubmissionPage
{
    public class KestrelHostBuilder
    {
        public IHostBuilder CreateHostBuilder(string[] args, string hostname = null, string webRoot = null, IDatabaseNameProvider databaseNameProvider = null) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    if (hostname != null)
                    {
                        webBuilder.UseUrls(hostname);
                    }

                    if (webRoot != null)
                    {
                        webBuilder.UseWebRoot(webRoot);
                    }
                }).ConfigureServices(serviceCollection =>
                {
                    if (databaseNameProvider != null)
                    {
                        serviceCollection.Replace(new ServiceDescriptor(typeof(IDatabaseNameProvider),
                            databaseNameProvider));
                    }
                });
    }
}
