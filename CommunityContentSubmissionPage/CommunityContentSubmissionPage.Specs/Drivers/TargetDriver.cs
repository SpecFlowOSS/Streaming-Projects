using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Configuration;

namespace CommunityContentSubmissionPage.Specs.Drivers
{

    public class Target
    {
        public string Name { get; set; } = string.Empty;
        public Dictionary<string, string> KeyValues { get; set; } = new Dictionary<string, string>();
    }

    public class TargetDriver
    {
        private readonly ISpecFlowJsonLocator _specFlowJsonLocator;

        public TargetDriver(ISpecFlowJsonLocator specFlowJsonLocator)
        {
            _specFlowJsonLocator = specFlowJsonLocator;
        }


        public Target GetCurrentTarget()
        {
            var specflowJson = _specFlowJsonLocator.GetSpecFlowJsonFilePath();
            using var doc = JsonDocument.Parse(File.ReadAllText(specflowJson));

            string? currentTarget = GetCurrentTarget(doc);
            

            return GetTarget(doc, currentTarget);
        }

        private static Target GetTarget(JsonDocument doc, string currentTarget)
        {
            string targetName = string.Empty;
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            foreach (var jsonProperty in doc.RootElement.EnumerateObject())
            {
                if (jsonProperty.NameEquals("targets"))
                {
                    foreach (var targetElement in jsonProperty.Value.EnumerateArray())
                    {
                        foreach (var targetProperties in targetElement.EnumerateObject())
                        {
                            if (targetProperties.Name != currentTarget)
                            {
                                continue;
                            }

                            targetName = targetProperties.Name;

                            foreach (var targetProperty in targetProperties.Value.EnumerateObject())
                            {
                                keyValues[targetProperty.Name] = targetProperty.Value.GetString() ?? String.Empty;
                            }
                        }
                    }
                }
            }

            var target = new Target()
            {
                Name = targetName
            };
            target.KeyValues = keyValues;

            return target;
        }

        private static string? GetCurrentTarget(JsonDocument doc)
        {
            var specflowTarget = Environment.GetEnvironmentVariable("SPECFLOW_TARGET");
            if (specflowTarget is not null)
            {
                return specflowTarget;
            }
            
            foreach (var jsonProperty in doc.RootElement.EnumerateObject())
            {
                if (jsonProperty.NameEquals("currentTarget"))
                {
                    return jsonProperty.Value.GetString() ?? string.Empty;
                    
                }
            }

            return null;
        }
    }
}
