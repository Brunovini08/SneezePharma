using SneezePharma.Enums;
using SneezePharma.Models.RestrictedSupplier;
using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Ingredient
{
    public class Ingredient
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateOnly UltimaCompra { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public SituationIngredient Situacao { get; private set; }

        public Ingredient(int id,
            string nome,
            DateOnly ultimaCompra,
            DateOnly dataCadastro)
        {
            this.Id = id;
            this.Nome = nome;
            this.UltimaCompra = ultimaCompra;
            this.DataCadastro = dataCadastro;
            this.Situacao = SituationIngredient.A;
        }

        List<Ingredient> ingredientes = new List<Ingredient>();

        public void setUltimaCompra (DateOnly ultimaCompra)
        {
            this.UltimaCompra = ultimaCompra;
        }
        public bool VerificarNome(string nome)
        {
            foreach (var caractere in nome)
            {
                if(!char.IsLetterOrDigit(caractere))
                {
                    
                    return false;
                }
            }
            return true;
        }

        public Ingredient LocalizarIngrediente(int Id)
        {
            return ingredientes.Find(i => i.Id == Id);
        }

        public void IncluirIngrediente()
        {
            int id = ingredientes.Count + 1;
            string nome;
            DateOnly ultimaCompra;
            DateOnly dataCadastro;
            do
            {
                nome = InputHelper.RetornarString("Digite o nome do ingrediente: ", "Nome do ingrediente inválido, o ingrediente deve ter até 20 caracteres e alfanumericos");
            } while (nome.Length > 20 || !VerificarNome(nome));

            do
            {
                ultimaCompra = InputHelper.RetornarData("Digite a data da ultima compra de ingredientes (DDMMAAAA): ", "Data inválida da ultima compra de ingredientes, digite outra:");
            } while (ultimaCompra == null || ultimaCompra.ToString() == string.Empty);

            do
            {
                dataCadastro = InputHelper.RetornarData("Digite a data do cadastro de ingredientes (DDMMAAAA): ", "Data inválida de cadastro de ingredientes, digite outra:");
            } while (dataCadastro == null || dataCadastro.ToString() == string.Empty);

            ingredientes.Add(new Ingredient(id, nome,
             ultimaCompra,
             dataCadastro));
        }

        public override string ToString()
        {
            return $"$Id: AI{this.Id.ToString().PadLeft(5, '0')}\n,Nome: {this.Nome.ToString().PadRight(20, ' ')}," +
                $"Data da ultima compra: {this.UltimaCompra},Data do ultimo cadastro: {this.DataCadastro}";
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
            var Id = int.Parse(Console.ReadLine());
            Ingredient ingrediente = LocalizarIngrediente(Id);

            if (Id != null)
            {
                DateOnly novaDataUltimaCompra;
                do
                {
                    novaDataUltimaCompra = InputHelper.RetornarData("Digite a nova data da ultima compra no formato DDMMAAAA:", "Data da ultimo compra inválida");
                } while (novaDataUltimaCompra.ToString() == string.Empty);

                ingrediente.setUltimaCompra(novaDataUltimaCompra);
                Console.WriteLine("Data da ultima compra alterada com sucesso!");
            }
            else
            {
                Console.WriteLine("Id da ultima compra nao encontrado");
            }
        }
        public void AlterarSituacao(List<Supplier> suppliers, char Situacao)
        {

            Console.WriteLine("Digite o Id que deseja alterar a Situacao: ");
            var Id = int.Parse(Console.ReadLine());
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

