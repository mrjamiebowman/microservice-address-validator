using System;
using System.Collections.Generic;
using System.Text;
using AddressValidator.Data.Configuration.Metadata;

namespace AddressValidator.Data.Configuration
{
    public class DefaultCompanyConfiguration
    {
        public const string Position = "DefaultCompany";

        public string Name { get; set; }

        public SmartyStreetsConfiguration SmartyStreets { get; set; }

        public UspsConfiguration USPS { get; set; }
    }
}
