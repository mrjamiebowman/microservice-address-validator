using System;
using System.Collections.Generic;
using System.Text;
using AddressValidator.Data.Models;
using AddressValidator.Data.Models.AutoMapper;
using AddressValidator.Data.Models.Enums;
using AutoMapper;
using Xunit;

namespace AddressValidator.Data.Tests.AutoMapper
{
    public class AutoMapperTests
    {
        private IMapper _mapper;

        [Fact]
        public void AutoMapperConfigurationIsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void TestMap()
        {
            // arrange
            var request = new AddressValidatorRequest();
            request.Addresses = new List<Address>();
            request.Addresses.Add(new Address
            {
                StreetAddress1 = "1 Microsoft Way",
                StreetAddress2 = "1337",
                City = "Redmond",
                State = "WA",
                PostalCode = "98052",
                Country = "USA"
            });

            var addressValidator = AddressValidatorEnum.SmartyStreets;

            // result
            var result = new AddressValidatorResult();

            // act
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
            _mapper = config.CreateMapper();

            // map request to result object
            result = _mapper.Map<AddressValidatorResult>(request, opt =>
                {
                    opt.Items["AddressValidatorService"] = addressValidator;
                }
            );

            // assert
            Assert.Equal(false, result.IsValid);
            Assert.Equal(AddressValidatorEnum.SmartyStreets, result.AddressValidatorService);

            // assert: addresses
            Assert.Equal(request.Addresses[0].StreetAddress1, result.Addresses[0].StreetAddress1);
            Assert.Equal(request.Addresses[0].StreetAddress2, result.Addresses[0].StreetAddress2);
            Assert.Equal(request.Addresses[0].City, result.Addresses[0].City);
            Assert.Equal(request.Addresses[0].State, result.Addresses[0].State);
            Assert.Equal(request.Addresses[0].PostalCode, result.Addresses[0].PostalCode);
            Assert.Equal(request.Addresses[0].Country, result.Addresses[0].Country);
            Assert.Equal(false, result.Addresses[0].Valid);
        }
    }
}
