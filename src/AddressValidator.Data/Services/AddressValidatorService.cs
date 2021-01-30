using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services.Interfaces;
using AddressValidator.Data.Services.Validators.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Prometheus;

namespace AddressValidator.Data.Services
{
    public class AddressValidatorService : IAddressValidatorService
    {
        private readonly Func<AddressValidatorEnum, IAddressValidatorApi> _validatorFactory;
        private readonly IMapper _mapper;

        private IAddressValidatorApi _api;

        // prometheus
        private static readonly Counter _promValidAddressCounter = Metrics.CreateCounter("addressvalidation_valid_total", "Number of addresses successfully validated.");
        private static readonly Counter _promInvalidAddressCounter = Metrics.CreateCounter("addressvalidation_invalid_total", "Number of addresses unsuccessfully validated.");


        public AddressValidatorService(Func<AddressValidatorEnum, IAddressValidatorApi> validatorFactory, IMapper mapper)
        {
            _mapper = mapper;
            _validatorFactory = validatorFactory;
        }

        // get validator service

        public Task<IAddressValidatorApi> GetAddressValidatorService(AddressValidatorEnum addressValidator)
        {
            return Task.FromResult(_validatorFactory(addressValidator));
        }

        public async Task<AddressValidatorResult> ValidateAddressesAsync(Tenant tenant, AddressValidatorEnum addressValidator, AddressValidatorRequest request)
        {
            // result
            var result = new AddressValidatorResult();

            // map request to result object
            result = _mapper.Map<AddressValidatorResult>(request);

            // get api
            _api = await GetAddressValidatorService(addressValidator);
            
            if (_api.BatchCapable == true)
            {
                await _api.ValidateAddressesAsync(result.Addresses);
            } else
            {
                // validate
                //foreach (var address in result.ValidatedAddresses)
                //{
                //    await _api.ValidateAddressesAsync(address);
                //}
            }

            // prometheus
            _promValidAddressCounter.Inc(result.Addresses.Where(x => x.Valid == true).Count());
            _promInvalidAddressCounter.Inc(result.Addresses.Where(x => x.Valid == false).Count());

            return result;
        }
    }
}
