using System;
using System.Collections.Generic;
using System.Text;

namespace AddressValidator.Data.Configuration
{
    public class SmartyStreetsConfiguration
    {
        public const string Position = "SmartyStreets";

        public string Key { get; set; }

        public string AuthToken { get; set; }
    }
}
