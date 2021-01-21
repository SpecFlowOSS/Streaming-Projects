using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Generator.Plugins;
using TechTalk.SpecFlow.Generator.UnitTestConverter;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: GeneratorPlugin(typeof(StreamingGeneratorPlugin.SpecFlowPlugin.StreamingGeneratorPlugin))]

namespace StreamingGeneratorPlugin.SpecFlowPlugin
{
    class StreamingGeneratorPlugin : IGeneratorPlugin
    {
        public void Initialize(GeneratorPluginEvents generatorPluginEvents, GeneratorPluginParameters generatorPluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            generatorPluginEvents.RegisterDependencies += RegisterDependencies;
        }

        private void RegisterDependencies(object sender, RegisterDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<StreamingDecorator, ITestMethodDecorator>("StreamingDecorator");
        }
    }
}
