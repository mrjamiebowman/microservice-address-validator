using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Enums;
using System.Text.Json;
using Xunit;

namespace AddressValidator.Data.Tests
{
    public class CreateJsonAddressValidatorRequest
    {
        [Fact]
        public void CreateJsonAddressValidatorRequestTest()
        {
            // arrange
            var request = new AddressValidatorRequest();
            request.AddressValidatorService = AddressValidatorType.Default;

            var address1 = new Address
            {
                StreetAddress1 = "One Microsoft Way",
                StreetAddress2 = null,
                City = "Redmond",
                State = "WA",
                PostalCode = "98052",
                Country = "USA"
            };

            request.Addresses.Add(address1);

            // act
            string json = JsonSerializer.Serialize(request);

            // assert (this is irrelevant)
            Assert.NotNull(json);
        }

        [Fact]
        public void CreateJsonAddressValidatorRequestBadAddressTest()
        {
            // arrange
            var request = new AddressValidatorRequest();
            request.AddressValidatorService = AddressValidatorType.Default;

            var address1 = new Address
            {
                StreetAddress1 = "1337 One Microsoft Way",
                StreetAddress2 = null,
                City = "Redmond",
                State = "WA",
                PostalCode = "27104",
                Country = "USA"
            };

            request.Addresses.Add(address1);

            // act
            string json = JsonSerializer.Serialize(request);

            // assert (this is irrelevant)
            Assert.NotNull(json);
        }
    }
}
