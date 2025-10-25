using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Utils
{
    public static class InputHelper
    {
        public static void PressioneEnterParaContinuar()
        {
            Console.WriteLine("Pressioner a tecla ENTER para continuar");
            Console.ReadLine();
        }

        public static string RetornarSomenteNumeros(string msgDeInput)
        {
            var repetir = true;
            var stringDeRetorno = "";
            do
            {
                Console.WriteLine(msgDeInput);
                stringDeRetorno = Console.ReadLine();

                repetir = long.TryParse(stringDeRetorno, out long x);

                if (!repetir)
                {
                    Console.WriteLine("Entrada inválida! Tente novamente!");
                    InputHelper.PressioneEnterParaContinuar();
                }
            }
            while (!repetir);

            return stringDeRetorno;
        }

        public static int RetornarNumeroInteiro(string msgDeInput)
        {
            var repetir = true;
            var numero = 0;

            do
            {
                Console.WriteLine(msgDeInput);
                repetir = int.TryParse(Console.ReadLine(), out numero);

                if (!repetir)
                {
                    Console.WriteLine("Entrada inválida! Tente novamente!");
                    InputHelper.PressioneEnterParaContinuar();
                }
            }
            while (!repetir);
            return numero;
        }

        public static decimal RetornarNumeroDecimal(string msgDeInput, string msgOutput)
        {
            var repetir = true;
            var numero = 0.0m;

            do
            {
                Console.Write(msgDeInput);
                repetir = decimal.TryParse(Console.ReadLine(), out numero);

                if (!repetir)
                {
                    InputHelper.ExibirErro(msgOutput);
                    InputHelper.PressioneEnterParaContinuar();
                }
            }
            while (!repetir);
            return numero;
        }

        public static string RetornarString(string msgDeInput, string msgOutput)
        {
            string nome;
            do
            {
                Console.WriteLine();
                Console.Write(msgDeInput);
                nome = Console.ReadLine();
                if (nome == null || nome == string.Empty)
                {
                    InputHelper.ExibirErro(msgOutput);
                    InputHelper.PressioneEnterParaContinuar();
                }
            } while (nome == null || nome == string.Empty);

            return nome;
        }

        public static void ExibirErro(string msgOutput)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msgOutput);
            Console.ResetColor();
        }

        public static string RetornarData(string msgDeInput, string msgAviso)
        {
            DateOnly data;
            bool verificar;
            try
            {
                do
                {
                    Console.WriteLine();
                    Console.WriteLine(msgDeInput);
                    verificar = DateOnly.TryParse(Console.ReadLine(), out data);
                    if (verificar == false)
                    {
                        throw new Exception("Data inválida!");
                        InputHelper.PressioneEnterParaContinuar();
                    }
                    if (data == null || Convert.ToString(data) == string.Empty)
                    {
                        InputHelper.PressioneEnterParaContinuar();
                    }
                } while (data == null || Convert.ToString(data) == string.Empty || verificar == false);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return data.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        }

        public static bool ValidarCpf(string CPF)
        {
            int[] verificadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] verificadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            int digito1;
            int digito2;
            string cpfValidado = CPF.Substring(0, CPF.Length - 2);
           if(CPF.Distinct().Count() == 1)
            {
                return false;
            }
            for (int i = 0; i < verificadores1.Length; i++)
            {
                soma += (int)Char.GetNumericValue(cpfValidado[i]) * verificadores1[i];
            }

            int resto = soma % 11;

            if (resto < 2)
            {
                digito1 = 0;
                cpfValidado = cpfValidado + digito1.ToString();
            }
            else if (resto >= 2)
            {
                digito1 = 11 - resto;
                cpfValidado = cpfValidado + digito1.ToString();
            }

            soma = 0;
            resto = 0;

            for (int i = 0; i < verificadores2.Length; i++)
            {
                soma += (int)Char.GetNumericValue(cpfValidado[i]) * verificadores2[i];
            }
            resto = soma % 11;
            if (resto < 2)
            {
                digito2 = 0;
                cpfValidado = cpfValidado + digito2.ToString();
            }
            else if (resto >= 2)
            {
                digito2 = 11 - resto;
                cpfValidado = cpfValidado + digito2.ToString();
                if (CPF != cpfValidado)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            if (CPF != cpfValidado)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int[] TransformarCnpjEmInt(string Cnpj)
        {
            int[] numeros = new int[Cnpj.Length];
            for (int i = 0; i < numeros.Length; i++)
            {
                numeros[i] = int.Parse(Cnpj[i].ToString());
            }
            return numeros;
        }
        public static bool ValidarCnpj(string Cnpj)
        {
            int[] pesoVerificador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] pesoVerificador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            int resto = 0;
            int[] numeros = TransformarCnpjEmInt(Cnpj);


            for (int i = 0; i < pesoVerificador1.Length; i++)
            {
                soma += numeros[i] * pesoVerificador1[i];
                Console.WriteLine(soma);
            }
            resto = soma % 11;

            int digitoVerificador1 = 0;
            int digitoVerificador2 = 0;

            if (resto == 0 || resto == 1)
            {
                digitoVerificador1 = 0;
            }
            else
            {
                digitoVerificador1 = 11 - resto;
            }
            numeros[12] = digitoVerificador1;

            soma = 0;
            resto = 0;

            for (int i = 0; i < pesoVerificador2.Length; i++)
            {
                soma += numeros[i] * pesoVerificador2[i];
            }

            resto = soma % 11;

            if (resto == 0 || resto == 1)
            {
                digitoVerificador2 = 0;
            }
            else
            {
                digitoVerificador2 = 11 - resto;
            }

            if (numeros[12] == digitoVerificador1 && numeros[13] == digitoVerificador2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
