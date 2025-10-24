using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Purchases
{
    public class Purchases
    {
        public int IdCompra {  get; private set; }
        public DateOnly DataCompra { get; private set; }
        public decimal ValorTotal { get; private set; }

        public Purchases(int idCompra, DateOnly dataCompra, decimal valorTotal)
        {
            IdCompra = idCompra;
            DataCompra = dataCompra;
            ValorTotal = valorTotal;
        }

        public void CriarPurchase()
        {
            
        }
    }
}
