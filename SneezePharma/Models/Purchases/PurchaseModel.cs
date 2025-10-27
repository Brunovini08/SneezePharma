using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace SneezePharma.Models
{
    public class PurchaseModel
    {
        public int Id { get; private set; }
        public DateOnly DataCompra { get; private set; }
        public string Fornecedor { get; private set; }
        public decimal ValorTotal { get; private set; }
        public PurchaseModel(int idCompra, string fornecedor, DateOnly dataCompra, decimal valorTotal)
        {
            Id = idCompra;
            DataCompra = dataCompra;
            Fornecedor = fornecedor;
            ValorTotal = valorTotal;
        }
        public PurchaseModel(int id, string fornecedor)
        {
            Id = id;
            Fornecedor = fornecedor;
            DataCompra = new DateOnly();
        }
        public void setValorTotal(decimal valorTotal)
        {
            ValorTotal = valorTotal;
        }
        public override string ToString()
        {
            return $"Id da Compra: {Id}\nData da Compra: {DataCompra}\nFornecedor: {Fornecedor}\nValor Total Venda: {ValorTotal}";
        }
        public string SalvarArquivo()
        {
            return $"{this.Id.ToString().PadLeft(5, '0')}{this.DataCompra.ToString("ddMMyyyy", CultureInfo.InvariantCulture)}{this.Fornecedor}{this.ValorTotal.ToString("F").PadLeft(11, '0')}";
        }
    }
}