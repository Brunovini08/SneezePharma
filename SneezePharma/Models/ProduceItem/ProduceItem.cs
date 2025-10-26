using SneezePharma.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SneezePharma.Models
{
    public class ProduceItemModel
    {
        public int Id { get; private set; }
        public int IdProducao { get; set; }
        public string Principio { get; set; }
        public int QuantidadeItens { get; set; }

        public ProduceItemModel(int id, int idProducao, string idPrincipio, int quantidade)
        {
            Id = id;
            IdProducao = idProducao;
            Principio = idPrincipio;
            QuantidadeItens = quantidade;
        }
        public ProduceItemModel()
        {

        }
        public override string ToString()
        {
            return $"ID: {IdProducao}, Quantidade: {QuantidadeItens}";
        }
        public string SalvarArquivo()
        {
            return $"{this.IdProducao:D5}{this.QuantidadeItens:D4}";
        }
    }
}
