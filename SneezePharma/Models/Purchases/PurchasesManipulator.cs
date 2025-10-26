using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Purchases
{
    public class PurchasesManipulator : ArchiveManipulator<PurchasesModel>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SheezePharma\";
        private string FilePath { get; set; } = "Purchases.data";

        public PurchasesManipulator()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }

        public override List<PurchasesModel> Ler()
        {
            var purchasesLidos = new List<PurchasesModel>();
            string fullPath = FullPath(DirectoryPath, FilePath);
            var sr = new StreamReader(fullPath);

            using (sr)
            {
                if(File.ReadAllLines(fullPath).Length > 0)
                {
                    while (sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var idPurchase = contentLine[0..5];
                        var dataCompra = contentLine[5..19];
                        var fornecedor = contentLine[19..27];
                        var valorTotal = contentLine[27..38];

                        purchasesLidos.Add(new PurchasesModel(
                            int.Parse(idPurchase),
                            fornecedor,
                            DateOnly.Parse(dataCompra),
                            decimal.Parse(valorTotal)
                            ));
                    }

                }
                sr.Close();
            }
            return purchasesLidos;
        }

        public override void Gravar(List<PurchasesModel> purchases)
        {
            var sw = new StreamWriter(FullPath(DirectoryPath, FilePath));

            using (sw)
            {
                foreach(var p in purchases)
                {
                    sw.WriteLine(p.SalvarArquivo());
                }
                sw.Close();
            }
        }
    }
}
