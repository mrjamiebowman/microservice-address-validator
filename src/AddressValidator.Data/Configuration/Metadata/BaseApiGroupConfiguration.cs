namespace AddressValidator.Data.Configuration.Metadata
{
    public abstract class BaseApiGroupConfiguration
    {
        public string Name { get; set; }

        public SmartyStreetsConfiguration SmartyStreets { get; set; }

        public UspsConfiguration USPS { get; set; }
    }
}
