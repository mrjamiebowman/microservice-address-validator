using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Validators.Interfaces;
using System.Threading.Tasks;

namespace AddressValidator.Data.Services.Validators
{
    public class UspsAddressValidator : IAddressValidatorApi
    {
        public Task ValidateAddressesAsync(ValidatedAddress address)
        {
            throw new System.NotImplementedException();
        }
    }
}
