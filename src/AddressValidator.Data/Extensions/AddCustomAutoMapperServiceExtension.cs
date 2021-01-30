using System;
using System.Collections.Generic;
using System.Text;
using AddressValidator.Data.Models.AutoMapper;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AddressValidator.Data.Extensions
{
    public static class AddCustomAutoMapperServiceExtension
    {
        public static void AddCustomAutoMapperService(this ServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfiles());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
