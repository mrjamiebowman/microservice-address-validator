using System.Collections.Generic;
using System.Linq;
using AddressValidator.Data.Models.Enums;

namespace AddressValidator.Data.Models
{
    public class AddressValidatorResult
    {
        public AddressValidatorType AddressValidatorService { get; set; }

        /// <summary>
        /// Returns true or false if all of the addresses are valid.
        /// Each individual address can be validated in the Addresses property.
        /// </summary>
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
