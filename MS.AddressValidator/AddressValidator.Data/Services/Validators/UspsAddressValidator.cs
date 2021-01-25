using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Validators.Interfaces;
using System.Threading.Tasks;

namespace AddressValidator.Data.Services.Validators
{
    public class UspsAddressValidator : IAddressValidatorApi
    {
        public Task<AddressValidatorResult> ValidateAddressesAsync(AddressValidatorRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
