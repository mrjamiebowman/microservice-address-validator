﻿using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Configuration;
using AddressValidator.Data.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressValidator.Data.Configuration.Metadata;

namespace AddressValidator.Data.Services.Interfaces
{
    public interface IConfigurationService
    {
        BaseApiConfiguration GetApiConfiguration(Tenant tenant, string config);
        Task<AddressValidatorEnum> GetDefaultAddressValidator(Tenant tenant);
        Dictionary<string, ConfigurationExpiration> GetConfigurationExpirations(ConfigurationTypeEnum configurationType, int days = 30);
    }
}
