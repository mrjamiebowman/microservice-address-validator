using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AddressValidator.Api.Extensions
{
    public static class AddCustomHealthChecksExtension
    {
        public static void AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<ExpiredApiConfigurationHealthCheck>("expired_api_creds_check");
        }
    }
}
