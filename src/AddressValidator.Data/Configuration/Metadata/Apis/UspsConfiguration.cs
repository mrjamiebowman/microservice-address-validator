using AddressValidator.Data.Models.Configuration;

namespace AddressValidator.Data.Configuration.Metadata
{
    public class UspsConfiguration : BaseApiConfiguration
    {
        public const string Position = "USPS";

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
