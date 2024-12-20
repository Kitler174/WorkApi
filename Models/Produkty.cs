using System.ComponentModel.DataAnnotations;

namespace WorkAPI.Models
{
    public class Produkty
    {
        [Key]
        public int Id { get; private set; }

        public int? indeks { get; set; }

        public string? parametr { get; set; }

        public string? wartosc { get; set; }
    }
}
