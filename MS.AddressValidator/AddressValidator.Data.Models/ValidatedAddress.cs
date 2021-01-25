using System;
using System.Collections.Generic;
using System.Text;

namespace AddressValidator.Data.Models
{
    public class ValidatedAddress : Address
    {
        public bool Valid { get; set; }

        public ValidatedAddress()
        {

        }

        public ValidatedAddress(Address address)
        {
            StreetAddress1 = address.StreetAddress1;
            StreetAddress2 = address.StreetAddress2;
            City = address.City;
            State = address.State;
            PostalCode = address.PostalCode;
            Country = address.Country;
        }
    }
}
