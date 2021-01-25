using System.Collections.Generic;
using AddressValidator.Data.Models;
using System.Threading.Tasks;

namespace AddressValidator.Data.Services.Validators.Interfaces
{
    public interface IAddressValidatorApi
    {
        public bool BatchCapable { get; }

        Task ValidateAddressAsync(ValidatedAddress address);
        Task ValidateAddressesAsync(List<ValidatedAddress> addresses);
    }
}
