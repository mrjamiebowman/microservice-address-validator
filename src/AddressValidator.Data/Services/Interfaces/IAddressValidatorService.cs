using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Enums;
using System.Threading.Tasks;

namespace AddressValidator.Data.Services.Interfaces
{
    public interface IAddressValidatorService
    {
        Task<AddressValidatorResult> ValidateAddressesAsync(AddressValidatorType addressValidator, AddressValidatorRequest request);
    }
}
