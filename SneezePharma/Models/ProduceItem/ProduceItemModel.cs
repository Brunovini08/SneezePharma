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
        public int QuantidadePrincipio { get; set; }

        public ProduceItemModel(int id, int idProducao, string idPrincipio, int quantidade)
        {
            Id = id;
            IdProducao = idProducao;
            Principio = idPrincipio;
            QuantidadePrincipio = quantidade;
        }
        public ProduceItemModel()
        {

        }
        public override string ToString()
        {
            return $"ID: {IdProducao}, Quantidade: {QuantidadePrincipio}";
        }
        public void SetQuantidade(int quantidade)
        {
            QuantidadePrincipio = quantidade;
        }
        public string SalvarArquivo()
        {
            return $"{this.Id.ToString().PadLeft(5, '0')}" +
                $"{this.IdProducao.ToString().PadLeft(5, '0')}" +
                $"{this.Principio}" +
                $"{this.QuantidadePrincipio.ToString().PadLeft(4, '0')}";
        }
    }
}
