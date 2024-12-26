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
        public IEnumerable<Magazynek> GetProd()
        {
            try
            {
                return _context.magazynek.OrderBy(m => m.Id).ToList();
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
