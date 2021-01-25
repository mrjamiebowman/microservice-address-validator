using System;
using System.Threading.Tasks;
using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Interfaces;

namespace AddressValidator.Data.Services
{
    public class AddressValidatorFactoryService : IAddressValidatorFactoryService
    {


        public AddressValidatorFactoryService()
        {

        }

        public async Task<AddressValidatorResult> ValidateAddressesAsync(AddressValidatorRequest request)
        {
            var result = new AddressValidatorResult();

            return await Task.FromResult(result);
        }
    }
}
