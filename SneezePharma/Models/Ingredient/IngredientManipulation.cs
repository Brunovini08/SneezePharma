using SneezePharma.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Ingredient
{
    public class IngredientManipulation : ArchiveManipulator<IngredientModel>
    {
        private string DirectoryPath { get; set; } = @"\Arquivos\SneezePharma\";
        private string FilePath { get; set; } = "IngredientManipulation.data";

        public IngredientManipulation()
        {
            CriarArquivo(DirectoryPath, FilePath);
        }

        public override void Gravar(List<IngredientModel> ingredientes)
        {
            var sw = new StreamWriter(FullPath(DirectoryPath, FilePath));

            using (sw)
            {
                foreach (var ingrediente in ingredientes)
                {
                    sw.WriteLine(ingrediente.SalvarArquivo());
                }
                sw.Close();
            }
        }

        public override List<IngredientModel> Ler()
        {
            var ingredientes = new List<IngredientModel>();
            string fullPath = FullPath(DirectoryPath, FilePath);
            var sr = new StreamReader(fullPath);

            using (sr)
            {
                if (File.ReadAllLines(fullPath).Length > 0)
                {
                    while (sr.Peek() != -1)
                    {
                        var contentLine = sr.ReadLine();

                        var id = contentLine[0..6];
                        var nome = contentLine[6..26];
                        var ultimaCompra = contentLine[26..34];
                        var dataCadastro = contentLine[34..42];
                        var situacao = contentLine[42..46];

                        ingredientes.Add(new IngredientModel(
                            int.Parse(id),
                            nome,
                            DateOnly.Parse(ultimaCompra)
                            ));
                    }
                }
                sr.Close();
            }

            return ingredientes;
        }

        
    }
}
