using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AddressValidator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateAddressController : ControllerBase
    {
        private readonly IAddressValidatorFactoryService _addressValidatorFactoryService;

        public ValidateAddressController(IAddressValidatorFactoryService addressValidatorFactoryService)
        {
            _addressValidatorFactoryService = addressValidatorFactoryService;
        }

        public async Task<Data.Models.AddressValidatorResult> ValidateAddressAsync(AddressValidatorRequest request)
        {
            return await _addressValidatorFactoryService.ValidateAddressesAsync(request);
        }
    }
}
