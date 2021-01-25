using AddressValidator.Data.Models;
using System.Threading.Tasks;

namespace AddressValidator.Data.Services.Validators.Interfaces
{
    public interface IAddressValidatorApi
    {
        Task<AddressValidatorResult> ValidateAddressesAsync(AddressValidatorRequest request);
    }
}
