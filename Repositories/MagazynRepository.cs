using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WorkAPI.Contexts;
using WorkAPI.Models;

namespace WorkAPI.Repositories
{
    public class MagazynRepository
    {
        private readonly WorkContext _context;

        // Konstruktor repozytorium, który wstrzykuje kontekst bazy danych
        public MagazynRepository(WorkContext context)
        {
            _context = context;
        }
        public IEnumerable<string> GetWar()
        {
            try
            {
                return _context.magazynek.Select(m => m.nr_magazynu).Distinct().ToList();
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetSeller: {ex.Message}");
                throw;
            }
        }
        public IEnumerable<Magazynek> GetProd(string mag)
        {
            try
            {
                return _context.magazynek.OrderBy(m => m.Id).Where(m => (((m.status_dokum == "r") || (m.status_dokum == "o") || (m.status_dokum == "d")) && (m.nr_magazynu == mag))).ToList();
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetSeller: {ex.Message}");
                throw;
            }
        }
        public int ChangePos(string Id, decimal ilosc)
        {
            try
            {
                var pozycjaa = _context.magazynek.FirstOrDefault(p => p.indeks== Id);
                pozycjaa.status_dokum = "z";
                pozycjaa.ilosc = pozycjaa.ilosc - ilosc;
                _context.SaveChanges();
                return 0;
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetSeller: {ex.Message}");
                throw;
            }
        }
    }
}
