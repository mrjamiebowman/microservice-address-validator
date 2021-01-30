using AddressValidator.Data.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace AddressValidator.Data.Swagger.Examples
{
    public class AddressValidatorRequestExample : IExamplesProvider<AddressValidatorRequest>
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
                Addresses = addresses
            };

            return result;
        }
    }
}
