using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using CommunityContentSubmissionPage.Database;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Common;
using Ductus.FluentDocker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Polly;
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
                .Build()
                .Start();

            WaitForWebserver();
        }

        private static void WaitForWebserver()
        {
            var restclient = GetRestClient();

            var policy = Policy.HandleResult<bool>(r => !r)
                .WaitAndRetry(10, _ => TimeSpan.FromSeconds(10));

            policy.Execute(() =>
            {
                var restRequest = new RestRequest("/api/AvailableTypes", DataFormat.Json);

                var restResponse = restclient.Get(restRequest);

                return restResponse.IsSuccessful;
            });

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
            var restClient = GetRestClient();
            _scenarioContext.ScenarioContainer.RegisterInstanceAs(restClient);
        }


        [BeforeScenario()]
        [AfterScenario()]
        public void ResetDatabase()
        {
            var config = LoadConfiguration();
            var connectionString = config.GetConnectionString("db");

            var databaseContext = new DatabaseContext(){ConnectionString = connectionString};


            databaseContext.RemoveRange(databaseContext.SubmissionEntries);
            databaseContext.SaveChanges();
        }

        private static RestClient GetRestClient()
        {
            var config = LoadConfiguration();
            var baseAddress = config["Product.Api:BaseAddress"];
            var restClient = new RestClient(baseAddress);
            return restClient;
        }
    }
}
