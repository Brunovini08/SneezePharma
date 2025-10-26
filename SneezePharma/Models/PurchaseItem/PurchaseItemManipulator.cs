using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.PurchaseItem
{
    public class PurchaseItemManipulator : ArchiveManipulator<PurchaseItemModel>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private string FilePath { get; set; } = @"PurchaseItem.data";

        public PurchaseItemManipulator()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }

        public override List<PurchaseItemModel> Ler()
        {
            var purchaseItemsLidos = new List<PurchaseItemModel>();
            string fullPath = FullPath(DirectoryPath, FilePath);
            var sr = new StreamReader(fullPath);

            using (sr)
            {
                if(File.ReadAllLines(fullPath).Length > 0)
                {
                    while(sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var idCompraItem = contentLine[0..5];
                        string prefixo = contentLine[5..7];
                        var ingrediente = contentLine[7..11];
                        var quantidade = contentLine[11..18];
                        var valorUnitario = contentLine[18..24];
                        var totalItem = contentLine[24..35];

                        purchaseItemsLidos.Add(new PurchaseItemModel(
                            int.Parse(idCompraItem),
                            prefixo + decimal.Parse(ingrediente),
                            decimal.Parse(quantidade),
                            decimal.Parse(valorUnitario),
                            decimal.Parse(totalItem)
                            ));
                     
                    }
                }
                sr.Close();
            }
            return purchaseItemsLidos;
        }
        public override void Gravar(List<PurchaseItemModel> purchaseItem)
        {
            var sw = new StreamWriter(FullPath(DirectoryPath, FilePath));

            using (sw)
            {
                foreach(var pi in purchaseItem)
                {
                    sw.WriteLine(pi.SalvarArquivo());
                }
                sw.Close();
            }
        }
    }
}
