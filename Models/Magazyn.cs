namespace WorkAPI.Models
{
    public class Magazyn
    {
        public int Id { get; private set; }

        /// Jm
        public string Jm {  get; set; }

        /// Indeks produktu
        public int Index { get; set; }

        /// Nazwa produktu
        public string? Nazwa { get; set; }

        /// Numer rysunku
        public string? NrRysunku { get; set; }

        /// Stan w magazynie
        public string? StanWMagazynie { get; set; }
    }
}