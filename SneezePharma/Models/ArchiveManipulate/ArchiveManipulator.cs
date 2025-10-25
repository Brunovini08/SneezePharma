using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.ArchiveManipulate
{
    public abstract class ArchiveManipulator <T>
    {
        public void CriarArquivo(string diretorio, string arquivo)
        {
            var caminhoCompleto = Path.Combine(diretorio, arquivo);

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }
            if (!File.Exists(caminhoCompleto))
            {
                StreamWriter sw = new StreamWriter(caminhoCompleto);
                sw.Close();
            }
        }

        public abstract List<T> Ler();
        public abstract void Gravar(List<T> lista);
    }
}
