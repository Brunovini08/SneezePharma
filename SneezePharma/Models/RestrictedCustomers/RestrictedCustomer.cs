using System;
using System.Collections.Generic;

namespace SneezePharma.Models
{
    public class RestrictedCustomer
    {
        List<Customer> Customers { get; set; }

        public RestrictedCustomer()
        {
            Customers = new List<Customer>();
        }


        public void AdicionarCliente(Customer customer)
        {
            Customers.Add(customer);
        }

        public void RemoverCliente(Customer customer)
        {
            Customers.Remove(customer);
        }
    }
}
