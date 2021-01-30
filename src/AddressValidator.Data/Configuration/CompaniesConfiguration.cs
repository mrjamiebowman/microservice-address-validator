using AddressValidator.Data.Configuration.Metadata;
using System.Collections.Generic;

namespace AddressValidator.Data.Configuration
{
    public class CompaniesConfiguration
    {
        public const string Position = "Companies";

        public Dictionary<string, CompanyConfiguration> Configuration { get; set; }
    }
}
