using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Customer_Manipulation_
{
    public class CustomerManipulate : ArchiveManipulator<Customer>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private string FilePath { get; set; } = "Customers.data";

        public CustomerManipulate()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }

        public override void Gravar(List<Customer> customers)
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

        public override List<Customer> Ler()
        {
            throw new NotImplementedException();
        }
    }
}
