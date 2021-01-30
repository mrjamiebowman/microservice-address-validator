using System;
using System.Collections.Generic;
using System.Text;
using AddressValidator.Data.Models.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace AddressValidator.Data.Models.Swagger
{
    class AddressValidatorRequestExample : InternalClasses, IExamplesProvider<AddressValidatorRequest>
    {
        public AddressValidatorRequest GetExamples()
        {
            var address = new Address() 
            {
                StreetAddress1 = "One Microsoft Way",
                StreetAddress2 = null,
                City = "Redmond",
                State = "WA",
                PostalCode = "98052",
                Country = "USA",
            };

            var addresses = new List<Address>();
            addresses.Add(address);

            var result = new AddressValidatorRequest
            {
                AddressValidatorService = AddressValidatorType.SmartyStreets,
                Addresses = addresses
            };

            return result;
        }
    }
}
