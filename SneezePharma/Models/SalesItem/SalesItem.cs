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

        public void SetTotalItem(decimal totalItem)
        {
            this.TotalItem = totalItem;
        }

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                $"Id Venda: {IdVenda}\n" +
                $"Medicamento: {Medicamento}\n" +
                $"Quantidade: {Quantidade}\n" +
                $"Valor Unitário: {ValorUnitario}\n" +
                $"Total Item: {TotalItem:C}";
        }

        public string SalvarNoArquivo()
        {
            var id = Id.ToString();            
            id = id.PadLeft(5, '0');

            var idVenda = IdVenda.ToString();
            idVenda = idVenda.PadLeft(5, '0');

            var quantidade = Quantidade.ToString();
            quantidade = quantidade.PadLeft(3, '0');

            var valorUnitario = ValorUnitario.ToString();
            valorUnitario = valorUnitario.PadLeft(7, '0');

            var totalItem = TotalItem.ToString();
            totalItem = totalItem.PadLeft(8, '0');
            return $"{id}{idVenda}{Medicamento}{quantidade}{valorUnitario}{totalItem}";
        }        
    }
}
