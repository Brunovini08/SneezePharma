using SneezePharma.Helpers;

namespace SneezePharma.Models.Supplier_Manipulation_
{
    public class SupplierManipulate : ArchiveManipulator<SupplierModel>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private string FilePath { get; set; } = "Supplier.data";

        public SupplierManipulate()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }
        public override void Gravar(List<SupplierModel> suppliers)
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

        public override List<SupplierModel> Ler()
        {
            var suppliers = new List<SupplierModel>();
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
                        var ultimoFornecimento = contentLine[84..88];
                        var dataAbertura = contentLine[88..92];
                        var dataCadastro = contentLine[92..96];
                        var situacao = contentLine[96..100];

                        suppliers.Add(new SupplierModel(
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
