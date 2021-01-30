using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AddressValidator.Data.Models.Enums;

namespace AddressValidator.Data.Configuration.Metadata
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
                    var str = $"DefaultCompany_{ApiName}";
                    str = Regex.Replace(str, "[^a-zA-Z0-9]", "_");
                    return str;
                }

                if (ConfigurationType == ConfigurationTypeEnum.Companies)
                {
                    var str = $"{CompanyName}_{AppName}_{ApiName}";
                    str = Regex.Replace(str, "[^a-zA-Z0-9]", "_");
                    return str;
                }

                return string.Empty;
            }
        }
    }
}
