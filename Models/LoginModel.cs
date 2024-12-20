namespace WorkAPI.Models
{
    public class LoginModel
    {
        public int Id { get; private set; }

        /// Nazwa użytkownika
        public string? Username { get; set; }

        /// Hasło użytkownika
        public string? Password { get; set; }
    }
}