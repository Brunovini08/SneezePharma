using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Produce
{
    public class ProduceItemManipulate : ArchiveManipulator<ProduceItemModel>
    {
        private static string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private static string FilePath { get; set; } = "ProduceItem.data";
        public ProduceItemManipulate()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }
        public override List<ProduceItemModel> Ler()
        {


            var manipulationitensLidos = new List<ProduceItemModel>();
            string fullPath = FullPath(DirectoryPath, FilePath);
            var sr = new StreamReader(fullPath);

            using (sr)
            {
                if (File.ReadAllLines(fullPath).Length > 0)
                {
                    while (sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var id = contentLine[0..5];
                        var idprod = contentLine[5..10];
                        var medicamento = contentLine[10..16];
                        var quantidade = contentLine[16..20];

                        manipulationitensLidos.Add(new ProduceItemModel(
                            int.Parse(id),
                            int.Parse(idprod),
                            medicamento,
                            int.Parse(quantidade)
                        ));
                    }
                }
                sr.Close();
            }

            return manipulationitensLidos;
        }

        public override void Gravar(List<ProduceItemModel> manipulation)
        {
            var sw = new StreamWriter(FullPath(DirectoryPath, FilePath));

            using (sw)
            {
                foreach (var si in manipulation)
                {
                    sw.WriteLine(si.SalvarArquivo());
                }
                sw.Close();
            }
        }
    }
}
