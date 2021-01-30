using System;
using System.Collections.Generic;
using System.Text;

namespace AddressValidator.Data.Configuration.Metadata
{
    public class CompanyConfiguration
    {
        public string Name { get; set; }

        public Dictionary<string, ApplicationConfiguration> Applications { get; set; }
    }
}
