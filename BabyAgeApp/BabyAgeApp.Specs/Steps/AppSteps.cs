using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BabyAgeApp.Specs.Steps
{
    public class AppSteps
    {
        [Then(@"the app was started")]
        public void ThenTheAppWasStarted()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
