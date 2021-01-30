using System.Collections.Generic;
using AddressValidator.Data.Models;
using System.Threading.Tasks;
using AddressValidator.Data.Models.Configuration;

namespace AddressValidator.Data.Services.Validators.Interfaces
{
    public interface IAddressValidatorApi
    {
        public bool BatchCapable { get; }
        void SetConfiguration(BaseApiConfiguration config);
        Task ValidateAddressAsync(ValidatedAddress address);
        Task ValidateAddressesAsync(List<ValidatedAddress> addresses);
    }
}
