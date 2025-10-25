using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Produce
{
    public class FileProduce
    {
        private static string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private static string FilePath { get; set; } = "Produce.data";
        public FileProduce() { }
        private static string FullPath()
        {
            return Path.Combine(DirectoryPath, FilePath);
        }

        private static bool ChecarSeDiretorioNaoExiste()
        {
            return !Directory.Exists(DirectoryPath);
        }

        private static bool ChecarSeArquivoNaoExiste()
        {
            return !Path.Exists(FullPath());
        }

        public static void CriarArquivo()
        {
            if (ChecarSeDiretorioNaoExiste())
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            if (ChecarSeArquivoNaoExiste())
            {
                StreamWriter sw = new StreamWriter(FullPath());
                sw.Close();
            }
        }

        public static List<Produce> LerManipulacao()
        {
            var manipulacaolida = new List<Produce>();
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
                        var dataProd = contentLine[5..13];
                        var quantidade = contentLine[13..20];

                        manipulacaolida.Add(new Produce (
                            int.Parse(id),
                           DateOnly.Parse (dataProd),
                            int.Parse(quantidade)
                            ));
                    }
                }
                sr.Close();
            }

            return manipulacaolida;
        }

        public void GravarManipulacao(List<Produce> manipulacao)
        {
            string Fullpath = FullPath();
            StreamWriter sw = new StreamWriter(Fullpath, true);

            using (sw)
            {
                foreach (var si in manipulacao)
                {
                    sw.WriteLine(si.SalvarArquivo());
                }
                sw.Close();
            }
        }
    }
}
