using System.Collections.Generic;
using System.Linq;

namespace AddressValidator.Data.Models
{
    public class AddressValidatorResult : AddressValidatorRequest
    {
        /// <summary>
        /// Returns true or false if all of the addresses are valid.
        /// Each individual address can be validated in the Addresses property.
        /// </summary>
        public bool ValidAddresses {
            get
            {
                if (!Addresses.Any(x => x.Valid == false))
                {
                    return true;
                }

                return false;
            }
        }

        new public List<ValidatedAddress> Addresses { get; set; }
    }
}
