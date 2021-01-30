using AddressValidator.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace AddressValidator.Data.Models
{
    public class AddressValidatorResult
    {
        /// <summary>
        /// Address Validator Type (SmartyStreets, USPS)
        /// </summary>
        /// <example>SmartyStreets</example>
        public AddressValidatorType AddressValidatorService { get; set; }

        /// <summary>
        /// Returns true or false if all of the addresses are valid.
        /// Each individual address can be validated in the Addresses property.
        /// </summary>
        /// <example>true</example>
        [DefaultValue(false)]
        public bool AddressesValid {
            get
            {
                if (Addresses.Count > 0 && !Addresses.Any(x => x.Valid == false))
                {
                    return true;
                }

                return false;
            }
        }

        public List<ValidatedAddress> Addresses { get; set; } = new List<ValidatedAddress>();
    }
}
