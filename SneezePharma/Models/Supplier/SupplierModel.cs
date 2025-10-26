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

        List<SupplierModel> suppliers = new List<SupplierModel>();

        public int[] TransformarCnpjEmInt(string Cnpj)
        {
            int[] numeros = new int[Cnpj.Length];
            for (int i = 0; i < numeros.Length; i++)
            {
                numeros[i] = int.Parse(Cnpj[i].ToString());
            }
            return numeros;
        }

        public void AdicionarFornecedor(List<SupplierModel> suppliers)
        {
            try
            {
                string cnpj;
                string razaoSocial;
                string pais;
                DateOnly dataAbertura;
                do
                {
                    cnpj = InputHelper.RetornarString("Digite o Cnpj com 14 dígitos (Apenas numeros e sem caracteres especiais) ", "Cnpj inválido, digite novamente:");
                } while (cnpj.Length != 14);
                do
                {
                    razaoSocial = InputHelper.RetornarString("Digite a razão social (até 50 caracteres)", "Razao saocial estorou o limite, digite novamente com até 50 caracteres: ");
                } while (razaoSocial.Length >= 50 || razaoSocial.Length <= 0);
                do
                {
                    pais = InputHelper.RetornarString("Digite o país: ", "o nome do país deve ter até 20 caracteres");
                } while (pais.Length >= 20);
                do {
                    dataAbertura = InputHelper.RetornarData("Digite a data de abertura (no modelo: DDMMAAAA): ", "Data de abertura inválida");
                } while (dataAbertura == null || dataAbertura.ToString() == string.Empty);
                this.suppliers.Add(new SupplierModel(Cnpj, RazaoSocial,
                Pais, DataAbertura));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public SupplierModel LocalizarFornecedor(string Cnpj)
        {
            return suppliers.Find(c => c.Cnpj == Cnpj);
        }

        public void setRazaoSocial(string razaoSocial)
        {
            this.RazaoSocial = razaoSocial;
        }

        public void AtualizarRazaoSocial()
        {
            Console.WriteLine("Digite o Cnpj que deseja alterar a razão social:");
            var Cnpj = Console.ReadLine();
            var Supplier = LocalizarFornecedor(Cnpj);
            
            if (Supplier is null)
            {
                Console.WriteLine("Fornecedor não encontrado");
            }
            else
            {
                Console.WriteLine("Digite a nova razão social: ");
                var novaRazaoSocial = Console.ReadLine();
                Supplier.RazaoSocial = novaRazaoSocial;
                Console.WriteLine(novaRazaoSocial);
            }
        }

        public void AlterarSituacao(List<SupplierModel> suppliers, char Situacao)
        {

            Console.WriteLine("Digite o Cnpj que deseja alterar a Situacao: ");
            var Cnpj = Console.ReadLine();
            LocalizarFornecedor(Cnpj);

            if (Cnpj != null)
            {
                Console.WriteLine("Digite a nova situação: ");
                Situacao = char.Parse(Console.ReadLine());
            }
        }

        public void AlterarDataUltimoFornecimento(List<SupplierModel> suppliers, DateOnly UltimoFornecimento)
        {
            Console.WriteLine("Digite o Cnpj que deseja alterar a data do ultimo fornecimento: ");
            var Cnpj = Console.ReadLine();
            LocalizarFornecedor(Cnpj);

            if(Cnpj != null)
            {
                Console.WriteLine("Digite a nova data de fornecimento no formato DDMMAAAA: ");
                UltimoFornecimento = DateOnly.Parse(Console.ReadLine());
            }
        }

        public override string ToString()
        {
            return $"Cnpj: {Cnpj},Razão social: {RazaoSocial},País: {Pais},Data abertura: {DataAbertura}," +
                $"Ultimo fornecimento: {UltimoFornecimento},Data cadastro: {DataCadastro},Situação: {Situacao}";
        }

    }
}
