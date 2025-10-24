using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class Medicine
    {
        public string CDB { get; private set; }
        public string Nome { get; private set; }
        public char Categoria { get; private set; }
        public decimal ValorVenda { get; private set; }
        public DateOnly UltimaVenda { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public char Situacao { get; private set; }

        public Medicine(string cdb, string nome, char categoria, decimal valorVenda, DateOnly ultimaVenda, DateOnly dataCadastro, char situacao)
        {
            CDB = cdb;
            Nome = nome;
            Categoria = categoria;
            ValorVenda = valorVenda;
            UltimaVenda = ultimaVenda;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }

        public bool ValidarCDB(string cdb)
        {
            int[] resultado = new int[13];
            resultado = cdb.Select(c => int.Parse(c.ToString())).ToArray();
            if (resultado[0] != 7 || resultado[1] != 8 || resultado[2] != 9)
            {
                Console.WriteLine("Os três primeiros dígitos devem ser especificamente: 789");
                return false;
            }
            else
            {
                int somaImpar = 0, somaPar = 0;
                for (int i = 0; i < 12; i++)
                {
                    if (i % 2 == 0)
                    {
                        somaImpar += resultado[i];
                    }
                    else
                    {
                        somaPar += resultado[i];
                    }
                }
                int somaTotal = somaImpar + somaPar * 3;
                int digitoVerificador = 10 - somaTotal % 10;
                if (digitoVerificador == 10)
                {
                    digitoVerificador = 0;
                }
                if (digitoVerificador == resultado[12])
                {
                    Console.WriteLine("Código de Barra válido");
                    return true;
                }
                else
                {
                    Console.WriteLine("Dígito verificador inválido.");
                    return false;
                }
            }
        }

        public void CriarMedicamento()
        {
            bool validadeCDB = false, validadeNome = false, validadeCategoria = false;
            string nome = "";
            char categoria = ' ';

            do
            {
                Console.WriteLine("Digite o código de barras do medicamento:");
                string cdb = Console.ReadLine();
                validadeCDB = ValidarCDB(cdb);
            } while (!validadeCDB);
            do
            {
                do
                {
                    Console.WriteLine("Digite o nome do medicamento:");
                    nome = Console.ReadLine();
                    validadeNome = nome.All(c => char.IsLetterOrDigit(c));
                    if (validadeNome == false)
                    {
                        Console.WriteLine("O nome do medicamento deve conter apenas letras e números.");
                    }
                } while (!validadeNome);
                try
                {
                    Exceptions.GeneralException.VerificarTamanhoMaiorString(nome, 40, "O nome do medicamento deve ter no máximo 100 caracteres.");
                    Exceptions.GeneralException.VerificarTamanhoMenorString(nome, 0, "O nome do medicamento deve ter no mínimo 3 caracteres.");
                    Nome = nome;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (Nome == "");

            do
            {
                Console.WriteLine("Digite a categoria do medicamento (A , B, I, V):");
                validadeCategoria = char.TryParse(Console.ReadLine().ToUpper(), out categoria);

                if (validadeCategoria == false)
                {
                    Console.WriteLine("O medicamento deve estar em uma das 4 categorias");
                }
            } while (categoria != 'A' || categoria != 'B' || categoria != 'I' || categoria != 'V');

            do
            {
                Console.WriteLine("Digite o valor de Venda:");
                decimal valorVenda = decimal.Parse(Console.ReadLine());
                try
                {
                    Exceptions.GeneralException.
                }
            }while()




        }

    }
}
