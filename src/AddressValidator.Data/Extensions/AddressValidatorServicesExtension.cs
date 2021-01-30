using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AddressValidator.Data.Configuration;
using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services;
using AddressValidator.Data.Services.Interfaces;
using AddressValidator.Data.Services.Validators;
using AddressValidator.Data.Services.Validators.Interfaces;

namespace AddressValidator.Data.Extensions
{
    public static class AddressValidatorServicesExtension
    {
        public static void AddAddressValidatorServices(this IServiceCollection services, IConfiguration config)
        {
            // config
            var smartyStreetsConfig = new SmartyStreetsConfiguration();
            config.GetSection(SmartyStreetsConfiguration.Position).Bind(smartyStreetsConfig);
            services.AddSingleton<SmartyStreetsConfiguration>(smartyStreetsConfig);

            // factories
            services.AddTransient<IAddressValidatorFactory, AddressValidatorFactory>();
            services.AddSingleton<Func<AddressValidatorEnum, IAddressValidatorApi>>(AddressValidatorFactory.GetAddressValidatorApi);

            // validator services
            services.AddTransient<IAddressValidatorService, AddressValidatorService>();

            // validator apis
            services.AddTransient<IAddressValidatorApi, SmartyStreetsAddressValidator>();
            services.AddTransient<IAddressValidatorApi, UspsAddressValidator>();
        }
    }
}
