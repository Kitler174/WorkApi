using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkAPI.Models;
namespace WorkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Tajny klucz itp
        private const string SecretKey = "s6iGg6T3Htn8LkJW/vC1fDq0dfMi2wH1WJW43d5TnF8=";
        private string Issuer = "http://localhost:" + Environment.GetEnvironmentVariable("PORT");
        private string Audience = "http://localhost:" + Environment.GetEnvironmentVariable("PORT");

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            // Autoryzacja logowania, dodałbym dane autoryzacji do jakiejś bazy danych albo zmiennej środowiskowej
            if (login.Username == "string" && login.Password == "string")
            {
                var tokenString = GenerateJwtToken(login.Username);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized("Invalid username or password.");
        }

        [HttpGet("check_token/{token}")]
        public IActionResult Check(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, // Sprawdza, czy token wygasł
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                // Jeśli token jest poprawny i ważny
                return Ok("Aktywny");
            }
            catch
            {
                return Unauthorized("Nieprawidlowy");
            }
        }

        // Generowanie klucza do autoryzacji
        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

// Nieprzetestowany kontroler do obsługi kluczy rsa

/*
namespace WorkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Pobranie kluczy z zmiennych środowiskowych
        private static readonly string RsaPrivateKeyBase64 = Environment.GetEnvironmentVariable("RSA_PRIVATE_KEY");
        private static readonly string RsaPublicKeyBase64 = Environment.GetEnvironmentVariable("RSA_PUBLIC_KEY");

        private const string Issuer = "https://localhost:7080";
        private const string Audience = "https://localhost:7080";

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            // Autoryzacja logowania
            if (login.Username == "string" && login.Password == "string")
            {
                var tokenString = GenerateJwtToken(login.Username);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized("Invalid username or password.");
        }

        // Generowanie tokenu JWT
        private string GenerateJwtToken(string username)
        {
            // Wczytaj klucz prywatny RSA
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(RsaPrivateKeyBase64), out _);
            var rsaKey = new RsaSecurityKey(rsa);
            var credentials = new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
 */