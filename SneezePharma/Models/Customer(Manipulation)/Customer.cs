using SneezePharma.Enums;
using SneezePharma.Exceptions;
using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        public DateOnly? UltimaCompra { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public SituationCustomer Situacao { get; private set; }

        public Customer(string cpf, string nome, DateOnly dataNascimento, string telefone)
        {
            this.Nome = nome;
            this.ValidarCPF(cpf);
            this.ValidarIdade(dataNascimento);
            Telefone = telefone;
            DataCadastro = new DateOnly();
            Situacao = SituationCustomer.A;
        }

        public Customer(string CPF, string nome, DateOnly dataNascimento, string telefone, DateOnly? ultimaCompra, DateOnly dataCadastro, SituationCustomer situacao)
        {
            this.Nome = nome;
            this.CPF = CPF;
            this.Telefone = telefone;
            this.UltimaCompra = ultimaCompra;
            this.DataCadastro = dataCadastro;
            this.Situacao = situacao;
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
        
        private void ValidarIdade(DateOnly dataNascimento)
        {
            DateOnly dataAtual = new DateOnly();
            int idade = dataAtual.Year - dataNascimento.Year;
            if (idade >= 18)
            {
                this.DataNascimento = dataNascimento;
            }
            else
            {
                Console.WriteLine("Você tem menos de 18 anos, por isso não poderá comprar na SneezePharma");
            }
        }
        public string SalvarArquivo()
        {
            return $"{this.CPF}{this.Nome.PadRight(50, ' ')}{this.DataNascimento}{this.Telefone}{this.UltimaCompra}{this.DataCadastro}{this.Situacao}";
        }
    }
}
