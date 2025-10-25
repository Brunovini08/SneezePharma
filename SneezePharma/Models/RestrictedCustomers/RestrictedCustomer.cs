using System;
using System.Collections.Generic;

namespace SneezePharma.Models
{
    public class RestrictedCustomer
    {
        public string CPF { get; set; }

        public RestrictedCustomer(string cpf)
        {
            this.CPF = cpf;
        }
    }
}
