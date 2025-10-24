using System;
using System.Collections.Generic;

namespace SneezePharma.Models.RestrictedCustomers
{
    public class RestrictedCustomer
    {
        List<Customer> Customers { get; set; }

        public RestrictedCustomer()
        {
            Customers = new List<Customer>();
        }


        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public void RemoveCustomer(Customer customer)
        {
            Customers.Remove(customer);
        }
    }
}
