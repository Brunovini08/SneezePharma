using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public static class SalesItemManipulate
    {
        private static string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private static string FilePath { get; set; } = "SaleItems.data";
        
        private static string FullPath()
        {
            return Path.Combine(this.DirectoryPath, this.FilePath);
        }

        private static bool ChecarSeDiretorioNaoExiste()
        {
            return !Directory.Exists(this.DirectoryPath);
        }

        private static bool ChecarSeArquivoNaoExiste()
        {
            return !Path.Exists(FullPath());
        }

        public static void CriarArquivo()
        {
            if (ChecarSeDiretorioNaoExiste())
            {
                Directory.CreateDirectory(this.DirectoryPath);
            }
            if (ChecarSeArquivoNaoExiste())
            {
                StreamWriter sw = new StreamWriter(FullPath());
                sw.Close();
            }
        }

        public static List<SalesItemModel> LerItensDeVenda()
        {
            var salesItemsLidos = new List<SalesItemModel>();
            string fullPath = FullPath();
            var sr = new StreamReader(fullPath);

            using (sr)
            {
                if (File.ReadAllLines(fullPath).Length > 0)
                {
                    while (sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var id = contentLine[0..5];
                        var idVenda = contentLine[5..10];
                        var medicamento = contentLine[10..23];
                        var quantidade = contentLine[23..26];
                        var valorUtilitario = contentLine[26..33];
                        var totalItem = contentLine[33..41];

                        salesItemsLidos.Add(new SalesItemModel(
                            int.Parse(id),
                            int.Parse(idVenda),
                            medicamento,
                            int.Parse(quantidade),
                            decimal.Parse(valorUtilitario),
                            decimal.Parse(totalItem)
                            ));
                    }
                }
                sr.Close();
            }

            return salesItemsLidos;
        }

        public static void GravarItensDeVenda(List<SalesItemModel> salesItems)
        {
            var sw = new StreamWriter(FullPath());
            
            using (sw)
            {
                foreach (var si in salesItems)
                {
                    sw.WriteLine(si.SalvarArquivo());
                }
                sw.Close();
            }
        }
    }
}
