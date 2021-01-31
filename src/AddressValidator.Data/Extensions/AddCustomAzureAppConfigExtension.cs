using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;

namespace AddressValidator.Data.Extensions
{
    public static class AddCustomAzureAppConfigExtension
    {
        public static void AddCustomAzureAppConfig(this IConfigurationBuilder config)
        {
            // environment variables
            var appConnStr = Environment.GetEnvironmentVariable("AZ_APPCONFIG_CONNECTION_STRING");
            var labelFilter = Environment.GetEnvironmentVariable("AZ_APPCONFIG_LABEL_FILTER");

            // validate configuration
            StringBuilder error = new StringBuilder();

            if (String.IsNullOrWhiteSpace(appConnStr))
            {
                error.Append("Connection String (AZ_APPCONFIG_CONNECTION_STRING) is missing! \n");
            }
            if (String.IsNullOrWhiteSpace(labelFilter))
            {
                error.Append("Label Filter (AZ_APPCONFIG_LABEL_FILTER) is missing! \n");
            }

            if (error.Length > 0)
            {
                // throw error
                error.Insert(0, "Azure App Configuration is not configured correctly. \n");
                throw new Exception(error.ToString());
            }

            config.AddAzureAppConfiguration(o =>
            {
                // label filter
                o.Select(KeyFilter.Any, labelFilter);

                // key vault
                o.Connect(appConnStr).ConfigureKeyVault(kv =>
                {
                    kv.SetCredential(new DefaultAzureCredential());
                });
            });
        }
    }
}
