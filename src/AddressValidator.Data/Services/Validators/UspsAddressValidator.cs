using AddressValidator.Data.Configuration.Metadata;
using AddressValidator.Data.Extensions;
using AddressValidator.Data.Models;
using AddressValidator.Data.Models.Configuration;
using AddressValidator.Data.Services.Validators.Interfaces;
using System;
using System.Collections.Generic;
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

                var url = "http://production.shippingapis.com/ShippingAPI.dll?API=Verify&XML=" + requestDoc;
                Console.WriteLine(url);
                
                var client = new WebClient();
                var response = client.DownloadString(url);

                var xdoc = XDocument.Parse(response.ToString());
                Console.WriteLine(xdoc.ToString());

                foreach (XElement element in xdoc.Descendants("Address"))
                {
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Address ID:	" + element.GetXMLAttribute("ID"));
                    Console.WriteLine("Address1:	" + element.GetXMLElement("Address1"));
                    Console.WriteLine("Address2:	" + element.GetXMLElement("Address2"));
                    Console.WriteLine("City:		" + element.GetXMLElement("City"));
                    Console.WriteLine("State:		" + element.GetXMLElement("State"));
                    Console.WriteLine("Zip5:		" + element.GetXMLElement("Zip5"));
                    Console.WriteLine("Zip4:		" + element.GetXMLElement("Zip4"));

                    //address.Id = element.GetXMLElement("ID");
                    address.Business = element.GetXMLElement("Business");

                    address.StreetAddress1 = element.GetXMLElement("Address1");
                    address.StreetAddress2 = element.GetXMLElement("Address2");
                    address.City = element.GetXMLElement("City");
                    address.State = element.GetXMLElement("State");

                    // postalcode
                    var zip4 = element.GetXMLElement("Zip4");
                    var zip5 = element.GetXMLElement("Zip5");
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
