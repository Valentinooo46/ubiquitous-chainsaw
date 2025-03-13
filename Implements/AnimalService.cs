using AnimalHouse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Interfaces;

namespace AnimalHouse.Implements
{
    internal class AnimalService(IRepository<AnimalEntity> repository) : IAnimalService
    {
        private readonly IRepository<AnimalEntity> _repository = repository;

        public bool CreateAnimal(string name, string description, int age)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Animal name cannot be empty.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Animal description cannot be empty.");
            if (age == 0)
            {
                throw new ArgumentException("Animal age cannot be null.");
            }

            if (!_repository.GetQuery().Any(x => x.Name == name))
            {
                _repository.Add(new AnimalEntity { Name = name, Description = description, AGE = age });
                _repository.SaveChanges();
                Console.WriteLine("Animal added");
                return true;
            }
            Console.WriteLine("Animal with name {0} already exists.", name);
            return false;
        }

        public bool DeleteAnimal(int id)
        {
            var animal = _repository.GetById(id);
            if (animal != null)
            {
                _repository.Delete(animal);
                _repository.SaveChanges();
                Console.WriteLine("Animal deleted");
                return true;
            }
            return true;
        }

        public List<AnimalEntity> GetAllAnimals()
        {
            return _repository.GetAll().ToList();
        }

        public AnimalEntity? GetAnimalById(int id)
        {
            return _repository.GetById(id);
        }

        public bool UpdateAnimal(int id, int age, string newName = "", string newDescription = "")
        {
            var animal = _repository.GetById(id) ?? throw new ArgumentException("Animal not found.");
            if (!string.IsNullOrWhiteSpace(newName))
            {
                if (!_repository.GetQuery().Any(x => x.Name == newName))
                    animal.Name = newName;
                else throw new ArgumentException("Animal with name {0} already exists.", newName);
            }
            if (!string.IsNullOrWhiteSpace(newDescription))
                animal.Description = newDescription;
            _repository.Update(animal);
            _repository.SaveChanges();
            Console.WriteLine("Animal updated");
            return true;
        }
    }
}
