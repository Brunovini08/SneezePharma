using SneezePharma.Enums;
using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Customer_Manipulation_
{
    public class CustomerManipulate : ArchiveManipulator<CustomerModel>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private string FilePath { get; set; } = "Customers.data";

        public CustomerManipulate()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }

        public override void Gravar(List<CustomerModel> customers)
        {
            var sw = new StreamWriter(FullPath(DirectoryPath, FilePath));

            using (sw)
            {
                foreach (var si in customers)
                {
                    sw.WriteLine(si.SalvarArquivo());
                }
                sw.Close();
            }
        }
        public override List<CustomerModel> Ler()
        {
            var customerList = new List<CustomerModel>();
            string fullPath = FullPath(DirectoryPath, FilePath);
            var sr = new StreamReader(fullPath);

            using (sr)
            {
                if (File.ReadAllLines(fullPath).Length > 0)
                {

                    while (sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var cpf = contentLine[0..10];
                        var nome = contentLine[11..49];
                        var telefone = contentLine[50..60];
                        var dataNascimento = contentLine[61..69];
                        var dataUltimaCompra = contentLine[70..77];
                        var dataCadastro = contentLine[78..85];
                        var situacao = contentLine[86];
                        if (situacao.ToString() == "A")
                            situacao = (char)SituationCustomer.A;
                        else
                            situacao = (char)SituationCustomer.I;

                        var customer = new CustomerModel(cpf, nome, DateOnly.Parse(dataNascimento), telefone, DateOnly.Parse(dataUltimaCompra), DateOnly.Parse(dataCadastro), (SituationCustomer)situacao);
                        customerList.Add(customer);
                    }
                }
                sr.Close();
            }

            return customerList;
        }
    }
}
