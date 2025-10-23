using Microsoft.VisualBasic;
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
                int somaTotal = somaImpar + (somaPar * 3);
                int digitoVerificador = 10 - (somaTotal % 10);
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
            bool validadeCDB = false, validadeNome = false, validadeCategoria = false, validadeSituacao = false;
            string nome = "";
            char categoria = ' ', situacao = ' ';

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
                    Exceptions.GeneralException.VerificarTamanhoMenorString(nome, 3, "O nome do medicamento deve ter no mínimo 3 caracteres.");
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
                string msg = "O valor de venda deve ser maior que R$0,00 e menor que R$10000,00";
                try
                {
                    Exceptions.GeneralException.VerificarQuantidadeInvalidaDecimal(0, 10000, valorVenda, msg);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while ();

            Console.WriteLine("Digite a data da ultima venda (DD/MM/AAAA): ");
            DateOnly ultimaVenda = DateOnly.Parse(Console.ReadLine());

            Console.WriteLine("Digite a data do cadastro do medicamento (DD/MM/AAAA): ");
            DateOnly dataCadastro = DateOnly.Parse(Console.ReadLine());

            do
            {
                Console.WriteLine("Digite a situação do medicamento (A, I): ");
                validadeSituacao = char.TryParse(Console.ReadLine().ToUpper(), out situacao);
                if (validadeSituacao == false)
                {
                    Console.WriteLine("");
                }
            } while (situacao != 'A' || situacao != 'I');



        }

    }
}
