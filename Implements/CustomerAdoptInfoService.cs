using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Entities;
using AnimalHouse.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalHouse.Implements
{
    public class CustomerAdoptInfoService : ICustomerAdoptInfoService
    {
        private readonly IRepository<CustomerAdoptInfo> _repository;
        public CustomerAdoptInfoService(IRepository<CustomerAdoptInfo> repository)
        {
            _repository = repository;
        }
        public void AdoptAnimal(int animalId, int customerId)
        {
            CustomerAdoptInfo customerAdoptInfo = new CustomerAdoptInfo
            {
                AnimalId = animalId,
                CustomerId = customerId
            };
            _repository.Add(customerAdoptInfo);
            _repository.SaveChanges();

        }
        public void CancelAdoption(int animalId, int customerId)
        {
            CustomerAdoptInfo customerAdoptInfo = _repository.GetQuery().Where(x => x.AnimalId == animalId && x.CustomerId == customerId).FirstOrDefault()!;
            if (customerAdoptInfo != null)
            {
                _repository.Delete(customerAdoptInfo);
                _repository.SaveChanges();
            }
        }
        public void AdoptInfo(int customerId)
        {
            IEnumerable<CustomerAdoptInfo> customerAdoptInfos = _repository.GetQuery().Where(x => x.CustomerId == customerId).Include(x => x.Customer).Include(x=>x.Animal).ToList();
            
            foreach (var item in customerAdoptInfos)
            {
                Console.WriteLine($"AnimalId: {item.AnimalId}");
                Console.WriteLine(item.Animal.Name);
                Console.WriteLine(item.Animal.AGE);
                Console.WriteLine("-----------------");
               
                Console.WriteLine(item.Customer.Name);
                Console.WriteLine(item.Customer.Phone);

            }
        }
    }
}