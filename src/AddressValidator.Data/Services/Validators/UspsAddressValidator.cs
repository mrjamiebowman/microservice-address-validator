using System;
using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Validators.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using AddressValidator.Data.Models.Configuration;
using AddressValidator.Data.Extensions;

namespace AddressValidator.Data.Services.Validators
{
    public class UspsAddressValidator : IAddressValidatorApi
    {
        public bool BatchCapable => false;

        public void SetConfiguration(BaseApiConfiguration config)
        {
            throw new System.NotImplementedException();
        }

        public Task ValidateAddressAsync(ValidatedAddress address)
        {
            XDocument requestDoc = new XDocument(
                new XElement("AddressValidateRequest",
                    new XAttribute("USERID", ""),
                    new XElement("Revision", "1"),
                    new XElement("Address",
                        new XAttribute("ID", "0"),
                        new XElement("Address1", "2335 S State"),
                        new XElement("Address2", "Suite 300"),
                        new XElement("City", "Provo"),
                        new XElement("State", "UT"),
                        new XElement("Zip5", "84604"),
                        new XElement("Zip4", "")
                    )
                )
            );

            try
            {
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
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.ToString());
            }

            return Task.CompletedTask;
        }


        public Task ValidateAddressesAsync(List<ValidatedAddress> addresses)
        {
            throw new System.NotImplementedException();
        }
    }
}
