using System.Linq;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
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
                    .GetConfigurationExpirations(ConfigurationTypeEnum.Default, 30)
                    .ToList();

            // check companies/applications configuration
            if (defaultConfigExps.Any(x => x.IsExpiring || defaultConfigExps.Any(x => x.IsExpired)))
            {
                healthCheckResultHealthy = false;
            }

            if (healthCheckResultHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("A healthy result."));
            }

            return Task.FromResult(HealthCheckResult.Degraded("A degraded result."));
        }
    }
}
