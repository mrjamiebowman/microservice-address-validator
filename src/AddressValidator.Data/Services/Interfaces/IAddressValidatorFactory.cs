using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services.Validators.Interfaces;
using System;

namespace AddressValidator.Data.Services.Interfaces
{
    public interface IAddressValidatorFactory
    {
        static Func<IServiceProvider, Func<AddressValidatorEnum?, IAddressValidatorApi>> GetAddressValidatorApi;
    }
}
