using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Validators.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressValidator.Data.Services.Validators
{
    public class UspsAddressValidator : IAddressValidatorApi
    {
        public bool BatchCapable => false;

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
