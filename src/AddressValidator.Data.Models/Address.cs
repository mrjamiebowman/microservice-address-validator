using System;
using System.ComponentModel.DataAnnotations;

namespace AddressValidator.Data.Models
{
    public class Address 
    {
        /// <summary>
        /// Street Address 1
        /// </summary>
        /// <example>One Microsoft Way</example>
        [Required]
        public string StreetAddress1 { get; set; }

        /// <summary>
        /// Street Address 2
        /// </summary>
        /// <example></example>
        public string StreetAddress2 { get; set; }

        /// <summary>
        /// City
        /// </summary>
        /// <example>Redmond</example>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// State
        /// </summary>
        /// <example>Washington</example>
        [Required]
        public string State { get; set; }

        /// <summary>
        /// Postal Code
        /// </summary>
        /// <example>98052</example>
        [Required]
        public string PostalCode { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        /// <example>USA</example>
        public string Country { get; set; }
    }
}
