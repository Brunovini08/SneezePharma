using SneezePharma.Enums;
using SneezePharma.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class Customer
    {

        public string CPF { get; private set; }
        public string Nome { get; private set; }
        public DateOnly DataNascimento { get; private set; }
        public string Telefone { get; private set; }
        public DateOnly UltimaCompra { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public SituationCustomer Situacao { get; private set; }

        public Customer(string cpf, string nome, DateOnly dataNascimento, string telefone, DateOnly ultimaCompra, SituationCustomer situacao)
        {
            this.Nome = nome;
            this.ValidarCPF(cpf);
            DataNascimento = dataNascimento;
            Telefone = telefone;
            UltimaCompra = ultimaCompra;
            DataCadastro = new DateOnly();
            Situacao = situacao;
        }



        private void ValidarCPF(string CPF)
        {
            int[] verificadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] verificadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            int digito1;
            int digito2;
            string cpfValidado = CPF.Substring(0, CPF.Length - 2);
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
                if (CPF != cpfValidado)
                {
                    Console.WriteLine("CPF Inválido");
                }
                else
                {
                    this.CPF = CPF;
                }
            }
            else if (resto >= 2)
            {
                digito2 = 11 - resto;
                cpfValidado = cpfValidado + digito2.ToString();
                if (CPF != cpfValidado)
                {
                    Console.WriteLine("CPF Inválido");
                }
                else
                {
                    this.CPF = CPF;
                }
            }

            Console.WriteLine(CPF);
            Console.WriteLine(cpfValidado);
        }
    }
}
