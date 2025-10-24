using SneezePharma.Utils;
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

        public static int Contador { get; private set; } = 1;

        public PurchaseItem(string ingrediente, int quantidade, decimal valorUnitario, decimal totalItem, int idCompra)
        {
            Ingrediente = ingrediente;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
            IdCompra = idCompra.ToString().PadLeft(5, '0');
        }

        public void CriarItemCompra()
        {
            Console.WriteLine("Digite o ID do princípio ativo que deseja comprar: ");
            Ingrediente = Console.ReadLine();

            decimal quantidade = 0;
            do
            {
                quantidade = InputHelper.RetornarNumeroDecimal("Digite a quantidade de itens em gramas (máx: 999,99): ", "O valor deve ser maior que 0 e menor que 10000");
            } while (quantidade <= 0 || quantidade >= 10000);

            decimal valorUnitario = 0;
            do
            {
                valorUnitario = InputHelper.RetornarNumeroDecimal("Digite o valor unitário por grama (máx: 999,99):", "O valor digitado deve ser maior que 0 e menor que 1000");
            } while (valorUnitario <= 0 || valorUnitario >= 1000);

            decimal totalItem = quantidade * valorUnitario;

        }

    }
}
