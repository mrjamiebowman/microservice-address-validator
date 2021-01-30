using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Swagger.Examples;
using AddressValidator.Data.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Threading.Tasks;
using System.Web.Http;
using AddressValidator.Data.Models.Enums;
using AddressValidator.Data.Services;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace AddressValidator.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ValidateAddressController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IAddressValidatorService _addressValidatorService;

        public ValidateAddressController(IMapper mapper, IAddressValidatorService addressValidatorService)
        {
            _addressValidatorService = addressValidatorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Queries address validator service to validate address(es).
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The AddressValidatorResult object with validated addresses.</returns>
        /// <response code="200">Addresses validated</response>
        /// <response code="500">If the request fails</response>     
        [HttpPost]
        [Route("{addressValidator}")]
        [ProducesResponseType(typeof(AddressValidatorResult),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(AddressValidatorRequest), typeof(AddressValidatorRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AddressValidatorResultExample))]
        public async Task<Data.Models.AddressValidatorResult> ValidateAddressAsync([FromQuery] AddressValidatorType addressValidator, AddressValidatorRequest request)
        {
            var tenant = new Tenant();
            return await _addressValidatorService.ValidateAddressesAsync(addressValidator, request);
        }
    }
}
