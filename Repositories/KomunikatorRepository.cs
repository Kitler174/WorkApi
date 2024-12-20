using WorkAPI.Contexts;
using WorkAPI.Models;

namespace WorkAPI.Repositories
{
    public class KomunikatorRepository
    {
        private readonly WorkContext _context;

        public KomunikatorRepository(WorkContext context)
        {
            _context = context;
        }

        public IEnumerable<Komunikator> GetAll(string odbiorca)
        {
            try
            {
                return _context.komunikator.Where(k => k.Odbiorca == odbiorca || k.Nadawca == odbiorca).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetAll: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Komunikator> GetSended(string odbiorca)
        {
            try
            {
                return _context.komunikator.Where(k => k.Nadawca == odbiorca).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSended: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Komunikator> GetReceived(string odbiorca)
        {
            try
            {
                return _context.komunikator.Where(k => k.Odbiorca == odbiorca).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetReceived: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Komunikator> GetAllUnread(string odbiorca)
        {
            try
            {
                return _context.komunikator.Where(k => k.Odbiorca == odbiorca && k.Status == "Nieodczytane").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllUnread: {ex.Message}");
                throw;
            }
        }

        public Komunikator GetById(int id, string odbiorca)
        {
            try
            {
                return _context.komunikator.FirstOrDefault(k => k.Id == id && k.Odbiorca == odbiorca);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetById: {ex.Message}");
                throw;
            }
        }

        public void Add(Komunikator komunikator)
        {
            try
            {
                _context.komunikator.Add(komunikator);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add: {ex.Message}");
                throw;
            }
        }

        public void Update(Komunikator komunikator)
        {
            try
            {
                _context.komunikator.Update(komunikator);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update: {ex.Message}");
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var komunikator = _context.komunikator.Find(id);
                if (komunikator != null)
                {
                    _context.komunikator.Remove(komunikator);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete: {ex.Message}");
                throw;
            }
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SaveChanges: {ex.Message}");
                throw;
            }
        }
    }
}