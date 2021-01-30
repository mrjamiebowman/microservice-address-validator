using System;
using System.Collections.Generic;
using System.Text;

namespace AddressValidator.Data.Configuration.Metadata
{
    public class UspsConfiguration
    {
        public const string Position = "USPS";

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
