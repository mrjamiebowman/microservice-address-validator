using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AddressValidator.Data.Models;

namespace AddressValidator.Data.Services.Interfaces
{
    public interface IAddressValidatorService
    {
        Task<AddressValidatorResult> ValidateAddressesAsync(AddressValidatorRequest request);
    }
}
