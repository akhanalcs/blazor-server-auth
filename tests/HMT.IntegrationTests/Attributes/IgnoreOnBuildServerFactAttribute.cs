using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMT.IntegrationTests.Attributes
{
    // This is copied from: https://stackoverflow.com/a/69196697/8644294
    public class IgnoreOnBuildServerFactAttribute : FactAttribute
    {
        public IgnoreOnBuildServerFactAttribute()
        {
            if (IsRunningOnBuildServer())
            {
                Skip = "This integration test is skipped running in the build server as it involves launching an UI which requires build agents to be run as non-service. Run it locally!";
            }
        }
        /// <summary>
        /// Determine if the test is running on build server
        /// </summary>
        /// <returns>True if being executed in Build server, false otherwise.</returns>
        public static bool IsRunningOnBuildServer()
        {
            return bool.TryParse(Environment.GetEnvironmentVariable("IsRunningOnBuildServer"), out var buildServerFlag) ? buildServerFlag : false;
        }
    }
}
