using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressValidator.Data.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AddressValidator.Data.Models.Swagger.Filters
{
    internal class AddressValidatorTypeParameterFilter : InternalClasses, IParameterFilter
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public AddressValidatorTypeParameterFilter(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name.Equals("addressValidator", StringComparison.InvariantCultureIgnoreCase))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    IEnumerable<string> names = (from action in (AddressValidatorEnum[])Enum.GetValues(typeof(AddressValidatorEnum)) select action.ToString()).ToList();
                    parameter.Schema.Enum = names.Select(p => new OpenApiString(p)).ToList<IOpenApiAny>();
                }
            }
        }
    }
}
