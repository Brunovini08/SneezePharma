using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.RestrictedCustomers
{
    public class RestrictedCustomerManipulate : ArchiveManipulator<RestrictedCustomerModel>
    {
        private static string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private static string FilePath { get; set; } = " RestrictedCustomers.data";

        public override void Gravar(List<RestrictedCustomerModel> restricteds)
        {
            using var sw = new StreamWriter(FullPath(DirectoryPath, FilePath));
            using (sw)
            {
                foreach (var restricted in restricteds)
                {
                    sw.WriteLine(restricted.SalvarArquivo());
                }
                sw.Close();
            }
        }


        public override List<RestrictedCustomerModel> Ler()
        {
            var restricteds = new List<RestrictedCustomerModel>();
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

                        restricteds.Add(new RestrictedCustomerModel(cpf));
                    }
                }
                sr.Close();
            }

            return restricteds;
        }
    }
}
