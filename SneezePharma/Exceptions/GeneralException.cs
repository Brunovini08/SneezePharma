using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Exceptions
{
    public static class GeneralException
    {
        public static void VerificarTamanhoMenorString(string valor, int limite, string msg)
        {
            if(valor.Length > limite)
            {
                throw new ArgumentException(msg);
            }
        }

        public static void VerificarTamanhoMaiorString(string valor, int limite, string msg)
        {
            if (valor.Length > limite)
            {
                throw new ArgumentException(msg);
            }
        }

        public static void VerificarQuantidadeInvalidaInteiro(int valorMin, int valorMax, int valorInserido, string msg)
        {
            if (valorInserido < valorMin || valorInserido > valorMax)
                throw new ArgumentException(msg);
        } 
    }
}
