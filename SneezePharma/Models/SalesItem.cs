using SneezePharma.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class SalesItem
    {
        public int Id { get; private set; }
        public int IdVenda { get; private set; }
        public string Medicamento { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal TotalItem { get; set; }

        private static int Contador = 1;

        public SalesItem(int idVenda,
            string cdbMedicamento,
            int quantidade,
            decimal valorUnitario
        )
        {
            this.Id = Contador++;
            this.IdVenda = idVenda;
            this.Medicamento = cdbMedicamento;
            this.Quantidade = quantidade;
            this.ValorUnitario = valorUnitario;
            this.TotalItem = this.Quantidade * ValorUnitario;
        }

        public SalesItem(
            int id,
            int idVenda,
            string cdbMedicamento,
            int quantidade,
            decimal valorUnitario,
            decimal totalItem
        )
        {
            this.Id = id;
            this.IdVenda = idVenda;
            this.Medicamento = cdbMedicamento;
            this.Quantidade = quantidade;
            this.ValorUnitario = valorUnitario;
            this.TotalItem = totalItem;
        }

        public void SetQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }

        public override string ToString()
        {
            return $"Id: {this.Id}\n" +
                $"Id Venda: {this.IdVenda}\n" +
                $"Medicamento: {this.Medicamento}\n" +
                $"Quantidade: {this.Quantidade}\n" +
                $"Valor Unitário: {this.ValorUnitario}\n" +
                $"Total Item: {this.TotalItem:C}";
        }

        public string SalvarNoArquivo()
        {
            var id = this.Id.ToString();            
            id = id.PadLeft(5, '0');

            var idVenda = this.IdVenda.ToString();
            idVenda = idVenda.PadLeft(5, '0');

            var quantidade = this.Quantidade.ToString();
            quantidade = quantidade.PadLeft(3, '0');

            var valorUnitario = this.ValorUnitario.ToString();
            valorUnitario = valorUnitario.PadLeft(7, '0');

            var totalItem = this.TotalItem.ToString();
            totalItem = totalItem.PadLeft(8, '0');
            return $"{id}{idVenda}{this.Medicamento}{quantidade}{valorUnitario}{totalItem}";
        }        
    }
}
