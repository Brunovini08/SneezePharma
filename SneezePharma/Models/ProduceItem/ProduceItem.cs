using SneezePharma.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SneezePharma.Models.ProduceItem
{
    public class ProduceItem
    {
        private static int ultimoID = 1;

        public string IDproducao { get; set; }
        public string Principio { get; set; }
        public int QuantidadeItens { get; set; }
         List<ProduceItem> listaitens = new List<ProduceItem>();

    public ProduceItem(string IDprod, int quantidade) {
        

            IDproducao = IDprod;
            QuantidadeItens = quantidade;
        }
        public ProduceItem ()
        {

        }
    public void ValidarID()
        {

            IDproducao = ultimoID.ToString().PadLeft(5, '0');
            Console.WriteLine($"ID: " + IDproducao);
            ultimoID++;
        }


        public ProduceItem CadastrarItemProducao()
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
                Console.WriteLine($"ID: {IDproducao}, Quantidade: {quantidade}");
                return new ProduceItem(IDproducao, quantidade);
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
        public static void MostrarItensManipulados(List<ProduceItem> listaitens)
        {
            if (listaitens.Count == 0)
            {
                Console.WriteLine("Nenhuma manipulação cadastrada.");
                return;
            }
            Console.WriteLine("Manipulações cadastradas: ");
            foreach (var item in listaitens)
            {
                Console.WriteLine($"ID: {item.IDproducao},Quantidade: {item.QuantidadeItens}");
            }
        }
        public ProduceItem LocalizarIDitens(string id)
        {

            return listaitens.Find(i => i.IDproducao == id);
        }

        public static void AlterarItemManipulado(List<ProduceItem> listaitem)
        {
            Console.WriteLine("Informe o ID do item que quer alterar: ");
            string id = Console.ReadLine() ?? "";
            ProduceItem alterar = listaitem.Find(m => m.IDproducao == id);

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

        public static void LocalizarItemManipulado(List<ProduceItem> listaitem)
        {
            Console.WriteLine("Digite o ID que gostaria de localizar: ");
            string id = Console.ReadLine() ?? "";

            ProduceItem itemencontrado = listaitem.Find(m => m.IDproducao == id);
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
    }
}
