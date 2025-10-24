using SneezePharma.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma
{
    public class Produce
    {
        private static int ultimoID = 1;

        public string ID { get; private set; }
        public DateOnly DataProducao { get; private set; }
        public string Medicamento { get; private set; }
        public int Quantidade { get; private set; }

        public Produce(string id, DateOnly dataproducao, string medicamento, int quantidade)
        {
            
            ID = id;
            DataProducao = dataproducao;
            Medicamento = medicamento;
            Quantidade = quantidade;
        }

        public Produce()
        {

        }
        

        public void ValidarID()
        {
            
            ID = ultimoID.ToString().PadLeft(5, '0');
            Console.WriteLine($"ID: " + ID);
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

        public Produce CadastrarManipulacao()
        {
            try
            {
      
                Console.WriteLine("Digite a data de produção (DD/MM/AAAA): ");
                string Data = Console.ReadLine();

                if (DateOnly.TryParseExact(Data, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out DateOnly data))
                {
                
                        Console.WriteLine("Formato da data errado. Use DD/MM/AAAA");
                       return null;
                    
                }

                if (!ValidarDataProducao(data))
                {
                    Console.WriteLine("Falha ao validar a data.");
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

                ultimoID++;
                Console.WriteLine("Manipulação Cadastrada!");
                Console.WriteLine($"ID: {ID}, Data: {data:dd/MM/yyy}, Quantidade {quantidade}");
                return new Produce();

            }
            catch (ArgumentException ex){

                Console.WriteLine(ex.Message);
                return null;
            
            }
            
        }
        public void MostrarManipulacao(List<Produce> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma manipulação cadastrada.");
                return;
            }
            Console.WriteLine("Manipulações cadastradas: ");
            foreach (var item in lista)
            {
                Console.WriteLine($"ID: {item.ID}, Data: {item.DataProducao: dd/MM/yyyy}");
            }
        }

    }
}
