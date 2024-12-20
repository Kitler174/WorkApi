using System.ComponentModel.DataAnnotations;

namespace WorkAPI.Models
{
    public class Lista_parametrow
    {
        [Key]
        public int Id { get; set; }
        public string? wartosc { get; set; }
        public string? rodzaj { get; set; }
    }
}
