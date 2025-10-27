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
    public class CustomerModel
    {
        public string CPF { get; private set; }
        public string Nome { get; private set; }
        public DateOnly DataNascimento { get; private set; }
        public string Telefone { get; private set; }
        public DateOnly? UltimaCompra { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public SituationCustomer Situacao { get; private set; }

        public CustomerModel(string cpf, string nome, DateOnly dataNascimento, string telefone)
        {
            this.Nome = nome;
            this.CPF = cpf;
            this.ValidarIdade(dataNascimento);
            Telefone = telefone;
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = SituationCustomer.A;
        }

        public CustomerModel(string CPF, string nome, DateOnly dataNascimento, string telefone, DateOnly? ultimaCompra, DateOnly dataCadastro, SituationCustomer situacao)
        {
            this.Nome = nome;
            this.CPF = CPF;
            this.Telefone = telefone;
            if (ultimaCompra != null)
            {
                this.UltimaCompra = ultimaCompra;
            }
            this.DataCadastro = dataCadastro;
            this.Situacao = situacao;
            this.DataNascimento = dataNascimento;
        }
        
        private void ValidarIdade(DateOnly dataNascimento)
        {
            DateOnly dataAtual = new DateOnly();
            int idade = dataNascimento.Year - dataAtual.Year;
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
            return $"" +
                $"{this.CPF}" +
                $"{this.Nome.PadRight(50, ' ')}" +
                $"{this.DataNascimento.ToString("ddMMyyyy", CultureInfo.InvariantCulture)}" +
                $"{this.Telefone}" +
                $"{this.UltimaCompra?.ToString("ddMMyyyy", CultureInfo.InvariantCulture) ?? "00000000"}" +
                $"{this.DataCadastro.ToString("ddMMyyyy", CultureInfo.InvariantCulture)}{this.Situacao}";
        }

        public override string ToString()
        {
            return 
                $"CPF: {this.CPF}\n" +
                $"NOME: {this.Nome}\n" +
                $"DataNascimento: {this.DataNascimento}\n" +
                $"Telefone:{this.Telefone}\nData de Cadastro: {this.DataCadastro}\n" +
                $"Ultima Compra: {this.UltimaCompra}\n" +
                $"Situação: {this.Situacao}\n";
        }

        public void setNome(string nome)
        {
            this.Nome = nome;
        } 

        public void setTelefone(string telefone)
        {
            this.Telefone = telefone;
        }

        public void setSituacao(SituationCustomer situacao)
        {
            this.Situacao = situacao;
        }
    }
}
