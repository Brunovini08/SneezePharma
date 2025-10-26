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
        private static int ultimoID = 1;

        public int IDproducao { get; set; }
        public string Principio { get; set; }
        public int QuantidadeItens { get; set; }
        List<ProduceItemModel> listaitens = new List<ProduceItemModel>();
        public ProduceItemModel(int IDprod, int quantidade)
        {


            IDproducao = IDprod;
            QuantidadeItens = quantidade;
        }
        public ProduceItemModel()
        {

        }
        public void ValidarID()
        {

            IDproducao = ultimoID;
            Console.WriteLine($"ID: " + IDproducao);
            ultimoID++;
        }
        public ProduceItemModel CadastrarItemProducao()
        {
            try
            {
                Console.WriteLine("Digite a quantidade de itens produzidos: ");
                string Qntd = Console.ReadLine();

                if (!int.TryParse(Qntd, out int quantidade))
                {
                    Console.WriteLine("Quantidade invalida. Digite um numero inteiro");
                    return null;
                }
                GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade de itens excedida");

                ValidarID();
                Console.WriteLine("Item Cadastrado!");
                Console.WriteLine($"ID: {IDproducao.ToString().PadLeft(5, '0')}, Quantidade: {quantidade}");
                return new ProduceItemModel(IDproducao, quantidade);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            //var manipulacao = new Produce().CadastrarManipulacao();
            //if (manipulacao is not null)
            //{
            //   listaManipulacoes.Add(manipulacao);
            //  Console.WriteLine("\nManipulação salva com sucesso!");
            //}

        }
        public static void MostrarItensManipulados(List<ProduceItemModel> listaitens)
        {
            if (listaitens.Count == 0)
            {
                Console.WriteLine("Nenhuma manipulação cadastrada.");
                return;
            }
            Console.WriteLine("Manipulações cadastradas: ");
            foreach (var item in listaitens)
            {
                Console.WriteLine(item);
            }
        }
        public ProduceItemModel LocalizarIDitens(int idprod)
        {

            return listaitens.Find(i => i.IDproducao == idprod);
        }

        public static void AlterarItemManipulado(List<ProduceItemModel> listaitem)
        {
            Console.WriteLine("Informe o ID do item que quer alterar: ");
            int idprod = int.Parse(Console.ReadLine() ?? "");
            ProduceItemModel alterar = listaitem.Find(m => m.IDproducao == idprod);

            if (alterar is not null)
            {
                Console.WriteLine("Informe a quantidade para alterar: ");
                if (int.TryParse(Console.ReadLine(), out int novaQnt))
                {
                    alterar.QuantidadeItens = novaQnt;
                    Console.WriteLine("Quantidade atualizada");
                }
            }
            else
            {
                Console.WriteLine("ID não encontrado");
            }

        }

        public static void LocalizarItemManipulado(List<ProduceItemModel> listaitem)
        {
            Console.WriteLine("Digite o ID que gostaria de localizar: ");
            int idprod = int.Parse(Console.ReadLine() ?? "");

            ProduceItemModel itemencontrado = listaitem.Find(m => m.IDproducao == idprod);
            if (itemencontrado is not null)
            {
                Console.WriteLine("ID Encontrado!");
                Console.WriteLine(itemencontrado);
            }
            else
            {
                Console.WriteLine("ID não encontrado.");
            }
        }
        public override string ToString()
        {
            return $"ID: {IDproducao}, Quantidade: {QuantidadeItens}";
        }
        public string SalvarArquivo()
        {
            return $"{this.IDproducao:D5}{this.QuantidadeItens:D4}";
        }
    }
}
