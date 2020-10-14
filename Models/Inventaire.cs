using System;
using System.Collections.Generic;

namespace inventory_management_api.Models
{
    public partial class Inventaire
    {
        public ulong IdInventaire { get; set; }
        public string DateInventaire { get; set; }
        public ulong IdEmplacement { get; set; }

        public virtual Emplacement IdEmplacementNavigation { get; set; }
    }
}
