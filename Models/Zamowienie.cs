using System.ComponentModel.DataAnnotations;

namespace WorkAPI.Models
{
    public class Zamowienie
    {
        /// Numer zamówienia klienta
        public string? NumerZamowieniaKlienta { get; set; } = Guid.NewGuid().ToString();

        /// Numer zamówienia
        [Key]
        public string? NumerZamowienia { get; set; }

        /// Waluta
        public string Waluta { get; set; }

        // Login kupca
        public string Login { get; set; }

        /// Data zamówienia
        public DateTime DataZamowienia { get; set; }

        /// Handlowiec
        public string? Handlowiec { get; set; }

        public Kontrahent Kontrahent { get; set; } = new Kontrahent();

        public IEnumerable<Zamowienie_pozycja> PozycjeZamowienia { get; set; } = new List<Zamowienie_pozycja>();
    }
}