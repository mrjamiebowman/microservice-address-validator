using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace AddressValidator.Data.Models
{
    /// <summary>
    /// Optional configuration for multi-tenant environment.
    /// </summary>
    public class Tenant
    {
        [Required]
        public int? CompanyId { get; set; }

        [Required]
        public int? ApplicationId { get; set; }

        public Tenant(int? companyId, int? applicationId)
        {
            CompanyId = companyId;
            ApplicationId = applicationId;
        }
    }
}
