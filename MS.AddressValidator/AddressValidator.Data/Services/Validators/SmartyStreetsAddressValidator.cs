using System;
using System.Collections.Generic;
using System.Text;
using AddressValidator.Data.Configuration;
using AddressValidator.Data.Services.Interfaces;
using AddressValidator.Data.Services.Validators.Interfaces;

namespace AddressValidator.Data.Services.Validators
{
    public class SmartyStreetsAddressValidator : ISmartyStreetsAddressValidator, IAddressValidatorService
    {
        private readonly SmartyStreetsConfiguration _smartyStreetsConfiguration;

        public SmartyStreetsAddressValidator(SmartyStreetsConfiguration smartyStreetsConfiguration)
        {
            _smartyStreetsConfiguration = smartyStreetsConfiguration;
        }
    }
}
