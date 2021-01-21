using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
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
            runtimePluginEvents.CustomizeGlobalDependencies += RuntimePluginEvents_CustomizeGlobalDependencies;
        }

        private void RuntimePluginEvents_CustomizeGlobalDependencies(object sender, CustomizeGlobalDependenciesEventArgs e)
        {
            var runtimePluginTestExecutionLifecycleEvents = e.ObjectContainer.Resolve<RuntimePluginTestExecutionLifecycleEvents>();

            runtimePluginTestExecutionLifecycleEvents.AfterStep += RuntimePluginTestExecutionLifecycleEvents_AfterStep;
        }

        private void RuntimePluginTestExecutionLifecycleEvents_AfterStep(object sender, RuntimePluginAfterStepEventArgs e)
        {
            var scenarioContext = e.ObjectContainer.Resolve<ScenarioContext>();

            if (scenarioContext.ScenarioExecutionStatus != ScenarioExecutionStatus.OK)
            {
                //we have an error in the step
            }
        }


    }
}
