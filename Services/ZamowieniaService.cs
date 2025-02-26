using Microsoft.EntityFrameworkCore;
using WorkAPI.Models;
using WorkAPI.Repositories;

namespace WorkAPI.Services
{
    // Serwis do zarządzania zamówieniami, handlowcami i magazynem
    public class ZamowieniaService
    {
        private readonly ZamowieniaRepository _repository;

        // Konstruktor serwisu, który wstrzykuje repozytorium zamówień
        public ZamowieniaService(ZamowieniaRepository repository)
        {
            _repository = repository;
        }

        // Pobiera handlowca na podstawie loginu (odbiorcy)
        public IEnumerable<Handlowiec> GetSeller(string odbiorca)
        {
            return _repository.GetSeller(odbiorca);
        }
        // Pobiera kontrahenta na podstawie loginu (odbiorcy)
        public IEnumerable<Kontrahent> GetContrator(string odbiorca)
        {
            return _repository.GetContractor(odbiorca);
        }


        // Pobiera wszystkie rekordy z tabeli magazynu
        public IEnumerable<Magazyn> GetWarehouse()
        {
            return _repository.GetWarehouse();
        }

        // Pobiera zamówienia na podstawie loginu odbiorcy
        public IEnumerable<Zamowienie> GetOrders(string odbiorca)
        {
            return _repository.GetOrders(odbiorca);
        }

        // Pobiera pozycje zamówienia na podstawie jego numeru 
        public IEnumerable<Zamowienie> GetItems(string nr)
        {
            return _repository.GetItems(nr);
        }

        public IEnumerable<Kontrahent> GetKontrahent(string numer, string nazwa)
        {
            return _repository.GetKontrahent(numer, nazwa);
        }


        // Tworzy nowe zamówienie i ustawia domyślne wartości dla jego pól
        public void Create(Zamowienie zamowienie)
        {
            var istniejacyKontrahent = _repository.GetKontrahent(zamowienie.Kontrahent.NumerKontrahenta, zamowienie.Kontrahent.NazwaKontrahenta).FirstOrDefault();
            if (zamowienie.NumerZamowieniaKlienta == null)
            {
                zamowienie.NumerZamowieniaKlienta = Guid.NewGuid().ToString();
            }
            var zmien = Guid.NewGuid().ToString();
            zamowienie.NumerZamowienia ??= zmien;
            foreach (var pozycja in zamowienie.PozycjeZamowienia)
            {
                pozycja.NumerZamowienia = zmien;
            }
            zamowienie.Kontrahent = istniejacyKontrahent;
            zamowienie.DataZamowienia = DateTime.Now.Date;
            _repository.Add(zamowienie);
            _repository.SaveChanges();
        }

        public void DelPosition(string nr, int poz)
        {
            _repository.DelPosition(nr, poz);
        }

        public void DelOrder(string nr)
        {
            _repository.DelOrder(nr);
        }
        public IEnumerable<Lista_parametrow> GetParams()
        {
            return _repository.GetParams();
        }
        public List<Magazyn> GetFiltered(List<String> categoryIds, List<List<String>> categorypar, String nazwa)
        {
            return _repository.GetFiltered(categoryIds, categorypar, nazwa);
        }
        public void EditPosition(string nr, int poz, int ilosc)
        {
            _repository.EditPosition(nr, poz, ilosc);
        }

        public void AddPosition(string num, Zamowienie_pozycja zam)
        {
            _repository.AddPosition(num, zam);
        }
    }
}