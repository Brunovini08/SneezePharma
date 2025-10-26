using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Medicine
{
    public class MedicineManipulate : ArchiveManipulator<MedicineModel>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";

        private string FilePath { get; set; } = "Medicine.data";

        public MedicineManipulate()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }

        public override List<MedicineModel> Ler()
        {
            var medicineLidos = new List<MedicineModel>();
            string fullPath = FullPath(DirectoryPath, FilePath);
            var sr = new StreamReader(fullPath);

            using (sr)
            {
                if(File.ReadAllLines(fullPath).Length > 0)
                {
                    while(sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var cdb = contentLine[0..13];
                        var nome = contentLine[13..53];
                        var categoria = contentLine[53..54];
                        var valorVenda = contentLine[54..61];
                        var ultimaVenda = contentLine[61..69];
                        var dataCadastro = contentLine[69..77];
                        var situacao = contentLine[77..78];

                        medicineLidos.Add(new MedicineModel(
                            cdb,
                            nome,
                            char.Parse(categoria),
                            decimal.Parse(valorVenda),
                            DateOnly.Parse(ultimaVenda),
                            DateOnly.Parse(dataCadastro),
                            char.Parse(situacao)
                            ));
                            
                    }
                }
                sr.Close();
            }
            return medicineLidos;
        }

        public override void Gravar(List<MedicineModel> medicine)
        {
            var sw = new StreamWriter(FullPath(DirectoryPath, FilePath));

            using (sw)
            {
                foreach(var m in medicine)
                {
                    sw.WriteLine(m.SalvarArquivo());
                }
                sw.Close();
            }
        }
    }
}
