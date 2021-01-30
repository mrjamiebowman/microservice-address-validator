using System;
using System.Collections.Generic;
using System.Text;
using AddressValidator.Data.Configuration.Metadata;

namespace AddressValidator.Data.Configuration
{
    public class CompaniesConfiguration
    {
        public const string Position = "Companies";

        public Dictionary<string, CompanyConfiguration> Configuration { get; set; }
    }
}
