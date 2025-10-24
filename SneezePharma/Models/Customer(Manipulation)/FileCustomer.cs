using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Customer_Manipulation_
{
    public static class FileCustomer
    {

        public static void Escrever(List<Customer> clientes, StreamWriter streamWriter)
        {
            using (streamWriter)
            {
                try
                {
                    foreach (var cliente in clientes.ToList())
                    {
                        streamWriter.WriteLine(cliente.SalvarArquivo());
                    }
                    streamWriter.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void Ler(StreamReader streamReader)
        {

        }
        public static void VerificandoDiretorio(string directoryPath, string fullPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }
            catch (Exception exeception)
            {
                Console.WriteLine(exeception.Message);
            }
        }

        public static void VerificandoArquivo(string fullPath)
        {
            try
            {
                if (!File.Exists(fullPath))
                {
                    File.Create(fullPath);
                }
            }
            catch (Exception exeception)
            {
                Console.WriteLine(exeception.Message);
            }
        }
    }
}
