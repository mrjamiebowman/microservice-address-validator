using AddressValidator.Data.Configuration.Metadata;
using AddressValidator.Data.Extensions;
using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Configuration;
using AddressValidator.Data.Services.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AddressValidator.Data.Services.Validators
{
    public class UspsAddressValidator : IAddressValidatorApi
    {
        public bool BatchCapable => false;
        private UspsConfiguration _uspsConfiguration;

        public void SetConfiguration(BaseApiConfiguration config)
        {
            _uspsConfiguration = config as UspsConfiguration ?? throw new ArgumentException(nameof(config));
        }

        private Task<XDocument> ConvertAddressToUspsXDoc(ValidatedAddress address)
        {
            XDocument requestDoc = new XDocument(
                new XElement("AddressValidateRequest",
                    new XAttribute("USERID", $"{_uspsConfiguration.Username}"),
                    new XElement("Revision", "1"),
                    new XElement("Address",
                        new XAttribute("ID", "0"),
                        new XElement("Address1", $"{address.StreetAddress1}"),
                        new XElement("Address2", $"{address.StreetAddress2}"),
                        new XElement("City", $"{address.City}"),
                        new XElement("State", $"{address.State}"),
                        new XElement("Zip5", $"{address.PostalCode}"),
                        new XElement("Zip4", $"")
                    )
                )
            );

            return Task.FromResult(requestDoc);
        }

        public async Task ValidateAddressAsync(ValidatedAddress address)
        {
            try
            {
                XDocument requestDoc = await ConvertAddressToUspsXDoc(address);

                var url = $"http://production.shippingapis.com/ShippingAPI.dll?API=Verify&XML={requestDoc}";
                Console.WriteLine(url);
                
                var client = new WebClient();
                var response = client.DownloadString(url);

                var xdoc = XDocument.Parse(response.ToString());
                Console.WriteLine(xdoc.ToString());

                foreach (XElement element in xdoc.Descendants("Address"))
                {
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Address ID:	" + element.GetXmlAttribute("ID"));
                    Console.WriteLine("Address1:	" + element.GetXmlElement("Address1"));
                    Console.WriteLine("Address2:	" + element.GetXmlElement("Address2"));
                    Console.WriteLine("City:		" + element.GetXmlElement("City"));
                    Console.WriteLine("State:		" + element.GetXmlElement("State"));
                    Console.WriteLine("Zip5:		" + element.GetXmlElement("Zip5"));
                    Console.WriteLine("Zip4:		" + element.GetXmlElement("Zip4"));

                    // check for errors
                    XElement error = element.Descendants("Error").SingleOrDefault();

                    if (error != null)
                    {
                        // invalid address or some error
                        address.Valid = false;
                        address.UiMessage = error.GetXmlElement("Description").Trim());
                        continue;
                    }

                    // valid
                    address.Valid = true;
                    address.Business = element.GetXmlElement("Business");
                    address.UiMessage = element.GetXmlElement("ReturnText");

                    address.StreetAddress1 = element.GetXmlElement("Address1");
                    address.StreetAddress2 = element.GetXmlElement("Address2");
                    address.City = element.GetXmlElement("City");
                    address.State = element.GetXmlElement("State");

                    // postal code
                    var zip4 = element.GetXmlElement("Zip4");
                    var zip5 = element.GetXmlElement("Zip5");
                    address.PostalCode = $"{zip4}-{zip5}";
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public Task ValidateAddressesAsync(List<ValidatedAddress> addresses)
        {
            throw new System.NotImplementedException();
        }
    }
}
