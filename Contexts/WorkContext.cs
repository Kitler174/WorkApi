using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkAPI.Models;

namespace WorkAPI.Contexts;

public class WorkContext : DbContext
{
    public WorkContext(DbContextOptions<WorkContext> options)
        : base(options)
    {
    }

    public DbSet<Komunikator> komunikator { get; set; }
    public DbSet<Handlowiec> handlowiec { get; set; }
    public DbSet<Zamowienie> zamowienie { get; set; }
    public DbSet<Kontrahent> kontrahent { get; set; }
    public DbSet<Magazyn> magazyn { get; set; }
    public DbSet<Produkty> produkty { get; set; }
    public DbSet<Magazynek> magazynek { get; set; }

    public DbSet<Lista_parametrow> lista_parametrow { get; set; }
    public DbSet<Zamowienie_pozycja> zamowienia_pozycja { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Zamowienie>()
            .HasOne(z => z.Kontrahent);
        modelBuilder.Entity<Zamowienie>()
            .HasMany(z => z.PozycjeZamowienia);
    }
}