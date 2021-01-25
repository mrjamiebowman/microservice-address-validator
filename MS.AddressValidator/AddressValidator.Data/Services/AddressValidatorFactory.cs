using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services.Interfaces;
using AddressValidator.Data.Services.Validators.Interfaces;
using System;
using System.Linq;
using AddressValidator.Data.Services.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace AddressValidator.Data.Services
{
    public class AddressValidatorFactory : IAddressValidatorFactory
    {
        public static Func<IServiceProvider, Func<AddressValidatorType, IAddressValidatorApi>> GetAddressValidatorApi =>
            service =>
            {
                return
                    addressValidatorType =>
                    {
                        switch (addressValidatorType)
                        {
                            case AddressValidatorType.SmartyStreets:
                                return service.GetServices<IAddressValidatorApi>().First(s => s is SmartyStreetsAddressValidator);
                            case AddressValidatorType.Usps:
                                return service.GetServices<IAddressValidatorApi>().First(s => s is UspsAddressValidator);
                            case AddressValidatorType.Default:
                            default:
                                throw new ArgumentException("Could not resolve address locator service.");
                        }
                    };
            };
    }
}
