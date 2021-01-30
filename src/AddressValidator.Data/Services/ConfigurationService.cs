using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Dictionary<string, ConfigurationExpiration> GetConfigurationExpirations(ConfigurationTypeEnum configurationType, int days = 30)
        {
            var apiconfig = new Dictionary<string, ConfigurationExpiration>();

            Func<ConfigurationTypeEnum, string, string, string, DateTime, ConfigurationExpiration> createConfigExp = (ConfigurationTypeEnum configurationType, string companyName, string appName, string apiName, DateTime expirationDate) =>
            {
                var configExp = new ConfigurationExpiration();
                configExp.ConfigurationType = configurationType;
                configExp.CompanyName = companyName;
                configExp.AppName = appName;
                configExp.ApiName = apiName;

                // expires in days
                int expiresInDays = Convert.ToInt16((expirationDate - DateTime.UtcNow).TotalDays);
                configExp.ExpiresInDays = expiresInDays;
                configExp.ExpiresOn = expirationDate.ToString("d");

                if (expiresInDays >= days)
                {
                    // good
                    configExp.Expired = false;
                }
                else if (expiresInDays <= days && expirationDate > DateTime.UtcNow)
                {
                    // close to expiring
                    configExp.Expired = false;
                    configExp.Expiring = true;
                }
                else
                {
                    // expired
                    configExp.ExpiresInDays = 0; // so it's not a negative number...
                    configExp.Expired = true;
                    configExp.Expiring = false;
                }

                return configExp;
            };

            // validate default configuration
            if (configurationType == ConfigurationTypeEnum.Default)
            {
                if (_defaultCompanyConfig.SmartyStreets?.Expiration != null)
                {
                    // smarty streets
                    ConfigurationExpiration config = createConfigExp(configurationType, DefaultCompanyConfiguration.Position, null, SmartyStreetsConfiguration.Position, _defaultCompanyConfig.SmartyStreets.Expiration.Value);
                    apiconfig.Add(config.Key, config);
                }

                if (_defaultCompanyConfig.USPS?.Expiration != null)
                {
                    // usps
                    ConfigurationExpiration config = createConfigExp(configurationType, DefaultCompanyConfiguration.Position, null, UspsConfiguration.Position, _defaultCompanyConfig.USPS.Expiration.Value);
                    apiconfig.Add(config.Key, config);
                }
            } else if (configurationType == ConfigurationTypeEnum.Companies)
            {
                // validate multi-tenancy with companies/applications
                foreach(var company in _companiesConfiguration.Configuration)
                {
                    foreach (var app in company.Value.Applications)
                    {
                        if (app.Value.SmartyStreets?.Expiration != null)
                        {
                            // smarty streets
                            ConfigurationExpiration config = createConfigExp(configurationType, company.Value.Name, app.Value.Name, SmartyStreetsConfiguration.Position, app.Value.SmartyStreets.Expiration.Value);
                            apiconfig.Add(config.Key, config);
                        }

                        if (app.Value.USPS?.Expiration != null)
                        {
                            // usps
                            ConfigurationExpiration config = createConfigExp(configurationType, company.Value.Name, app.Value.Name, UspsConfiguration.Position, app.Value.USPS.Expiration.Value);
                            apiconfig.Add(config.Key, config);
                        }
                    }
                } 
            }

            return apiconfig;
        }
    }
}
