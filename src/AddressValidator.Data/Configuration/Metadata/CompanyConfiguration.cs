using System.Collections.Generic;

namespace AddressValidator.Data.Configuration.Metadata
{
    public class CompanyConfiguration
    {
        public string Name { get; set; }

        public Dictionary<string, ApplicationConfiguration> Applications { get; set; }
    }
}
