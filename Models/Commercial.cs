using System;
using System.Collections.Generic;

namespace inventory_management_api.Models
{
    public partial class Commercial
    {
        public ulong IdCommercial { get; set; }
        public string NomCommercial { get; set; }
        public string PrenomCommercial { get; set; }
        public string LoginCommercial { get; set; }
        public string PasswordCommercial { get; set; }
    }
}
