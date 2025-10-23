using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class Sales
    {
        public int Id { get; private set; }
        public DateOnly DataVenda { get; private set; } 
        public string Cliente { get; private set; }
        public decimal ValorTotal { get; private set; }

        private static int Contador = 1;

        public Sales(string clienteCpf, decimal valorTotal)
        {
            this.Id = Contador++;
            this.DataVenda = DateOnly.FromDateTime(DateTime.Now);
            this.Cliente = clienteCpf;
            this.ValorTotal = valorTotal;
        }

    }
}
