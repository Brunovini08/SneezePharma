using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class RestrictedSupplierModel
    {
        public string Cnpj { get; private set; }
        public RestrictedSupplierModel(string cnpj)
        {
            this.Cnpj = cnpj;
        }

        public string SalvarArquivo()
        {
            var cnpj = this.Cnpj;
            
            return $"{cnpj}";
        }
    }
}
