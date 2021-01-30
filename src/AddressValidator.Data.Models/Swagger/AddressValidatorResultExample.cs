using System;
using System.Collections.Generic;
using System.Text;
using AddressValidator.Data.Models.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace AddressValidator.Data.Models.Swagger
{
    class AddressValidatorResultExample : InternalClasses, IExamplesProvider<AddressValidatorResult>
    {
        public AddressValidatorResult GetExamples()
        {
            var validAddress = new ValidatedAddress
            {
                StreetAddress1 = "One Microsoft Way",
                StreetAddress2 = null,
                City = "Redmond",
                State = "WA",
                PostalCode = "98052",
                Country = "USA",
                /* returned values */
                Valid = true,
                Latitude = "1",
                Longitude = "2",
            };

            var addresses = new List<ValidatedAddress>();
            addresses.Add(validAddress);

            var result = new AddressValidatorResult
            {
                AddressValidatorService = AddressValidatorType.SmartyStreets,
                Addresses = addresses
            };

            return result;
        }
    }
}
