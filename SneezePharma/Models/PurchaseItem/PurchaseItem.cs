using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class PurchaseItem
    {
        public int IdCompra { get; private set; }
        public int Ingrediente { get; private set; }
        public decimal Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal TotalItem { get; private set; }

        public static int Contador { get; private set; } = 1;

        public PurchaseItem(int ingrediente, decimal quantidade, decimal valorUnitario, decimal totalItem, int idCompra)
        {
            Ingrediente = ingrediente;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
            IdCompra = idCompra;
        }

        public void setQuantidade(decimal quantidade)
        {
            Quantidade = quantidade;
        }

        public void setValorUnitario(decimal valorUnitario)
        {
            ValorUnitario = valorUnitario;
        }

        public override string ToString()
        {
            return $"Id Ingrediente: {this.Ingrediente}\nQuantidade: {this.Quantidade}\nValor Unitário: {this.ValorUnitario}\nTotal dos itens: {this.TotalItem}\nID Compra: {this.IdCompra}";
        }
    }
}
