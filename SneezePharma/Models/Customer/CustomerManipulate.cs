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

                        var cpf = contentLine[0..11];
                        var nome = contentLine[11..61];
                        var dataNascimento = DateOnly.ParseExact(contentLine[61..69], "ddMMyyyy");
                        var telefone = contentLine[69..80];
                        var dataUltimaCompra = contentLine[80..88];
                        if (dataUltimaCompra == "00000000")
                        {
                            dataUltimaCompra = null;
                        }
                            var dataCadastro = DateOnly.ParseExact(contentLine[88..96], "ddMMyyyy");
                        var situacao = contentLine[96];
                        if (situacao.ToString() == "A")
                            situacao = (char)SituationCustomer.A;
                        else
                            situacao = (char)SituationCustomer.I;

                        var customer = new CustomerModel(
                            cpf,
                            nome,
                            dataNascimento,
                            telefone,
                            ultimaCompra: dataUltimaCompra != null ? DateOnly.Parse(dataUltimaCompra) : null,
                            dataCadastro,
                            (SituationCustomer)situacao);
                        customerList.Add(customer);
                    }
                }
                sr.Close();
            }

            return customerList;
        }
    }
}
