using System;
using System.Collections.Generic;

namespace inventory_management_api.Models
{
    public partial class Administrateur
    {
        public ulong IdAdministrateur { get; set; }
        public string NomAdministrateur { get; set; }
        public string PrenomAdministrateur { get; set; }
        public string LoginAdministrateur { get; set; }
        public string PasswordAdministrateur { get; set; }
    }
}
