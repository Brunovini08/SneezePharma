using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class Pharma
    {
        public List<Customer> Clientes { get; private set; }
        public List<Supplier> Fonecedores { get; private set; }
        public List<Medicine> Medicamentos { get; private set; }
        public List<ProduceItem> ItemProducao { get; set; }

    }
}
