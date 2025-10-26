using SneezePharma.Enums;
using SneezePharma.Helpers;
using SneezePharma.Models;
using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class IngredientModel
    {
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public DateOnly UltimaCompra { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public SituationIngredient Situacao { get; private set; }


        string GerarProximoId(List<IngredientModel> ingrediente)
        {
            //var ingredientes = ingrediente.OrderBy(p => p.Id).ToList();
            //if (ingredientes.Count == 0)
            //    return 1;
            //return ingredientes.Last().Id + 1;
            var id = ingrediente.LastOrDefault()?.Id ?? "";
            if(id  == "")
            {
                return "AI0001";
            }
            else
            {
                var numero = id[2..6];
                var numeroConvertido = int.Parse(numero);
                numeroConvertido++;

                return $"AI{numeroConvertido.ToString().PadLeft(4, '0')}";
            }
        }


        public IngredientModel(string id,
            string nome, DateOnly ultimaCompra)
        {
            this.Id = id;
            this.Nome = nome;
            this.DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            this.UltimaCompra = ultimaCompra;
            this.Situacao = SituationIngredient.A;
        }

        List<IngredientModel> ingredientes = new List<IngredientModel>();

        public void setUltimaCompra(DateOnly ultimaCompra)
        {
            this.UltimaCompra = ultimaCompra;
        }
        public bool VerificarNome(string nome)
        {
            foreach (var caractere in nome)
            {
                if (!char.IsLetterOrDigit(caractere))
                {

                    return false;
                }
            }
            return true;
        }

        public IngredientModel LocalizarIngrediente(string Id)
        {
            return ingredientes.Find(i => i.Id == Id);
        }

        public void IncluirIngrediente()
        {          
            string novoId = GerarProximoId(ingredientes);
            string nome;
            DateOnly ultimaCompra;
            do
            {
                nome = InputHelper.RetornarString("Digite o nome do ingrediente: ", "Nome do ingrediente inválido, o ingrediente deve ter até 20 caracteres e alfanumericos");
            } while (nome.Length > 20 || !VerificarNome(nome));

            do
            {
                try
                {
                    ultimaCompra = InputHelper.RetornarData("Digite a data de abertura (no modelo: DDMMAAAA):: ", "Data inválida, digite a data de abertura novamente: ");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            } while (ultimaCompra == null || ultimaCompra.ToString() == String.Empty);
            Console.Clear();

            ingredientes.Add(new IngredientModel(novoId, nome, ultimaCompra));
        }

        public override string ToString()
        {
            string sufixo = "Ai";
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
            var ultimaCompra = this.UltimaCompra.ToString();
            var dataCadastro = this.DataCadastro.ToString();

            return $"AI{id}{nome}{ultimaCompra}{dataCadastro}";
        }

        public void AlterarDataUltimaCompra()
        {
            Console.WriteLine("Digite o Id da ultima compra feita: ");
            var Id = Console.ReadLine();
            IngredientModel localizado = LocalizarIngrediente(Id);

            if (Id != null)
            {
                DateOnly novaDataUltimaCompra;
                do
                {
                    novaDataUltimaCompra = InputHelper.RetornarData("Digite a nova data da ultima compra no formato DDMMAAAA:", "Data da ultimo compra inválida");
                } while (novaDataUltimaCompra.ToString() == string.Empty);

                localizado.setUltimaCompra(novaDataUltimaCompra);
                Console.WriteLine("Data da ultima compra alterada com sucesso!");
            }
            else
            {
                Console.WriteLine("Id da ultima compra nao encontrado");
            }
        }
        public void AlterarSituacao(List<SupplierModel> suppliers, char Situacao)
        {

            Console.WriteLine("Digite o Id que deseja alterar a Situacao: ");
            var Id = Console.ReadLine();
            LocalizarIngrediente(Id);

            if (Id != null)
            {
                Console.WriteLine("Digite a nova situação: ");
                Situacao = char.Parse(Console.ReadLine());
            }
        }

        public void ListarIngrediente()
        {
            if (ingredientes.Count == 0)
            {
                Console.WriteLine("Não há nenhum ingrediente no momento");
            }
            else
            {
                Console.WriteLine("Lista de ingredientes: ");
                foreach (var ingrediente in ingredientes)
                {
                    Console.WriteLine(ingrediente.ToString());
                }
            }
        }

    }
}

