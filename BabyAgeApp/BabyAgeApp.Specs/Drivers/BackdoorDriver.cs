using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyAgeApp.Specs.Drivers
{
    public class BackdoorDriver
    {
        private readonly AppiumDriver _appiumDriver;

        public BackdoorDriver(AppiumDriver appiumDriver)
        {
            _appiumDriver = appiumDriver;
        }

        public void SetBirthday(DateTime dateTime)
        {
            ExecuteBackdoor( "SetBirthday", dateTime.ToString("O"));
        }

        private void ExecuteBackdoor(string methodName, string value)
        {
            var args = CreateBackdoorArgs(methodName, value);

            _appiumDriver.Driver.ExecuteScript("mobile: backdoor", args);
        }

        public void SetNow(DateTime today)
        {
            ExecuteBackdoor("SetNow", today.ToString("O"));
        }

        private static Dictionary<string, object> CreateBackdoorArgs(string methodName, string value)
        {
            return new Dictionary<string, object>
            {
                {"target", "application"},
                {
                    "methods", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            {"name", methodName},
                            {
                                "args", new List<Dictionary<string, object>>
                                {
                                    new Dictionary<string, object>
                                    {
                                        {"value", value},
                                        {"type", "String"}
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
