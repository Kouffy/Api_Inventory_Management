using System;
using System.Collections.Generic;

namespace inventory_management_api.Models
{
    public partial class Article
    {
        public ulong IdArticle { get; set; }
        public string LibelleArticle { get; set; }
        public ulong IdEmplacement { get; set; }

        public virtual Emplacement IdEmplacementNavigation { get; set; }
    }
}
