using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressValidator.Data.Configuration;
using AddressValidator.Data.Configuration.Metadata;
using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Configuration;
using AddressValidator.Data.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace AddressValidator.Data.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly DefaultCompanyConfiguration _defaultCompanyConfig;
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
    }
}
