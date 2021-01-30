using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using AddressValidator.Data.Configuration.Metadata;
using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services.Interfaces;

namespace AddressValidator.Api.Health
{
    public class ExpiredApiConfigurationHealthCheck : IHealthCheck
    {
        private readonly IConfigurationService _configurationService;

        public ExpiredApiConfigurationHealthCheck(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        /// <summary>
        /// Expired API creds check (healthy or degraded)
        /// This only works with json file configuration.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,  CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthCheckResultHealthy = true;

            // check default configuration
            var defaultConfigExps =
                _configurationService
                    .GetConfigurationExpirations(ConfigurationTypeEnum.Default, 30);

            // check companies/applications configuration
            if (defaultConfigExps.Any(x => x.Value.Expiring || defaultConfigExps.Any(x => x.Value.Expired)))
            {
                healthCheckResultHealthy = false;
            }


            // check default configuration
            var multiTenancyExps =
                _configurationService
                    .GetConfigurationExpirations(ConfigurationTypeEnum.Companies, 30);

            // check companies/applications configuration
            if (multiTenancyExps.Any(x => x.Value.Expiring || multiTenancyExps.Any(x => x.Value.Expired)))
            {
                healthCheckResultHealthy = false;
            }


            var allExpData = new Dictionary<string, ConfigurationExpiration>();
            defaultConfigExps.ToList().ForEach(x => allExpData.Add(x.Key, x.Value));
            multiTenancyExps.ToList().ForEach(x => allExpData.Add(x.Key, x.Value));

            var healthReportData = (IReadOnlyDictionary<string, object>)allExpData.ToImmutableDictionary(pair => pair.Key, pair => (object)pair.Value);

            if (healthCheckResultHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("A healthy result.", healthReportData));
            }

            return Task.FromResult(HealthCheckResult.Degraded("A degraded result.", null, healthReportData));
        }
    }
}
