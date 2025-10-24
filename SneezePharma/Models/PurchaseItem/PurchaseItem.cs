using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.PurchaseItem
{
    public class PurchaseItem
    {
        public string IdCompra { get; private set; }
        public string Ingrediente { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal TotalItem { get; private set; }

        public PurchaseItem(string idCompra, string ingrediente, int quantidade, decimal valorUnitario, decimal totalItem)
        {
            IdCompra = idCompra;
            Ingrediente = ingrediente;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
        }
    }
}
