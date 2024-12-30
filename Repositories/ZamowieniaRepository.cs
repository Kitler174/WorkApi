using Microsoft.EntityFrameworkCore;
using WorkAPI.Contexts;
using WorkAPI.Models;

namespace WorkAPI.Repositories
{
    // Klasa repozytorium do zarządzania zamówieniami, handlowcami i danymi magazynowymi
    public class ZamowieniaRepository
    {
        private readonly WorkContext _context;

        // Konstruktor repozytorium, który wstrzykuje kontekst bazy danych
        public ZamowieniaRepository(WorkContext context)
        {
            _context = context;
        }

        // Pobiera handlowca na podstawie loginu (odbiorcy)
        public IEnumerable<Handlowiec> GetSeller(string odbiorca)
        {
            try
            {
                return _context.handlowiec.Where(k => k.Login == odbiorca).ToList();
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetSeller: {ex.Message}");
                throw;
            }
        }

        // Pobiera kontrahenta na podstawie loginu (odbiorcy)
        public IEnumerable<Kontrahent> GetContractor(string odbiorca)
        {
            try
            {
                return _context.kontrahent.Where(k => k.NazwaKontrahenta == odbiorca).ToList();
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetSeller: {ex.Message}");
                throw;
            }
        }
        public IEnumerable<Kontrahent> GetKontrahent(string numer, string nazwa)
        {
            try
            {
                return _context.kontrahent.Where(k => k.NumerKontrahenta == numer && k.NazwaKontrahenta == nazwa);
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetSeller: {ex.Message}");
                throw;
            }
        }
        // Pobiera wszystkie rekordy z tabeli magazynu
        public IEnumerable<Magazyn> GetWarehouse()
        {
            try
            {
                return _context.magazyn.ToList();
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetWarehouse: {ex.Message}");
                throw;
            }
        }

        // Pobiera zamówienia na podstawie loginu odbiorcy
        public IEnumerable<Zamowienie> GetOrders(string odbiorca)
        {
            try
            {
                var zamowienia = _context.zamowienie.Include(z => z.PozycjeZamowienia).Include(z => z.Kontrahent).ToList().OrderBy(z => z.DataZamowienia);
                return zamowienia;
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetOrders: {ex.Message}");
                throw;
            }
        }

        // Pobiera pozycje zamówienia na podstawie jego numeru 
        public IEnumerable<Zamowienie> GetItems(string nr)
        {
            try
            {
                var zamowienie = _context.zamowienie.Include(z => z.PozycjeZamowienia).Where(k => k.NumerZamowienia == nr).ToList();
                return zamowienie;
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetItems: {ex.Message}");
                throw;
            }
        }

        // Dodaje nowe zamówienie do kontekstu
        public void Add(Zamowienie zamowienie)
        {
            try
            {
                _context.zamowienie.Add(zamowienie);
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w AddZamowienia: {ex.Message}");
                throw;
            }
        }

        public void DelPosition(string numer, int pozycja)
        {
            try
            {
                // Pobierz zamówienie razem z pozycjami
                var zam = _context.zamowienie.Include(z => z.PozycjeZamowienia)
                    .FirstOrDefault(z => z.NumerZamowienia == numer);
                // Znajdź pozycję do usunięcia
                var pozycjaa = zam.PozycjeZamowienia.FirstOrDefault(p => p.Pozycja == pozycja);

                // Usuń pozycję z zamówienia
                zam.PozycjeZamowienia = zam.PozycjeZamowienia.Where(p => p.Pozycja != pozycja).ToList();

                // Zaktualizuj bazę danych
                _context.zamowienia_pozycja.Remove(pozycjaa);
                int nowaPozycja = 1;
                foreach (var poz in zam.PozycjeZamowienia.OrderBy(p => p.Pozycja))
                {
                    poz.Pozycja = nowaPozycja;
                    nowaPozycja++;
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetItems: {ex.Message}");
                throw;
            }
        }

        public void DelOrder(string numer)
        {
            try
            {
                // Pobierz zamówienie razem z pozycjami
                var zam = _context.zamowienie.Include(z => z.PozycjeZamowienia)
                    .FirstOrDefault(z => z.NumerZamowienia == numer);
                _context.zamowienie.Remove(zam);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetItems: {ex.Message}");
                throw;
            }
        }
        public IEnumerable<Lista_parametrow> GetParams()
        {
            try
            {
                // Pobierz parametry
                var par = _context.lista_parametrow.OrderBy(p => p.Id);
                return par;
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w GetParams: {ex.Message}");
                throw;
            }
        }
        public List<Magazyn> GetFiltered(List<string> categoryIds, List<List<string>> categoryPar, string nazwa)
        {
            try
            {
                // Startowe zapytanie do wszystkich produktów
                var query = _context.produkty.AsQueryable();

                // Sprawdzenie, czy listy są tej samej długości, żeby uniknąć błędów indeksowania
                if (categoryIds.Count != categoryPar.Count)
                {
                    throw new ArgumentException($"Listy categoryIds i categoryPar muszą mieć tę samą długość. " +
        $"categoryIds: [{string.Join(", ", categoryIds)}], categoryPar: [{string.Join(", ", categoryPar.Select(list => string.Join(", ", list)))}]");
                }

                string categoryId;
                List<string> categoryValue;

                // Przechodzenie przez listy i dodawanie filtrów na podstawie wartości
                for (int i = 0; i < categoryIds.Count; i++)
                {
                    categoryId = categoryIds[i];
                    categoryValue = categoryPar[i];
                    query = query.Where(p => categoryValue.Contains(p.wartosc) && p.parametr == categoryId.ToString());
                }

                var resultList = query.OrderBy(p => p.Id).ToList();
                List<Magazyn> mag = new List<Magazyn>();
                var uniqueIndexes = query.Select(p => p.indeks).Distinct().ToList();
                foreach (var indeks in uniqueIndexes)
                {
                    if (!string.IsNullOrWhiteSpace(nazwa))
                    {
                        var magazynProdukt = _context.magazyn.FirstOrDefault(m => m.Index == indeks && m.Nazwa.Contains(nazwa));
                        if (magazynProdukt != null && !mag.Contains(magazynProdukt)) // Sprawdzenie, czy już nie został dodany
                        {
                            mag.Add(magazynProdukt);
                        }
                    }
                    else
                    {
                        var magazynProdukt = _context.magazyn.FirstOrDefault(m => m.Index == indeks);
                        if (magazynProdukt != null && !mag.Contains(magazynProdukt))
                        {
                            mag.Add(magazynProdukt);
                        }
                    }
                }

                return mag;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd w GetFiltered: {ex.Message}");
                throw;
            }
        }
        public void EditPosition(string numer, int pozycja, int nowaIlosc)
        {
            try
            {
                // Pobierz zamówienie razem z pozycjami
                var zam = _context.zamowienie.Include(z => z.PozycjeZamowienia)
                    .FirstOrDefault(z => z.NumerZamowienia == numer);
                // Znajdź pozycję do edycji
                var pozycjaa = zam.PozycjeZamowienia.FirstOrDefault(p => p.Pozycja == pozycja);
                // Zaktualizuj ilość zamówionego towaru
                pozycjaa.IleZamowiono = nowaIlosc;

                // Zaktualizuj bazę danych
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w EditQuantity: {ex.Message}");
                throw;
            }
        }

        public void AddPosition(string num, Zamowienie_pozycja zam)
        {
            var zamowienie = _context.zamowienie.Include(z => z.PozycjeZamowienia).FirstOrDefault(z => z.NumerZamowienia == num);
            var pozycje = zamowienie.PozycjeZamowienia.ToList();
            int nowaPozycja = 1;
            foreach (var poz in zamowienie.PozycjeZamowienia.OrderBy(p => p.Pozycja))
            {
                poz.Pozycja = nowaPozycja;
                nowaPozycja++;
            }
            pozycje.Add(zam);
            zamowienie.PozycjeZamowienia = pozycje;
            _context.SaveChanges();
        }

        // Zapisuje zmiany wprowadzone w kontekście bazy danych
        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Logowanie wyjątku
                Console.WriteLine($"Błąd w SaveChangesZamowienia: {ex.Message}");
                throw;
            }
        }
    }
}