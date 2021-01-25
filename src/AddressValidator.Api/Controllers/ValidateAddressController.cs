using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AddressValidator.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValidateAddressController : ControllerBase
    {
        private readonly IAddressValidatorService _addressValidatorService;

        public ValidateAddressController(IAddressValidatorService addressValidatorService)
        {
            _addressValidatorService = addressValidatorService;
        }

        [HttpPost]
        public async Task<Data.Models.AddressValidatorResult> ValidateAddressAsync(AddressValidatorRequest request)
        {
            return await _addressValidatorService.ValidateAddressesAsync(request);
        }
    }
}
