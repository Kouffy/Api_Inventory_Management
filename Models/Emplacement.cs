using System;
using System.Collections.Generic;

namespace inventory_management_api.Models
{
    public partial class Emplacement
    {
        public Emplacement()
        {
            Article = new HashSet<Article>();
            Inventaire = new HashSet<Inventaire>();
            InverseIdEmplacementParentNavigation = new HashSet<Emplacement>();
        }

        public ulong IdEmplacement { get; set; }
        public string LibelleEmplacement { get; set; }
        public ulong? IdEmplacementParent { get; set; }
        public uint? ContientArticle { get; set; }

        public virtual Emplacement IdEmplacementParentNavigation { get; set; }
        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<Inventaire> Inventaire { get; set; }
        public virtual ICollection<Emplacement> InverseIdEmplacementParentNavigation { get; set; }
    }
}
