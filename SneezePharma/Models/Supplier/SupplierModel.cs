using SneezePharma.Enums;
using SneezePharma.Exceptions;
using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class SupplierModel
    {
        public string Cnpj { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Pais { get; private set; }
        public DateOnly DataAbertura { get; private set; }
        public DateOnly UltimoFornecimento { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public  SituationSupplier Situacao { get; private set; } 

        public SupplierModel(string cnpj, string razaoSocial,
            string pais, DateOnly dataAbertura)
        {
            this.DataCadastro = new DateOnly();
            this.UltimoFornecimento = new DateOnly();
            this.Cnpj = cnpj;
            this.RazaoSocial = razaoSocial;
            this.Pais = pais;
            this.DataAbertura = dataAbertura;
            this.Situacao = SituationSupplier.A;
        }

        public void setRazaoSocial(string razaoSocial)
        {
            this.RazaoSocial = razaoSocial;
        }
        public void setSituacao(SituationSupplier situacao)
        {
            this.Situacao = situacao;
        }
        public string SalvarArquivo()
        {
            var cnpj = this.Cnpj;
            var razaoSocial = this.RazaoSocial.ToString();
            razaoSocial = razaoSocial.PadRight(50, ' ');
            var pais = this.Pais.ToString();
            pais = pais.PadRight(20, ' ');
            var dataAbertura = this.DataAbertura.ToString();
            var ultimoFornecimento = this.UltimoFornecimento.ToString();
            var dataCadastro = this.DataCadastro.ToString();

            return $"{cnpj}{razaoSocial}{pais}{dataAbertura}{ultimoFornecimento}{dataCadastro}";
        }
        public override string ToString()
        {
            return $"Cnpj: {Cnpj},Razão social: {RazaoSocial},País: {Pais},Data abertura: {DataAbertura}," +
                $"Ultimo fornecimento: {UltimoFornecimento},Data cadastro: {DataCadastro},Situação: {Situacao}";
        }


    }
}
