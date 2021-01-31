using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AddressValidator.Data.Extensions;

namespace AddressValidator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration(config =>
                    {
                        config.SetBasePath(Directory.GetCurrentDirectory());

                        var featConfigProvider = Environment.GetEnvironmentVariable("FEATURE_CONFIG_PROVIDER");

                        if (String.Equals(featConfigProvider, "Azure", StringComparison.CurrentCultureIgnoreCase))
                        {
                            config.AddCustomAzureAppConfig();
                        }
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
