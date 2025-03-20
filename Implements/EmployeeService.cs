using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Interfaces;
using AnimalHouse.Entities;

namespace AnimalHouse.Implements
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void AddEmployee(string name, int shelterID, string? phoneNumber = null, string? Email = null)
        {
            var employee = new Employee
            {
                Name = name,
                ShelterId = shelterID,
                Phone = phoneNumber,
                Email = Email
            };
            _employeeRepository.Add(employee);
            _employeeRepository.SaveChanges();
        }
        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAll().ToList();
        }
        public bool UpdateEmployee(int id, int newShelterID = 0, string newName = "", string newPhoneNumber = "")
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return false;
            }
            if (newShelterID != 0)
            {
                employee.ShelterId = newShelterID;
            }
            if (!string.IsNullOrEmpty(newName))
            {
                employee.Name = newName;
            }
            
            if (!string.IsNullOrEmpty(newPhoneNumber))
            {
                employee.Phone = newPhoneNumber;
            }
            _employeeRepository.Update(employee);
            _employeeRepository.SaveChanges();
            return true;
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return false;
            }
            _employeeRepository.Delete(employee);
            _employeeRepository.SaveChanges();
            return true;
        }
    }
}
