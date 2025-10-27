using SneezePharma.Enums;

namespace SneezePharma.Models
{
    public class IngredientModel
    {

        public string Id { get; private set; }
        public string Nome { get; private set; }
        public DateOnly UltimaCompra { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public SituationIngredient Situacao { get; private set; }

        public IngredientModel(string id, string nome)
        {
            Id = id;
            Nome = nome;
            UltimaCompra = DateOnly.ParseExact("01010001", "ddMMyyyy");
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = SituationIngredient.A;
        }
        public IngredientModel(string id, string nome, DateOnly ultimaCompra, DateOnly dataCadastro, SituationIngredient situacao)
        {
            Id = id;
            Nome = nome;
            UltimaCompra = ultimaCompra;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }

        

        public override string ToString()
        {
            string sufixo = "AI";
            string strId = $"{sufixo}{this.Id.ToString().PadLeft(4, '0')}";
            return $"$Id: {strId}\nNome: {this.Nome.ToString().PadRight(20, ' ')}\n" +
                $"\nData da ultima compra: {this.UltimaCompra}\nData do ultimo cadastro: {this.DataCadastro}";
        }

        public string SalvarArquivo()
        {
            var id = this.Id.ToString();
            id = id.PadLeft(5, '0');
            var nome = this.Nome.ToString();
            nome = nome.PadRight(20, ' ');
            var ultimaCompra = this.UltimaCompra.ToString() ?? "00000000";
            var dataCadastro = this.DataCadastro.ToString();

            return $"{id}{nome}{ultimaCompra}{dataCadastro}{Situacao}";
        }

        public void setSituacao(SituationIngredient situacao)
        {
            Situacao = situacao;
        }

        public void setNome(string nome)
        {
            Nome = nome;
        }
    }
}

