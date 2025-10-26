using System;
using System.Collections.Generic;

namespace SneezePharma.Models
{
    public class RestrictedCustomerModel
    {
        public string CPF { get; set; }

        public RestrictedCustomerModel(string cpf)
        {
            this.CPF = cpf;
        }
    }
}
