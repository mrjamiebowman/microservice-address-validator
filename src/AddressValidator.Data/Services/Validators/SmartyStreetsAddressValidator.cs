using System;
using System.Collections.Generic;
using System.IO;
using AddressValidator.Data.Configuration;
using AddressValidator.Data.Models;
using AddressValidator.Data.Services.Validators.Interfaces;
using System.Threading.Tasks;
using AddressValidator.Data.Configuration.Metadata;
using AddressValidator.Data.Models.Configuration;
using SmartyStreets;
using SmartyStreets.USStreetApi;

namespace AddressValidator.Data.Services.Validators
{
    public class SmartyStreetsAddressValidator : ISmartyStreetsAddressValidator, IAddressValidatorApi
    {
        private SmartyStreetsConfiguration _smartyStreetsConfiguration;

        public bool BatchCapable => true;

        public SmartyStreetsAddressValidator()
        {

        }

        public void SetConfiguration(BaseApiConfiguration config)
        {
            _smartyStreetsConfiguration = config as SmartyStreetsConfiguration ?? throw new ArgumentException(nameof(config));
        }

        public Task ValidateAddressAsync(ValidatedAddress address)
        {
            address.Valid = true;
            return Task.CompletedTask;
        }

        public Task ValidateAddressesAsync(List<ValidatedAddress> addresses)
        {
            // TODO: map configuration to correct service
            var client = new ClientBuilder(_smartyStreetsConfiguration.Key, _smartyStreetsConfiguration.AuthToken).BuildUsStreetApiClient();
            var batch = new Batch();

            try
            {
                // for each
                foreach (var address in addresses)
                {
                    var lookup = ConvertAddressToUsLookup(address);
                    batch.Add(lookup);
                }

                client.Send(batch);
            }
            catch (BatchFullException ex)
            {
                throw ex;
            }
            catch (SmartyException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }


            for (var i = 0; i < batch.Count; i++)
            {
                var candidates = batch[i].Result;

                if (candidates.Count == 0)
                {
                    addresses[0].Valid = false;
                    Console.WriteLine("Address " + i + " is invalid.\n");
                    continue;
                }

                Console.WriteLine("Address " + i + " is valid. (There is at least one candidate)");

                foreach (var candidate in candidates)
                {
                    addresses[0].Valid = true;

                    var components = candidate.Components;
                    var metadata = candidate.Metadata;

                    Console.WriteLine("\nCandidate " + candidate.CandidateIndex + ":");
                    Console.WriteLine("Delivery line 1: " + candidate.DeliveryLine1);
                    Console.WriteLine("Last line:	   " + candidate.LastLine);
                    Console.WriteLine("ZIP Code:		" + components.ZipCode + "-" + components.Plus4Code);
                    Console.WriteLine("County:		  " + metadata.CountyName);
                    Console.WriteLine("Latitude:		" + metadata.Latitude);
                    Console.WriteLine("Longitude:	   " + metadata.Longitude);
                }

                Console.WriteLine();
            }

            return Task.CompletedTask;
        }

        public static SmartyStreets.USStreetApi.Lookup ConvertAddressToUsLookup(Address address)
        {
            var lookup = new Lookup();
            lookup.Street = address.StreetAddress1;
            lookup.Street2 = address.StreetAddress2;
            lookup.City = address.City;
            lookup.State = address.State;
            lookup.ZipCode = address.PostalCode;

            return lookup;
        }
    }
}
