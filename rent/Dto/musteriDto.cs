namespace rent.Dto
{
    public class musteriDto
    {
        public int id { get; set; }
        public string tckimlik { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string adsoyad { get { return $"{Ad} {Soyad}"; } }

    }
}
