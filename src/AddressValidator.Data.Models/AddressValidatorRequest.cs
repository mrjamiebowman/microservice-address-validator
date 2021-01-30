using AddressValidator.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AddressValidator.Data.Models
{
    public class AddressValidatorRequest
    {
        [Required]
        public AddressValidatorType AddressValidatorService { get; set; }

        [Required]
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
