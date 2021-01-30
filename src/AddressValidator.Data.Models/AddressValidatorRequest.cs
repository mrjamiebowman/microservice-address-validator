using AddressValidator.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace AddressValidator.Data.Models
{
    public class AddressValidatorRequest
    {
        [Required]
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
