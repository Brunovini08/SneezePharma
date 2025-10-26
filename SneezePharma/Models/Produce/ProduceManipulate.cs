using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Produce.Produce
{
    public class ProduceManipulate : ArchiveManipulator<ProduceModel>
    {
        private static string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private static string FilePath { get; set; } = "Produce.data";
        public ProduceManipulate() 
        {
            CriarArquivo(DirectoryPath, FilePath);
        }
        public override List<ProduceModel> Ler()
        {


            var producaoLista = new List<ProduceModel>();
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
                        var dataProd = contentLine[5..14];
                        var medicamento = contentLine[14..23];
                        var quantidade = contentLine[23..26];

                        producaoLista.Add(new ProduceModel(
                            int.Parse(id),
                            DateOnly.Parse(dataProd),
                            int.Parse(quantidade)
                            ));
                    }
                }
                sr.Close();
            }

            return producaoLista;
        }

        public void GravarManipulacao(List<ProduceModel> manipulacao)
        {
            var sw = new StreamWriter(FullPath(DirectoryPath, FilePath));

            using (sw)
            {
                foreach (var si in manipulacao)
                {
                    sw.WriteLine(si.SalvarArquivo());
                }
                sw.Close();
            }
        }

        public override void Gravar(List<ProduceModel> lista)
        {
            throw new NotImplementedException();
        }
    }
}
