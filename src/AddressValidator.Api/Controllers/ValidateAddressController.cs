using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AddressValidator.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValidateAddressController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IAddressValidatorService _addressValidatorService;

        public ValidateAddressController(IMapper mapper, IAddressValidatorService addressValidatorService)
        {
            _addressValidatorService = addressValidatorService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<Data.Models.AddressValidatorResult> ValidateAddressAsync(AddressValidatorRequest request)
        {
            var tenant = new Tenant();
            return await _addressValidatorService.ValidateAddressesAsync(request);
        }
    }
}
