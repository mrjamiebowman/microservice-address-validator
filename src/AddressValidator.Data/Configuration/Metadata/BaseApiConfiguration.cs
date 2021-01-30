using System;
using System.Collections.Generic;
using System.Text;

namespace AddressValidator.Data.Models.Configuration
{
    public abstract class BaseApiConfiguration
    {
        /// <summary>
        /// Expiration date/time for health checks and notifications.
        /// </summary>
        public DateTime? Expiration { get; set; }
    }
}
