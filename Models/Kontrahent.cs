using System.ComponentModel.DataAnnotations;

namespace WorkAPI.Models
{
    public class Kontrahent
    {
        [Key]
        public int Id { get; set; }

        public string NumerKontrahenta { get; set; }

        public string NazwaKontrahenta { get; set; }

        public string Miejscowosc { get; set; }

        public string Ulica { get; set; }

        public string NumerDomu { get; set; }

        public string NumerLokalu { get; set; }

        public string KodPocztowy { get; set; }

        public string Kraj { get; set; }

        public string Nip { get; set; }

        public string NazwaWysylki { get; set; }

        public string UlicaWysylki { get; set; }

        public string MiastoWysylki { get; set; }
    }
}