using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WorkAPI.Contexts;
using WorkAPI.Repositories;
using WorkAPI.Services;

var builder = WebApplication.CreateBuilder(args);
// Dodaj usługi do kontenera
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkAPI", Version = "v1" });

    // Konfiguracja Swaggera do używania JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and then your JWT token in the format: 'Bearer [token]'. Example: 'Bearer abc123xyz...'."
    });

    //Każdy endpoint wymaga tokenu dla swagger
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Konfiguracja DbContext dla MariaDB
var connectionstr = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
var portstr = Environment.GetEnvironmentVariable("PORT");
builder.WebHost.ConfigureKestrel(options =>
{
    int portt = int.Parse(portstr);
    options.ListenAnyIP(portt);
});
builder.WebHost.UseUrls("http://0.0.0.0:" + portstr);
builder.Services.AddDbContext<WorkContext>(options =>
    options.UseMySql(connectionstr, ServerVersion.AutoDetect(connectionstr)));

// Należy rozważyć użycie klucza prywatnego i publicznego rsa dla większego bezpieczeństwa i zapisać je w zmiennej środowiskowej systemu
/*
- Generowanie kluczy w linux
openssl genpkey -algorithm RSA -out private.pem -aes256
openssl rsa -pubout -in private.pem -out public.pem

- Dodawanie kluczy do zmiennej środowiskowej w linux
export RSA_PRIVATE_KEY="$(cat /ścieżka/private.pem)"
export RSA_PUBLIC_KEY = "$(cat /ścieżka/public.pem)"
source ~/.bashrc lub source ~/.bash_profile
*/

//Kod do odczytania tych kluczy (nieprzetestowany)
/*
var rsaPrivateKey = Environment.GetEnvironmentVariable("RSA_PRIVATE_KEY");
var rsaPublicKey = Environment.GetEnvironmentVariable("RSA_PUBLIC_KEY");
RSA rsa = RSA.Create();
rsa.ImportFromPem(rsaPrivateKey.ToCharArray());
var rsaPrivateKey = new RsaSecurityKey(rsa);
RSA rsaPublic = RSA.Create();
rsaPublic.ImportFromPem(rsaPublicKey.ToCharArray());
var rsaPublicKey = new RsaSecurityKey(rsaPublic);
*/

// Konfiguracja JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:"+portstr,
        ValidAudience = "http://localhost:"+portstr,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("s6iGg6T3Htn8LkJW/vC1fDq0dfMi2wH1WJW43d5TnF8="))
        /*IssuerSigningKey = rsaPublicKey     //w przypadku użycia klucza rsa odkomentuj i usuń powyższe rozwiązanie z kluczem HmacSha256 */
    };
});
//Rejestracja
builder.Services.AddScoped<KomunikatorService>();
builder.Services.AddScoped<KomunikatorRepository>();
builder.Services.AddScoped<ZamowieniaService>();
builder.Services.AddScoped<ZamowieniaRepository>();
builder.Services.AddScoped<MagazynServices>();
builder.Services.AddScoped<MagazynRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();