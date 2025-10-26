using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Sales
{
    public class SalesManipulate : ArchiveManipulator<SalesModel>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private string FilePath { get; set; } = "Sales.data";

        public override void Gravar(List<SalesModel> lista)
        {
            var sw = new StreamWriter(FullPath(DirectoryPath, FilePath));

            using (sw)
            {
                foreach (var s in lista)
                {
                    sw.WriteLine(s.SalvarArquivo());
                }
                sw.Close();
            }
        }

        public override List<SalesModel> Ler()
        {
            CriarArquivo(DirectoryPath, FilePath);
            var listaDeVendas = new List<SalesModel>();
            var sr = new StreamReader(FullPath(DirectoryPath, FilePath));

            using (sr)
            {
                if (File.ReadAllLines(FullPath(DirectoryPath, FilePath)).Length > 0)
                {
                    while (sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var id = contentLine[0..5];
                        var dataVenda = contentLine[5..13];
                        var clienteCpf = contentLine[13..24];
                        var valorTotal = contentLine[24..34];

                        listaDeVendas.Add(new SalesModel(
                            int.Parse(id),
                            DateOnly.Parse($"{dataVenda[0..2]}/{dataVenda[2..4]}/{dataVenda[4..8]}"),
                            clienteCpf,
                            decimal.Parse(valorTotal)
                        ));
                    }
                }
                sr.Close();
            }

            return listaDeVendas;
        }
    }
}
