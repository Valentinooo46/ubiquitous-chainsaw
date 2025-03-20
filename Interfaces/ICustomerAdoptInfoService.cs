using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalHouse.Interfaces
{
    interface ICustomerAdoptInfoService
    {
        public void AdoptAnimal(int animalId, int customerId);
        public void CancelAdoption(int animalId, int customerId);
        public void AdoptInfo(int customerId);
    }
}
