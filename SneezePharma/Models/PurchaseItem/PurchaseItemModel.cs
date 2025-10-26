using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.PurchaseItem
{
    public class PurchaseItemModel
    {
        public string IdCompra { get; private set; }
        public string Ingrediente { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal TotalItem { get; private set; }

        public static int Contador { get; private set; } = 1;

        public PurchaseItemModel(string ingrediente, int quantidade, decimal valorUnitario, decimal totalItem, int idCompra)
        {
            Ingrediente = ingrediente;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
            IdCompra = idCompra.ToString().PadLeft(5, '0');
        }
    }
}
