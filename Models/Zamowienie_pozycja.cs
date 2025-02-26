using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkAPI.Models
{
    public class Zamowienie_pozycja
    {
        [Key]
        public int ZamowienieId { get; set; }
        public int Pozycja { get; set; }

        /// Status zamówienia
        public string Status { get; set; }

        /// Indeks zamówienia
        public int Index { get; set; }

        /// Nazwa 1
        public string Nazwa { get; set; }

        /// Numer rysunku
        public string NrRysunku { get; set; }

        /// Jednostka miary
        public string Jm { get; set; }

        /// Ile zamówiono
        public int IleZamowiono { get; set; }

        /// Ile potwierdzono
        public int IlePotwierdzono { get; set; }

        /// Cena
        public float? Cena { get; set; }

        /// Planowana data dostawy
        public DateTime? PlanowanaDataDostawy { get; set; }

        /// Data dostawy (może być null)
        public DateTime? DataDostawy { get; set; }

        /// Numer zamówienia
        [Column("NumerZamowienia")]
        public string? NumerZamowienia { get; set; }

        /// Handlowiec
        public string? Handlowiec { get; set; }
    }
}