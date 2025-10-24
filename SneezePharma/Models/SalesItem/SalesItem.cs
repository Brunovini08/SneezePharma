using SneezePharma.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.SalesItem
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
            Id = Contador++;
            IdVenda = idVenda;
            Medicamento = cdbMedicamento;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = Quantidade * ValorUnitario;
        }

        public void SetQuantidade(int quantidade)
        {
            Quantidade = quantidade;
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
            id = id.PadLeft(5 - id.Length);

            var idVenda = IdVenda.ToString();
            idVenda = idVenda.PadLeft(5 - id.Length);

            var quantidade = Quantidade.ToString();
            return "";
        }
    }
}
