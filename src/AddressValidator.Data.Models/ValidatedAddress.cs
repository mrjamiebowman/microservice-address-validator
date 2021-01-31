using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AddressValidator.Data.Models
{
    /// <summary>
    /// Common Address that should be returned.
    /// While this could have been refactored to use abstract and derived classes
    /// there were a lot of issues with Swagger UI not returning that data.
    /// </summary>
    public class ValidatedAddress : Address
    {
        /// <summary>
        /// Is the address valid?
        /// </summary>
        /// <example>true</example>
        [DefaultValue(false)]
        public bool Valid { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string UiMessage { get; set; }

        #region SmartyStreets

        /// <summary>
        /// Latitude returned from address validation service provider.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Latitude { get; set; }

        /// <summary>
        /// Longitutde returned from address validation service provider.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Longitude { get; set; }

        #endregion

        #region USPS

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Business { get; set; }

        #endregion
    }
}
