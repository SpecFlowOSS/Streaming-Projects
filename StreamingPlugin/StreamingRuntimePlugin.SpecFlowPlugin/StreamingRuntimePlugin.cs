using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(StreamingRuntimePlugin.SpecFlowPlugin.StreamingRuntimePlugin))]

namespace StreamingRuntimePlugin.SpecFlowPlugin
{
    public class StreamingRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            
        }
    }
}
