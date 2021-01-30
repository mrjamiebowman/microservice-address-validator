using System;
using System.Collections.Generic;
using System.Text;

namespace AddressValidator.Data.Configuration.Metadata
{
    public class ApplicationConfiguration
    {
        public string Name { get; set; }

        public SmartyStreetsConfiguration SmartyStreets { get; set; }

        public UspsConfiguration USPS { get; set; }
    }
}
