using System;
using System.Collections.Generic;

#nullable disable

namespace rent.Models
{
    public partial class Musteri
    {
        public Musteri()
        {
            KiralamaBilgis = new HashSet<KiralamaBilgi>();
        }

        public int MusteriId { get; set; }
        public string TcKimlik { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }

        public virtual ICollection<KiralamaBilgi> KiralamaBilgis { get; set; }
    }
}
