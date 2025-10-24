using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class Purchases
    {
        public int IdCompra {  get; private set; }
        public DateOnly DataCompra { get; private set; }
        public string Fornecedor { get; private set; }
        public decimal ValorTotal { get; private set; }

        public Purchases(int idCompra, string fornecedor, DateOnly dataCompra, decimal valorTotal)
        {
            IdCompra = idCompra;
            DataCompra = dataCompra;
            Fornecedor = fornecedor;
            ValorTotal = valorTotal;
        }

        public Purchases(string fornecedor)
        {
            Fornecedor = fornecedor;
            DataCompra = new DateOnly();
        }

        public void CriarPurchase()
        {
            Console.WriteLine("Digite a data da compra (DD/MM/AAAA): ");
            DateOnly dataCompra = DateOnly.Parse(Console.ReadLine());

            do
            {
              Fornecedor =  InputHelper.RetornarString("Digite o CNPJ do fornecedor:", "O CNPJ é inválido.");
            }
            while (Fornecedor.Length != 14);
            decimal valorTotal = 0;
            do
            {
                valorTotal = InputHelper.RetornarNumeroDecimal("Digite o valor de Total da Compra:", "O valor deve ser maior que R$0,00 e menor que R$10000,00.");
            } while (valorTotal <= 0 || valorTotal >= 10000);

        }
    }
}
