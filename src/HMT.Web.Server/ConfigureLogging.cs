using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace HMT.Web.Server
{
    public static class ConfigureLogging
    {
        public static void ConfigureLogger(IConfiguration configuration)
        {
            //All the configuration is being read from appsettings.json file.
            Log.Logger = new LoggerConfiguration()
                                    .ReadFrom.Configuration(configuration)
                                    .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
        }

        public static IHostBuilder UseSerilogLoggingSetup(this IHostBuilder builder)
        {
            return builder.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(context.Configuration);
                Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
            });
        }
    }
}