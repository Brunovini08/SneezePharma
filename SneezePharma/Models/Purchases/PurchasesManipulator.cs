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
                    while(sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var idPurchase = contentLine[0..]
                    }
                }
            }
        }
    }
}
