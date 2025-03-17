using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Entities;

namespace AnimalHouse.Interfaces
{
    interface ICustomerService
    {
        bool CreateCustomer(string name, string address, string phoneNumber,string Email);
        List<Customer> GetAllCustomers();
        Customer? GetCustomerById(int id);
        bool UpdateCustomer(int id, string newName = "", string newAddress = "", string newPhoneNumber = "");
        bool DeleteCustomer(int id);
    }
}
