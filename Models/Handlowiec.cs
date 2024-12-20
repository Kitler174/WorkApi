namespace WorkAPI.Models
{
    public class Handlowiec
    {
        public int Id { get; private set; }

        /// Login
        public string? Login { get; set; }

        /// Imię handlowca
        public string? FirstName { get; set; }

        /// Nazwisko handlowca
        public string? LastName { get; set; }

        /// Email handlowca
        public string? Email { get; set; }

        /// Telefon handlowca
        public string? PhoneNumber { get; set; }
    }
}