﻿using WorkAPI.Models;
using WorkAPI.Repositories;

namespace WorkAPI.Services
{
    public class MagazynServices
    {
        private readonly MagazynRepository _repository;

        // Konstruktor serwisu, który wstrzykuje repozytorium zamówień
        public MagazynServices(MagazynRepository repository)
        {
            _repository = repository;
        }
        // Pobiera listy magazynów
        public IEnumerable<string> GetWar()
        {
            return _repository.GetWar();
        }
        public IEnumerable<Magazynek> GetProd(string mag)
        {
            return _repository.GetProd(mag);
        }
        public int ChangePos(string Id, decimal ilosc)
        {
            _repository.ChangePos(Id,ilosc);
            return 0;
        }
    }
}
