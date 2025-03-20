using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Interfaces;
using AnimalHouse.Entities;

namespace AnimalHouse.Implements
{
    class ShelterService : IShelterService
    {
        private readonly IRepository<Shelter> _shelterRepository;
        public ShelterService(IRepository<Shelter> shelterRepository)
        {
            _shelterRepository = shelterRepository;
        }
        public void AddShelter(string name, string? address = null, string? phoneNumber = null, string? Email = null)
        {
            var shelter = new Shelter
            {
                Name = name,
                Address = address,
                Phone = phoneNumber,
                Email = Email
            };
            _shelterRepository.Add(shelter);
            _shelterRepository.SaveChanges();
        }
        public List<Shelter> GetAllShelters()
        {
            return _shelterRepository.GetAll().ToList();
        }
        public bool UpdateShelter(int id, string newName = "", string newAddress = "", string newPhoneNumber = "")
        {
            var shelter = _shelterRepository.GetById(id);
            if (shelter == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(newName))
            {
                shelter.Name = newName;
            }
            if (!string.IsNullOrEmpty(newAddress))
            {
                shelter.Address = newAddress;
            }
            if (!string.IsNullOrEmpty(newPhoneNumber))
            {
                shelter.Phone = newPhoneNumber;
            }
            _shelterRepository.Update(shelter);
            _shelterRepository.SaveChanges();
            return true;
        }
        public bool DeleteShelter(int id)
        {
            var shelter = _shelterRepository.GetById(id);
            if (shelter == null)
            {
                return false;
            }
            _shelterRepository.Delete(shelter);
            _shelterRepository.SaveChanges();
            return true;
        }
    }
}
