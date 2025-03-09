using SpecieProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecieProject.Implements
{
    public class SpecieService
    {
        private readonly IRepository<Specie> _repository;

        public SpecieService(IRepository<Specie> repository)
        {
            _repository = repository;
        }

        public Specie GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("Specie not found");
        }

        public IEnumerable<Specie> GetAll()
        {
            return _repository.GetAll();
        }

        public void Add(Specie specie)
        {
            if (string.IsNullOrWhiteSpace(specie.Name))
            {
                throw new ArgumentException("Name is required");
            }
            if (string.IsNullOrWhiteSpace(specie.Description))
            {
                throw new ArgumentException("Description is required");
            }

            _repository.Add(specie);
            _repository.SaveChanges();
        }

        public void Update(Specie specie)
        {
            if (string.IsNullOrWhiteSpace(specie.Name))
            {
                throw new ArgumentException("Name is required");
            }
            if (string.IsNullOrWhiteSpace(specie.Description))
            {
                throw new ArgumentException("Description is required");
            }

            _repository.Update(specie);
            _repository.SaveChanges();
        }

        public void Delete(int id)
        {
            var specie = _repository.GetById(id);
            if (specie == null)
            {
                throw new Exception("Specie not found");
            }

            _repository.Delete(specie);
            _repository.SaveChanges();
        }
    }
}
