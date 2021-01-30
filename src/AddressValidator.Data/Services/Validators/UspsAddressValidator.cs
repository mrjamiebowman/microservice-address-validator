using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Validators.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressValidator.Data.Models.Configuration;

namespace AddressValidator.Data.Services.Validators
{
    public class UspsAddressValidator : IAddressValidatorApi
    {
        public bool BatchCapable => false;

        public void SetConfiguration(BaseApiConfiguration config)
        {
            throw new System.NotImplementedException();
        }

        public Task ValidateAddressAsync(ValidatedAddress address)
        {
            throw new System.NotImplementedException();
        }

        public Task ValidateAddressesAsync(List<ValidatedAddress> addresses)
        {
            throw new System.NotImplementedException();
        }
    }
}
