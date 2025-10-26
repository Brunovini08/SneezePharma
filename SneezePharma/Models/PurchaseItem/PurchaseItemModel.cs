using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class PurchaseItemModel
    {
        public int IdCompra { get; private set; }
        public string Ingrediente { get; private set; }
        public decimal Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal TotalItem { get; private set; }

        public static int Contador { get; private set; } = 1;

        public PurchaseItemModel(int id, string ingrediente, decimal quantidade, decimal valorUnitario, decimal totalItem)
        {
            IdCompra = id;
            Ingrediente = ingrediente;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
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
            return $"ID Compra: {this.IdCompra}\nId Ingrediente: {this.Ingrediente}\nQuantidade: {this.Quantidade}\nValor Unitário: {this.ValorUnitario}\nTotal dos itens: {this.TotalItem}\nID Compra: {this.IdCompra}";
        }

        public string SalvarArquivo()
        {
            return $"{this.IdCompra.ToString().PadLeft(5, '0')}{this.Ingrediente}{this.Quantidade.ToString().PadLeft(6, '0')}{this.ValorUnitario.ToString().PadLeft(5 , '0')}{this.TotalItem.ToString().PadLeft(10, '0')}";
        }
    }
}
