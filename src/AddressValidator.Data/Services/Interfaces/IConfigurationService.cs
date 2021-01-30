using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Configuration;

namespace AddressValidator.Data.Services.Interfaces
{
    public interface IConfigurationService
    {
        BaseApiConfiguration GetApiConfiguration(Tenant tenant, string config);
    }
}
