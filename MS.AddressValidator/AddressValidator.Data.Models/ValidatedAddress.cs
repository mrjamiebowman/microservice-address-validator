using System;
using System.Collections.Generic;
using System.Text;

namespace AddressValidator.Data.Models
{
    public class ValidatedAddress : Address
    {
        public bool Valid { get; set; }
    }
}
