using System;
using System.Collections.Generic;

#nullable disable

namespace rent.Models
{
    public partial class MusteriSirket
    {
        public MusteriSirket()
        {
            KiralamaBilgis = new HashSet<KiralamaBilgi>();
        }

        public int MusteriSirketId { get; set; }
        public string VergiNo { get; set; }
        public string Unvan { get; set; }
        public string IlgiliKisi { get; set; }
        public string Sirkettelefon { get; set; }
        public string SirketEposta { get; set; }

        public virtual ICollection<KiralamaBilgi> KiralamaBilgis { get; set; }
    }
}
