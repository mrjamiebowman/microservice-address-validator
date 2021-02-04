using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services.Interfaces;
using AddressValidator.Data.Services.Validators.Interfaces;
using AutoMapper;
using Prometheus;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AddressValidator.Data.Services
{
    public class AddressValidatorService : IAddressValidatorService
    {
        private readonly Func<AddressValidatorEnum, IAddressValidatorApi> _validatorFactory;
        private readonly IMapper _mapper;
        private readonly IConfigurationService _configurationService;

        private IAddressValidatorApi _api;

        // prometheus
        private static readonly Counter PomValidAddressCounter = Metrics.CreateCounter("addressvalidation_valid_total", "Number of addresses successfully validated.");
        private static readonly Counter PromInvalidAddressCounter = Metrics.CreateCounter("addressvalidation_invalid_total", "Number of addresses unsuccessfully validated.");


        public AddressValidatorService(Func<AddressValidatorEnum, IAddressValidatorApi> validatorFactory, IConfigurationService configurationService, IMapper mapper)
        {
            _mapper = mapper;
            _validatorFactory = validatorFactory;
            _configurationService = configurationService;
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
            result = _mapper.Map<AddressValidatorResult>(request, opt =>
                {
                    opt.Items["AddressValidatorService"] = addressValidator;
                }
            );

            // get api
            if (addressValidator == AddressValidatorEnum.Default)
            {
                addressValidator = await _configurationService.GetDefaultAddressValidator(tenant);
            }

            _api = await GetAddressValidatorService(addressValidator);
            _api.SetConfiguration(_configurationService.GetApiConfiguration(tenant, addressValidator.ToString()));
            
            if (_api.BatchCapable == true)
            {
                await _api.ValidateAddressesAsync(result.Addresses);
            } else
            {
                // validate
                foreach (var address in result.Addresses)
                {
                    await _api.ValidateAddressAsync(address);
                }
            }

            // prometheus
            PomValidAddressCounter.Inc(result.Addresses.Where(x => x.Valid == true).Count());
            PromInvalidAddressCounter.Inc(result.Addresses.Where(x => x.Valid == false).Count());

            return result;
        }
    }
}
