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
                        var dataUltimaCompra = contentLine[61..69];
                        if (dataUltimaCompra == "00000000")
                        {
                            dataUltimaCompra = null;
                        }
                        var dataCadastro = DateOnly.ParseExact(contentLine[69..77], "ddMMyyyy");
                        var situacao = contentLine[77];

                        medicineLidos.Add(new MedicineModel(
                            cdb,
                            nome,
                            char.Parse(categoria),
                            decimal.Parse(valorVenda),
                            ultimaVenda: dataUltimaCompra != null ? DateOnly.Parse(dataUltimaCompra) : null,
                            dataCadastro,
                            situacao
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
