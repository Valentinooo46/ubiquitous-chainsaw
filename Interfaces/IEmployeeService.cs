using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Entities;

namespace AnimalHouse.Interfaces
{
    interface IEmployeeService
    {
        public void AddEmployee(string name, int shelterID, string? phoneNumber = null, string? Email = null);
        public List<Employee> GetAllEmployees();
        public bool UpdateEmployee(int id,int newShelterID = 0, string newName = "", string newPhoneNumber = "");
        public bool DeleteEmployee(int id);
    }
}
