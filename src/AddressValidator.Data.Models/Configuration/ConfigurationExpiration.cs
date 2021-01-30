using System.ComponentModel;
using AddressValidator.Data.Models.Enums;

namespace AddressValidator.Data.Models.Configuration
{
    public class ConfigurationExpiration
    {
        public ConfigurationTypeEnum ConfigurationType { get; set; }

        public string CompanyName { get; set; }

        public string AppName { get; set; }

        public string ApiName { get; set; }

        /// <summary>
        /// Service degradation
        /// </summary>
        [DefaultValue(false)]
        public bool IsExpired { get; set; }

        /// <summary>
        /// Service degradation
        /// </summary>

        [DefaultValue(false)]
        public bool IsExpiring { get; set; }

        public int ExpiresInDays { get; set; }

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
