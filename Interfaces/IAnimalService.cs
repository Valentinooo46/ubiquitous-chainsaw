using AnimalHouse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalHouse.Interfaces
{
    public interface IAnimalService
    {
        bool CreateAnimal(string name, string description,int age);
        List<AnimalEntity> GetAllAnimals();
        AnimalEntity? GetAnimalById(int id);
        bool UpdateAnimal(int id, int age, string newName = "", string newDescription = "");
        bool DeleteAnimal(int id);
        
    }
}
