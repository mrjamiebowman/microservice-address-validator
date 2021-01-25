using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AddressValidator.Data.Models;

namespace AddressValidator.Data.Services.Interfaces
{
    public interface IAddressValidatorFactoryService
    {
        public Task<AddressValidatorResult> ValidateAddressesAsync(AddressValidatorRequest request);
    }
}
