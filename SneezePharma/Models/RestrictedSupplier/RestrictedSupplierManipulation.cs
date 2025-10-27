using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SneezePharma.Models;


namespace SneezePharma.Models
{
    public class RestrictedSupplierManipulation : ArchiveManipulator<RestrictedSupplierModel>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private string FilePath { get; set; } = "RestrictedSupplier.data";
        public RestrictedSupplierManipulation()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }


        public override void Gravar(List<RestrictedSupplierModel> restricteds)
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

        public override List<RestrictedSupplierModel> Ler()
        {
            var restricteds = new List<RestrictedSupplierModel>();
            string fullPath = FullPath(DirectoryPath, FilePath);
            var sr = new StreamReader(fullPath);

            using (sr)
            {
                if (File.ReadAllLines(fullPath).Length > 0)
                {

                    while (sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var cnpj = contentLine.Length >= 14 ? contentLine.Substring(0, 14) : contentLine;

                        restricteds.Add(new RestrictedSupplierModel(cnpj));
                    }
                }
                sr.Close();
            }

            return restricteds;
        }
    }
}

