using System.ComponentModel;

namespace AddressValidator.Data.Models
{
    public class ValidatedAddress : Address
    {
        /// <summary>
        /// Is the address valid?
        /// </summary>
        /// <example>true</example>
        [DefaultValue(false)]
        public bool Valid { get; set; }

        /// <summary>
        /// Latitude returned from address validation service provider.
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Longitutde returned from address validation service provider.
        /// </summary>
        public string Longitude { get; set; }

        public ValidatedAddress()
        {

        }
    }
}
