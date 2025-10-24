using SneezePharma.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class Pharma
    {
        public List<Customer> Clientes { get; private set; }
        public List<Supplier> Fonecedores { get; private set; }
        public List<Medicine> Medicamentos { get; private set; }
        public List<Produce> Producao { get; set; }
       // public List<ProduceItem> ItemProducao { get; set; }
        public List<RestrictedCustomer> ClientesRestritos { get; private set; }


        public void Cadastrar()
        {
            
        }

        public void VendasMedicamento()
        {

        }

        public void ComprarPrincipioAtivo()
        {

        }

        public void ManipularMedicamentos()
        {

        }

        public void ManipularClientes()
        {

        }

        public void ManipularFornecedores()
        {

        }

        public void ManipularPrincipioAtivo()
        {

        }

        public void Relatorios()
        {

        }
        #region
        /*
        //Produce

        public void CadastrarManipulacao()
        {
        
            try
            {

                Console.WriteLine("Digite a data de produção (DD/MM/AAAA): ");
                string Data = Console.ReadLine();


                if (DateOnly.TryParseExact(Data, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out DateOnly data))
                {
                    if (ValidarDataProducao(data))
                    {


                    }
                    else
                    {
                        Console.WriteLine("Falha ao validar a data");
                       
                    }
                }
                else
                {
                    Console.WriteLine("Formato de data errado. Use o DDMMAAAA");
                    
                }



                Console.WriteLine("Digite a quantidade que deseja fazer: ");
                string Qntd = Console.ReadLine();

                if (!int.TryParse(Qntd, out int quantidade))
                {
                    Console.WriteLine("Quantidade invalida. Digite um numero inteiro");
                    
                }
                GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade de medicamento excedida");

                ValidarID();
                Console.WriteLine("Manipulação Cadastrada!");
                Console.WriteLine($"ID: {ID}, Data: {data:dd/MM/yyyy}, Quantidade: {quantidade}");
                Produce produce = new Produce(ID, data, quantidade);
                this.ItemProducao.Add(produce);


            }
            catch (ArgumentException ex)
            {

                Console.WriteLine(ex.Message);
                

            }

        }

        public static void MostrarManipulacao(List<Produce> listaManipulacoes)
        {
            if (listaManipulacoes.Count == 0)
            {
                Console.WriteLine("Nenhuma manipulação cadastrada.");
                return;
            }
            Console.WriteLine("Manipulações cadastradas: ");
            foreach (var item in listaManipulacoes)
            {
                Console.WriteLine($"ID: {item.ID}, Data: {item.DataProducao: dd/MM/yyyy}, Quantidade: {item.Quantidade}");
            }
        }
        public Produce LocalizarIDmanipulacao(string id)
        {

            return listaManipulacoes.Find(m => m.ID == id);

        }

        public static void AlterarManipulacao(List<Produce> lista)
        {
            Console.WriteLine("Informe o ID que quer alterar: ");
            string id = Console.ReadLine() ?? "";
            Produce alterar = lista.Find(m => m.ID == id);

            if (alterar is not null)
            {
                Console.WriteLine("Informe a quantidade para alterar: ");
                if (int.TryParse(Console.ReadLine(), out int novaQnt))
                {
                    alterar.Quantidade = novaQnt;
                    Console.WriteLine("Quantidade atualizada");
                }
            }
            else
            {
                Console.WriteLine("ID não encontrado");
            }

        }

        public static void LocalizarManipulacao(List<Produce> lista)
        {
            Console.WriteLine("Digite o ID que gostaria de localizar: ");
            string id = Console.ReadLine() ?? "";

            Produce encontrado = lista.Find(m => m.ID == id);
            if (encontrado is not null)
            {
                Console.WriteLine("ID Encontrado!");
                Console.WriteLine(encontrado);
            }
            else
            {
                Console.WriteLine("ID não encontrado.");
            }
        }
        public override string ToString()
        {
            return $"ID: {ID}, Data: {DataProducao:dd/MM/yyyy}, Quantidade: {Quantidade}";
        }
        //ProduceItem
        public void CadastrarItemProducao()
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
        */
        #endregion
    }
}
