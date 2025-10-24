using SneezePharma.Models;
using SneezePharma.Models;
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
        public List<RestrictedCustomers> ClientesRestritos { get; private set; }


        public void Cadastrar()
        {
            
        }

        public void VendasMedicamento()
        {

        }

        public void ComprarPrincipioAtivo()
        {

        }

        public void ManipularMedicamentos()
        {

        }

        public void ManipularClientes()
        {

        }

        public void ManipularFornecedores()
        {

        }

        public void ManipularPrincipioAtivo()
        {

        }

        public void Relatorios()
        {

        }


    }
}
