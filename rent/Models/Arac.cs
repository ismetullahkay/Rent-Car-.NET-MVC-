using System;
using System.Collections.Generic;

#nullable disable

namespace rent.Models
{
    public partial class Arac
    {
        public Arac()
        {
            KiralamaBilgis = new HashSet<KiralamaBilgi>();
        }

        public int AracId { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int? Yil { get; set; }
        public int? MotorGucu { get; set; }
        public string Renk { get; set; }
        public decimal? GunlukKiraUcret { get; set; }
        public string Plaka { get; set; }

        public virtual ICollection<KiralamaBilgi> KiralamaBilgis { get; set; }
    }
}
