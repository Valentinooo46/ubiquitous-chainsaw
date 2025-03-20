using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Entities;

namespace AnimalHouse.Interfaces
{
    interface IShelterService
    {
        public void AddShelter(string name, string? address = null, string? phoneNumber = null,string? Email = null);
        public List<Shelter> GetAllShelters();
        public bool UpdateShelter(int id, string newName = "", string newAddress = "", string newPhoneNumber = "");
        public bool DeleteShelter(int id);
    }
}
