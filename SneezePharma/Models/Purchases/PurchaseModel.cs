using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace SneezePharma.Models
{
    public class PurchaseModel
    {
        public int IdCompra { get; private set; }
        public DateOnly DataCompra { get; private set; }
        public string Fornecedor { get; private set; }
        public decimal ValorTotal { get; private set; }
        public PurchaseModel(int idCompra, string fornecedor, DateOnly dataCompra, decimal valorTotal)
        {
            IdCompra = idCompra;
            DataCompra = dataCompra;
            Fornecedor = fornecedor;
            ValorTotal = valorTotal;
        }
        public PurchaseModel(string fornecedor)
        {
            Fornecedor = fornecedor;
            DataCompra = new DateOnly();
        }
        public void setValorTotal(decimal valorTotal)
        {
            ValorTotal = valorTotal;
        }
        public override string ToString()
        {
            return $"Id da Compra: {IdCompra}\nData da Compra: {DataCompra}\nFornecedor: {Fornecedor}\nValor Total Venda: {ValorTotal}";
        }
        public string SalvarArquivo()
        {
            return $"{this.IdCompra.ToString().PadLeft(5, '0')}{this.DataCompra}{this.Fornecedor}{this.ValorTotal}";
        }
    }
}