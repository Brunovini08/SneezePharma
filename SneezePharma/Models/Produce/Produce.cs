using SneezePharma.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Produce
{
    public class ProduceModel
    {
        private static int ultimoID = 1;

        public int ID { get; private set; }
        public DateOnly DataProducao { get; private set; }
        public string Medicamento { get; private set; }
        public int Quantidade { get; private set; }
        List<ProduceModel> listaManipulacoes = new List<ProduceModel>();

        public ProduceModel(int id, DateOnly dataproducao, int quantidade)
        {

            ID = id;
            DataProducao = dataproducao;
            //Medicamento = medicamento;
            Quantidade = quantidade;
        }

        public ProduceModel()
        {

        }


        public void ValidarID()
        {
            ID = ultimoID;
            Console.WriteLine($"ID: " + ID);
            ultimoID++;
        }

        public bool ValidarDataProducao(DateOnly data)
        {
            DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);


            if (data > hoje)
            {
                Console.WriteLine("Não pode adicionar uma data no futuro.");
                return false;
            }
            if (data < hoje.AddMonths(-1))
            {
                Console.WriteLine("Data de produção muito antiga.");
                return false;
            }

            DataProducao = data;
            Console.WriteLine($"Data da produção registrada: {DataProducao: dd/MM/yyyy}");
            return true;
        }

        public ProduceModel CadastrarManipulacao()
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
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("Formato de data errado. Use o DDMMAAAA");
                    return null;
                }



                Console.WriteLine("Digite a quantidade que deseja fazer: ");
                string Qntd = Console.ReadLine();

                if (!int.TryParse(Qntd, out int quantidade))
                {
                    Console.WriteLine("Quantidade invalida. Digite um numero inteiro");
                    return null;
                }
                GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade de medicamento excedida");

                ValidarID();
                Console.WriteLine("Manipulação Cadastrada!");
                Console.WriteLine($"ID: {ID.ToString().PadLeft(5, '0')}, Data: {data:dd/MM/yyyy}, Quantidade: {quantidade}");
                return new ProduceModel(ID, data, quantidade);



            }
            catch (ArgumentException ex)
            {

                Console.WriteLine(ex.Message);
                return null;

            }

        }


        public static void MostrarManipulacao(List<ProduceModel> listaManipulacoes)
        {
           
            if (listaManipulacoes.Count == 0)
            {
                Console.WriteLine("Nenhuma manipulação cadastrada.");
                return;
            }
            Console.WriteLine("Manipulações cadastradas: ");
            foreach (var item in listaManipulacoes)
            {
                //Console.WriteLine($"ID: {item.ID}, Data: {item.DataProducao: dd/MM/yyyy}, Quantidade: {item.Quantidade}");
                Console.WriteLine(item);
            }
        }

        public ProduceModel LocalizarIDmanipulacao(int id)
        {

            return listaManipulacoes.Find(m => m.ID == id);

        }

        public static void AlterarManipulacao(List<ProduceModel> lista)
        {
            Console.WriteLine("Informe o ID que quer alterar: ");
            int id = int.Parse(Console.ReadLine() ?? "");
            ProduceModel alterar = lista.Find(m => m.ID == id);

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

        public static void LocalizarManipulacao(List<ProduceModel> lista)
        {
            Console.WriteLine("Digite o ID que gostaria de localizar: ");
            int id = int.Parse(Console.ReadLine() ?? "");

            ProduceModel encontrado = lista.Find(m => m.ID == id);
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
            return $"ID: {ID.ToString().PadLeft(5, '0')}, Data: {DataProducao:dd/MM/yyyy}, Quantidade: {Quantidade}";
        }
        public string SalvarArquivo()
        {
            return $"{this.ID:D5}{this.DataProducao:ddMMyyyy}{this.Quantidade:D4}";
        }


    }
}
