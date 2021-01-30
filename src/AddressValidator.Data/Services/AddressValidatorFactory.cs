using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services.Interfaces;
using AddressValidator.Data.Services.Validators;
using AddressValidator.Data.Services.Validators.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AddressValidator.Data.Services
{
    public class AddressValidatorFactory : IAddressValidatorFactory
    {
        public static Func<IServiceProvider, Func<AddressValidatorEnum, IAddressValidatorApi>> GetAddressValidatorApi =>
            service =>
            {
                return
                    addressValidatorType =>
                    {
                        switch (addressValidatorType)
                        {
                            case AddressValidatorEnum.SmartyStreets:
                                return service.GetServices<IAddressValidatorApi>().First(s => s is SmartyStreetsAddressValidator);
                            case AddressValidatorEnum.Usps:
                                return service.GetServices<IAddressValidatorApi>().First(s => s is UspsAddressValidator);
                            case AddressValidatorEnum.Default:
                            default:
                                throw new ArgumentException("Could not resolve address locator service.");
                        }
                    };
            };
    }
}
