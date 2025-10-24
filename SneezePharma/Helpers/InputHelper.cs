using System;
using System.Collections.Generic;
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

        public static decimal RetornarNumeroDecimal(string msgDeInput)
        {
            var repetir = true;
            var numero = 0.0m;

            do
            {
                Console.Write(msgDeInput);
                repetir = decimal.TryParse(Console.ReadLine(), out numero);

                if (!repetir)
                {
                    InputHelper.ExibirErro();
                    InputHelper.PressioneEnterParaContinuar();
                }
            }
            while (!repetir);
            return numero;
        }

        public static string RetornarString(string msgDeInput, string msgAviso)
        {
            string nome;
            do
            {
                Console.WriteLine();
                Console.Write(msgDeInput);
                nome = Console.ReadLine();
                if (nome == null || nome == string.Empty)
                {
                    InputHelper.ExibirErro();
                    InputHelper.PressioneEnterParaContinuar();
                }
            } while (nome == null || nome == string.Empty);

            return nome;
        }

        public static void ExibirErro()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Entrada inválida! Tente novamente!");
            Console.ResetColor();
        }

        public static DateOnly RetornarData(string msgDeInput, string msgAviso)
        {
            DateOnly data;
            do
            {
                Console.WriteLine();
                Console.WriteLine(msgDeInput);
                data = DateOnly.Parse(Console.ReadLine());
                if (data == null || Convert.ToString(data) == string.Empty)
                {
                    InputHelper.ExibirErro();
                    InputHelper.PressioneEnterParaContinuar();
                }
            } while (data == null || Convert.ToString(data) == string.Empty);

            return data;
        }

    }
}
