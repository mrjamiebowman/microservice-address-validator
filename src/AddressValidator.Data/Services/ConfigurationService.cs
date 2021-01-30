using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressValidator.Data.Configuration;
using AddressValidator.Data.Configuration.Metadata;
using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Configuration;
using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace AddressValidator.Data.Services
{
    public class ConfigurationService : IConfigurationService
    {
         readonly DefaultCompanyConfiguration _defaultCompanyConfig;
        private readonly CompaniesConfiguration _companiesConfiguration;

        public ConfigurationService(DefaultCompanyConfiguration defaultCompanyConfig, CompaniesConfiguration companiesConfiguration)
        {
            _companiesConfiguration = companiesConfiguration;
            _defaultCompanyConfig = defaultCompanyConfig;
        }

        public BaseApiConfiguration GetApiConfiguration(Tenant tenant, string config)
        {
            if (tenant?.CompanyId != null && tenant?.ApplicationId != null)
            {
                // multi-tenant
                CompanyConfiguration company =
                    _companiesConfiguration.Configuration
                        .SingleOrDefault(x => x.Key == tenant.CompanyId.Value.ToString()).Value;

                // look up application
                ApplicationConfiguration app = company.Applications
                    .SingleOrDefault(x => x.Key == tenant.ApplicationId.Value.ToString()).Value;

                BaseApiConfiguration appApiConfig = config switch
                {
                    SmartyStreetsConfiguration.Position => app.SmartyStreets,
                    UspsConfiguration.Position => app.USPS,
                    _ => throw new NotImplementedException(
                        $"Could not find the API configuration ({config}) for Company/Application ({tenant.CompanyId.Value}, {tenant.ApplicationId.Value})."),
                };

                return appApiConfig;
            }

            // default 
            BaseApiConfiguration apiConfig = config switch
            {
                SmartyStreetsConfiguration.Position => _defaultCompanyConfig.SmartyStreets,
                UspsConfiguration.Position => _defaultCompanyConfig.USPS,
                _ => throw new NotImplementedException(
                    $"Could not find the API configuration ({config}) for Default Company."),
            };

            return apiConfig;
        }

        public IEnumerable<ConfigurationExpiration> GetConfigurationExpirations(ConfigurationTypeEnum configurationType, int days = 30)
        {
            var apiconfig = new List<ConfigurationExpiration>();

            // HACK
            days = 90;

            Func<string, string, string, DateTime, ConfigurationExpiration> createConfigExp = (string companyName, string appName, string apiName, DateTime expirationDate) =>
            {
                var configExp = new ConfigurationExpiration();
                configExp.CompanyName = companyName;
                configExp.AppName = appName;
                configExp.ApiName = apiName;

                // expires in days
                int expiresInDays = Convert.ToInt16((expirationDate - DateTime.UtcNow).TotalDays);
                configExp.ExpiresInDays = expiresInDays;

                if (expiresInDays >= days)
                {
                    // good
                    configExp.IsExpired = false;
                }
                else if (expiresInDays <= days && expirationDate > DateTime.UtcNow)
                {
                    // close to expiring
                    configExp.IsExpired = false;
                    configExp.IsExpiring = true;
                }
                else
                {
                    // expired
                    configExp.ExpiresInDays = 0; // so it's not a negative number...
                    configExp.IsExpired = true;
                    configExp.IsExpiring = false;
                }

                return configExp;
            };

            // validate default configuration
            if (configurationType == ConfigurationTypeEnum.Default)
            {
                if (_defaultCompanyConfig.SmartyStreets.Expiration != null)
                {
                    // smarty streets
                    ConfigurationExpiration config = createConfigExp("Default Company", null, SmartyStreetsConfiguration.Position, _defaultCompanyConfig.SmartyStreets.Expiration.Value);
                    apiconfig.Add(config);
                }

                if (_defaultCompanyConfig.USPS.Expiration != null)
                {
                    // usps
                    ConfigurationExpiration config = createConfigExp("Default Company", null, UspsConfiguration.Position, _defaultCompanyConfig.USPS.Expiration.Value);
                    apiconfig.Add(config);
                }
            } else if (configurationType == ConfigurationTypeEnum.Companies)
            {

            }

            return apiconfig;
        }
    }
}
