using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Configuration;
using AddressValidator.Data.Models.Enums;
using System.Collections.Generic;

namespace AddressValidator.Data.Services.Interfaces
{
    public interface IConfigurationService
    {
        BaseApiConfiguration GetApiConfiguration(Tenant tenant, string config);
        IEnumerable<ConfigurationExpiration> GetConfigurationExpirations(ConfigurationTypeEnum configurationType, int days = 30);
    }
}
