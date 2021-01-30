using System;
using System.Collections.Generic;
using System.Text;
using AddressValidator.Data.Models.Configuration;

namespace AddressValidator.Data.Configuration.Metadata
{
    public class SmartyStreetsConfiguration : BaseApiConfiguration
    {
        public const string Position = "SmartyStreets";

        public string Key { get; set; }

        public string AuthToken { get; set; }
    }
}
