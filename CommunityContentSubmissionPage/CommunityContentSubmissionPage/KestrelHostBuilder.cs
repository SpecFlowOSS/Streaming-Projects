using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CommunityContentSubmissionPage
{
    public class KestrelHostBuilder
    {
        public IHostBuilder CreateHostBuilder(string[] args, string hostname = null, string webRoot = null) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    if (hostname is not null)
                    {
                        webBuilder.UseUrls(hostname);
                    }

                    if (webRoot is not null)
                    {
                        webBuilder.UseWebRoot(webRoot);
                    }
                });
    }
}
