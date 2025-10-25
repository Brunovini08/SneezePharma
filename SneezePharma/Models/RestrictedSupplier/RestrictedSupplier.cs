using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class RestrictedSupplier
    {
        public string Cnpj { get; private set; }
        public RestrictedSupplier(string cnpj)
        {
            this.Cnpj = cnpj;
        }
    }
}
