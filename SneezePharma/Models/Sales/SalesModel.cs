using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Sales
{
    public class SalesModel
    {
        public int Id { get; private set; }
        public DateOnly DataVenda { get; private set; } 
        public string Cliente { get; private set; }
        public decimal ValorTotal { get; private set; }

        // Construtor de cadastro da Venda
        public SalesModel(int id, string clienteCpf)
        {
            this.Id = id;
            this.DataVenda = DateOnly.FromDateTime(DateTime.Now);
            this.Cliente = clienteCpf;
            this.ValorTotal = 0;
        }

        // Construtor para leitura do arquivo
        

        public override string ToString()
        {
            return $"$Id: {this.Id.ToString().PadLeft(5, '0')}\n" +
                $"Data de venda: {this.DataVenda.ToString()}\n" +
                $"CPF do cliente: {this.Cliente}\n" +
                $"Valor total: {this.ValorTotal.ToString().PadLeft(8, '0'):C}";
        }

        public string SalvarArquivo()
        {
            var id = this.Id.ToString();
            id = id.PadLeft(5, '0');

            var dataVenda = this.DataVenda.ToString();

            var cpfCliente = this.Cliente;

            var valorTotal = this.ValorTotal.ToString();
            valorTotal = valorTotal.PadLeft(8, '0');
            return $"{id}{dataVenda}{cpfCliente}{valorTotal}";
        }
    }
}
