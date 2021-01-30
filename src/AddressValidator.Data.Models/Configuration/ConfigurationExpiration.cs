using System.ComponentModel;
using System.Text.Json.Serialization;
using AddressValidator.Data.Models.Enums;

namespace AddressValidator.Data.Models.Configuration
{
    public class ConfigurationExpiration
    {
        [JsonIgnore]
        public ConfigurationTypeEnum ConfigurationType { get; set; }

        [JsonIgnore]
        public string CompanyName { get; set; }

        [JsonIgnore]
        public string AppName { get; set; }

        [JsonIgnore]
        public string ApiName { get; set; }

        /// <summary>
        /// Service degradation
        /// </summary>
        [DefaultValue(false)]
        public bool Expired { get; set; }

        /// <summary>
        /// Service degradation
        /// </summary>

        [DefaultValue(false)]
        public bool Expiring { get; set; }

        public int ExpiresInDays { get; set; }

        public string ExpiresOn { get; set; }

        [JsonIgnore]
        public string Key
        {
            get
            {
                if (ConfigurationType == ConfigurationTypeEnum.Default) {
                    return $"DefaultCompany_{ApiName}";
                }

                if (ConfigurationType == ConfigurationTypeEnum.Companies)
                {
                    return $"{CompanyName}_{AppName}_{ApiName}";
                }

                return string.Empty;
            }
        }
    }
}
