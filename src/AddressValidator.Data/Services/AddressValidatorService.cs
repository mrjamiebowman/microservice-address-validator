using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services.Interfaces;
using AddressValidator.Data.Services.Validators.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Prometheus;

namespace AddressValidator.Data.Services
{
    public class AddressValidatorService : IAddressValidatorService
    {
        private readonly Func<AddressValidatorType, IAddressValidatorApi> _validatorFactory;
        private IAddressValidatorApi _api;

        // prometheus
        private static readonly Counter _promValidAddressCounter = Metrics.CreateCounter("addressvalidation_valid_total", "Number of addresses successfully validated.");
        private static readonly Counter _promInvalidAddressCounter = Metrics.CreateCounter("addressvalidation_invalid_total", "Number of addresses unsuccessfully validated.");


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

            if (_api.BatchCapable == true)
            {
                await _api.ValidateAddressesAsync(result.ValidatedAddresses);
            } else
            {
                // validate
                //foreach (var address in result.ValidatedAddresses)
                //{
                //    await _api.ValidateAddressesAsync(address);
                //}
            }

            // prometheus
            _promValidAddressCounter.Inc(result.ValidatedAddresses.Where(x => x.Valid == true).Count());
            _promInvalidAddressCounter.Inc(result.ValidatedAddresses.Where(x => x.Valid == false).Count());

            return result;
        }
    }
}
