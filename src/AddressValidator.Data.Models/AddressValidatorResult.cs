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
        public bool AddressesValid {
            get
            {
                if (!ValidatedAddresses.Any(x => x.Valid == false))
                {
                    return true;
                }

                return false;
            }
        }

        public List<ValidatedAddress> ValidatedAddresses { get; set; } = new List<ValidatedAddress>();


        public AddressValidatorResult()
        {

        }

        public AddressValidatorResult(AddressValidatorRequest request) : this()
        {
            AddressValidatorService = request.AddressValidatorService;
            ValidatedAddresses = request.Addresses.Select(x => new ValidatedAddress(x)).ToList();
        }
    }
}
