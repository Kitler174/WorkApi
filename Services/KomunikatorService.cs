using WorkAPI.Models;
using WorkAPI.Repositories;

namespace WorkAPI.Services
{
    public class KomunikatorService
    {
        private readonly KomunikatorRepository _repository;

        public KomunikatorService(KomunikatorRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Komunikator> GetAll(string odbiorca)
        {
            return _repository.GetAll(odbiorca);
        }

        public IEnumerable<Komunikator> GetSended(string odbiorca)
        {
            return _repository.GetSended(odbiorca);
        }

        public IEnumerable<Komunikator> GetReceived(string odbiorca)
        {
            return _repository.GetReceived(odbiorca);
        }

        public IEnumerable<Komunikator> GetAllUnread(string odbiorca)
        {
            return _repository.GetAllUnread(odbiorca);
        }

        public Komunikator GetById(int id, string odbiorca)
        {
            return _repository.GetById(id, odbiorca);
        }

        public void Create(Komunikator komunikator)
        {
            komunikator.DataNadania = DateTime.Now.Date;
            komunikator.CzasNadania = DateTime.Now.TimeOfDay;
            komunikator.Status = "Nieodczytane";
            _repository.Add(komunikator);
            _repository.SaveChanges();
        }

        public void UpdateStatus(int id, string odbiorca)
        {
            var komunikator = _repository.GetById(id, odbiorca);
            if (komunikator != null)
            {
                komunikator.Status = "Odebrano";
                komunikator.DataOdebrania = DateTime.Now.Date;
                komunikator.CzasOdebrania = DateTime.Now.TimeOfDay;
                _repository.SaveChanges();
            }
        }
    }
}