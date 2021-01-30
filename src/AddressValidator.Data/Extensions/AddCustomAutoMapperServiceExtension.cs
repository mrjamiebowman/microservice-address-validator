using AddressValidator.Data.Models.AutoMapper;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AddressValidator.Data.Extensions
{
    public static class AddCustomAutoMapperServiceExtension
    {
        public static void AddCustomAutoMapperService(this IServiceCollection services)
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
