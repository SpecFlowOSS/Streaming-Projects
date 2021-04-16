using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using RestSharp;
using TechTalk.SpecFlow;

namespace CommunityContentSubmissionPage.API.Specs.Hooks
{
    [Binding]
    public class DIConfiguration
    {
        private readonly ScenarioContext _scenarioContext;

        public DIConfiguration(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        private static ICompositeService? _compositeService;

        [BeforeTestRun]
        public static void DockerComposeUp()
        {
            var dockerComposeFileName = FindDockerComposeFile();

            _compositeService = new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(dockerComposeFileName)
                .RemoveAllImages()
                .ForceRecreate()
                .RemoveOrphans()
                .WaitForPort("db", "42069")
                .WaitForHttp("webapi", "http://localhost:6969/api/AvailableTypes",
                    continuation: (response, _) => response.Code != HttpStatusCode.OK ? 2000 : 0)
                .Build()
                .Start();
        }

        [AfterTestRun]
        public static void DockerComposeDown()
        {
            _compositeService?.Stop();
            _compositeService?.Dispose();
        }

        private static string FindDockerComposeFile()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var root = Path.Combine(currentDirectory, "..", "..", "..", "..", "..");

            return Directory.EnumerateFiles(root, "docker-compose.yml", SearchOption.AllDirectories).First();
        }


        private static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build();
        }

        [BeforeScenario()]
        public void RegisterRestClient()
        {
            var config = LoadConfiguration();
            var baseAddress = config["Product.Api:BaseAddress"];
            _scenarioContext.ScenarioContainer.RegisterInstanceAs(new RestClient(baseAddress));
        }


    }
}
