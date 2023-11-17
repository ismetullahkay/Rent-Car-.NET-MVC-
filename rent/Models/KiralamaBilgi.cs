using System;
using System.Collections.Generic;

#nullable disable

namespace rent.Models
{
    public partial class KiralamaBilgi
    {
        public int KiralamaBilgiId { get; set; }
        public int? AracId { get; set; }
        public int? MusteriId { get; set; }
        public decimal? ToplamUcret { get; set; }
        public int? MusteriSirketId { get; set; }
        public int? KiralamaGun { get; set; }
        public bool? HasarDurum { get; set; }
        public DateTime? KiraGunu { get; set; }
        public DateTime? TeslimGunu { get; set; }

        public virtual Arac Arac { get; set; }
        public virtual Musteri Musteri { get; set; }
        public virtual MusteriSirket MusteriSirket { get; set; }
    }
}
