using System;
using System.Collections.Generic;
using System.Text;
using AddressValidator.Data.Models.Swagger.Examples;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace AddressValidator.Data.Extensions
{
    public static class AddCustomSwaggerExamplesExtension
    {
        public static void AddCustomSwaggerExamples(this IServiceCollection services)
        {
            services.AddSwaggerExamplesFromAssemblyOf<AddressValidatorRequestExample>();
            services.AddSwaggerExamplesFromAssemblyOf<AddressValidatorResultExample>();
        }
    }
}
