using AnimalHouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Entities;

namespace AnimalHouse.Implements
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public bool CreateCustomer(string name, string address, string phoneNumber, string Email)
        {
            var customer = new Customer
            {
                Name = name,
                Address = address,
                Phone = phoneNumber,
                Email = Email

            };
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
            return true;
        }
        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll().ToList();
        }
        public Customer? GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }
        public bool UpdateCustomer(int id, string newName = "", string newAddress = "", string newPhoneNumber = "")
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(newName))
            {
                customer.Name = newName;
            }
            if (!string.IsNullOrEmpty(newAddress))
            {
                customer.Address = newAddress;
            }
            if (!string.IsNullOrEmpty(newPhoneNumber))
            {
                customer.Phone = newPhoneNumber;
            }
            _customerRepository.Update(customer);
            _customerRepository.SaveChanges();
            return true;
        }
        public bool DeleteCustomer(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return false;
            }
            _customerRepository.Delete(customer);
            _customerRepository.SaveChanges();
            return true;
        }
    }
}
