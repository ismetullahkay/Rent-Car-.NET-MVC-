namespace rent.Dto
{
    public class aracDto
    {
        public int id { get; set; }
        public string marka { get; set; }
        public string model { get; set; }
        public string markamodel { get { return $"{marka} {model} {Yil} {Plaka}"; } }
        public decimal? gunkira { get; set; }
        public int? Yil { get; set; }
        public string Plaka { get; set; }

    }
}
