using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.PurchaseItem
{
    public class PurchaseItemManipulate : ArchiveManipulator<PurchaseItemModel>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private string FilePath { get; set; } = @"PurchaseItem.data";

        public PurchaseItemManipulate()
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
                        var id = contentLine[0..5];
                        var idCompraItem = contentLine[5..10];
                        var ingrediente = contentLine[10..16];
                        var quantidade = contentLine[16..20];
                        var valorUnitario = contentLine[20..25];
                        var totalItem = contentLine[25..33];

                        purchaseItemsLidos.Add(new PurchaseItemModel(
                            int.Parse(id),
                            int.Parse(idCompraItem),
                            ingrediente,
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
