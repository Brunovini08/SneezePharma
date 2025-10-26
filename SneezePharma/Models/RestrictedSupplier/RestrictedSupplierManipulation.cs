using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SneezePharma.Models;


namespace SneezePharma.Models
{
    public class RestrictedSupplierManipulation : ArchiveManipulator<RestrictedSupplier>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private string FilePath { get; set; } = "RestrictedSupplier.data";
        public RestrictedSupplierManipulation()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }


        public override void Gravar(List<RestrictedSupplier> restricteds)
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

        public override List<RestrictedSupplier> Ler()
        {
            var restricteds = new List<RestrictedSupplier>();
            string fullPath = FullPath(DirectoryPath, FilePath);
            var sr = new StreamReader(fullPath);

            using (sr)
            {
                if (File.ReadAllLines(fullPath).Length > 0)
                {

                    while (sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var cnpj = contentLine[0..14];
                       
                        restricteds.Add(new RestrictedSupplier(cnpj));
                    }
                }
                sr.Close();
            }

            return restricteds;
        }
    }
}

