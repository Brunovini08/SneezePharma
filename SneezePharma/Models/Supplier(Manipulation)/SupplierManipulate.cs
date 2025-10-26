using SneezePharma.Enums;
using SneezePharma.Helpers;
using SneezePharma.Models.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Supplier_Manipulation_
{
    public class SupplierManipulate : ArchiveManipulator<Supplier>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private string FilePath { get; set; } = "Supplier.data";

        public SupplierManipulate()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }
        public override void Gravar(List<Supplier> suppliers)
        {
            using var sw = new StreamWriter(FullPath(DirectoryPath, FilePath)); 
            using (sw)
            {
                foreach (var supplier in suppliers)
                {
                    sw.WriteLine(supplier.SalvarArquivo());
                }
                sw.Close();
            }
        }

        public override List<Supplier> Ler()
        {
            var suppliers = new List<Supplier>();
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
                        var razaoSocial = contentLine[14..64];
                        var pais = contentLine[64..84];
                        var ultimoFornecimento = contentLine[84..92];
                        var dataAbertura = contentLine[92..100];
                        var dataCadastro = contentLine[100..108];
                        var situacao = contentLine[108..116];

                        suppliers.Add(new Supplier(
                            cnpj,
                            razaoSocial,
                            pais,
                            DateOnly.Parse(dataAbertura)
                            ));
                    }
                }
                sr.Close();
            }

            return suppliers;
        }
    }
}
