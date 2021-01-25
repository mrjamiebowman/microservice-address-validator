using AddressValidator.Data.Configuration;
using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Validators.Interfaces;
using System.Threading.Tasks;

namespace AddressValidator.Data.Services.Validators
{
    public class SmartyStreetsAddressValidator : ISmartyStreetsAddressValidator, IAddressValidatorApi
    {
        private readonly SmartyStreetsConfiguration _smartyStreetsConfiguration;

        public SmartyStreetsAddressValidator(SmartyStreetsConfiguration smartyStreetsConfiguration)
        {
            _smartyStreetsConfiguration = smartyStreetsConfiguration;
        }

        public Task<AddressValidatorResult> ValidateAddressesAsync(AddressValidatorRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
