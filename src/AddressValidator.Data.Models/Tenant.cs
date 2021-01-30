using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace AddressValidator.Data.Models
{
    /// <summary>
    /// Optional configuration for multi-tenant environment.
    /// </summary>
    internal class Tenant : InternalClasses
    {
        [JsonIgnore]
        [Required]
        public int? CompanyId { get; set; }

        [JsonIgnore]
        [Required]
        public int? ApplicationId { get; set; }
    }
}
