using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.API.Specs.Drivers
{
    public class WebServerDriver
    {
        private readonly ScenarioContext _scenarioContext;
        private IHost? _host;

        public WebServerDriver(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            Hostname = $"http://localhost:{GeneratePort()}";
        }

        public string Hostname { get; }

        public void Start()
        {
            string location = typeof(KestrelHostBuilder).Assembly.Location;
            var applicationAssemblyPath = Path.GetDirectoryName(location);

            if (applicationAssemblyPath is null)
                throw new Exception("Location of application assembly could not be found");

            string webRoot = Path.Combine(applicationAssemblyPath, "..", "..", "..", "..",
                "CommunityContentSubmissionPage", "wwwroot");

            var hostBuilder = new KestrelHostBuilder();
            var builder = hostBuilder.CreateHostBuilder(new string[] { }, Hostname, webRoot, _scenarioContext.ScenarioInfo.Title);
            _host = builder.Build();
            _host.StartAsync().ConfigureAwait(false);
        }


        public async Task Stop()
        {
            if (_host is not null) await _host.StopAsync();
        }

        private int GeneratePort()
        {
            return new Random().Next(5000, 32000);
        }
    }
}