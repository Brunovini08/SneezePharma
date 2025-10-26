using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Produce.Produce
{
    public class FileProduce : ArchiveManipulator<ProduceModel>
    {
        private static string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private static string FilePath { get; set; } = "Produce.data";
        public FileProduce() 
        {
            CriarArquivo(DirectoryPath, FilePath);
        }
        public override List<ProduceModel> Ler()
        {


            var manipulationLidos = new List<ProduceModel>();
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

                        manipulationLidos.Add(new ProduceModel(
                            int.Parse(id),
                            DateOnly.Parse(dataProd),
                            int.Parse(quantidade)
                            ));
                    }
                }
                sr.Close();
            }

            return manipulationLidos;
        }

        public override void Gravar(List<ProduceModel> manipulation)
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
        public int GerarProximoID()
        {
            var lista = Ler();

            if (lista.Count == 0)
                return 1;

            int maiorId = 0;
            foreach (var si in lista)
            {
                if (si.ID > maiorId)
                    maiorId = si.ID;
            }
            return maiorId + 1;
        }
    }
}
