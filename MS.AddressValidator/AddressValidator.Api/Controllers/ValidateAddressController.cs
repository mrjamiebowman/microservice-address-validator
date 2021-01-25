using AddressValidator.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AddressValidator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateAddressController : ControllerBase
    {

        public async Task<Data.Models.AddressValidatorResult> ValidateAddressAsync(AddressValidatorRequest request)
        {
            var result = new Data.Models.AddressValidatorResult();

            return await Task.FromResult(result);
        }
    }
}
