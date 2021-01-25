using AddressValidator.Data.Models.Enums;
using System.Collections.Generic;

namespace AddressValidator.Data.Models
{
    public class AddressValidatorRequest
    {
        public AddressValidatorType AddressValidatorService { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
