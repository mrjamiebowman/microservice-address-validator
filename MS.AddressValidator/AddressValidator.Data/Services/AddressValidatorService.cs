using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services.Interfaces;
using AddressValidator.Data.Services.Validators.Interfaces;
using System;
using System.Threading.Tasks;

namespace AddressValidator.Data.Services
{
    public class AddressValidatorService : IAddressValidatorService
    {
        private readonly Func<AddressValidatorType, IAddressValidatorApi> _validatorFactory;
        private IAddressValidatorApi _api;

        public AddressValidatorService(Func<AddressValidatorType, IAddressValidatorApi> validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        // get validator service

        public Task<IAddressValidatorApi> GetAddressValidatorService(AddressValidatorType addressValidator)
        {
            return Task.FromResult(_validatorFactory(addressValidator));
        }

        public async Task<AddressValidatorResult> ValidateAddressesAsync(AddressValidatorRequest request)
        {
            // get api
            _api = await GetAddressValidatorService(request.AddressValidatorService);

            // result
            var result = new AddressValidatorResult(request);

            // validate
            foreach (var address in result.ValidatedAddresses)
            {
                await _api.ValidateAddressesAsync(address);
            }

            return result;
        }
    }
}
