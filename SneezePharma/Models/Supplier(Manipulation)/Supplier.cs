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
    public class Supplier
    {
        public string Cnpj { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Pais { get; private set; }
        public DateOnly DataAbertura { get; private set; }
        public DateOnly UltimoFornecimento { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public  SituationSupplier Situacao { get; private set; } 

        public Supplier(string cnpj, string razaoSocial,
            string pais, DateOnly dataAbertura,
            DateOnly ultimoFornecimento, DateOnly dataCadastro)
        {
            int[] pesoVerificador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] pesoVerificador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            bool validar = this.ValidarCnpj(Cnpj, pesoVerificador1, pesoVerificador2);
            if (validar)
            {
                this.Cnpj = cnpj;
            }
            else
            {
                GeneralException.VerificarTamanhoDiferente(Cnpj, 14, "Cnpj inválido");
            }

            this.RazaoSocial = razaoSocial;
            this.Pais = pais;
            this.DataAbertura = dataAbertura;
            this.UltimoFornecimento = ultimoFornecimento;
            this.DataCadastro = dataCadastro;
            this.Situacao = SituationSupplier.A;
        }

        List<Supplier> suppliers = new List<Supplier>();

        public int[] TransformarCnpjEmInt(string Cnpj)
        {
            int[] numeros = new int[Cnpj.Length];
            for (int i = 0; i < numeros.Length; i++)
            {
                numeros[i] = int.Parse(Cnpj[i].ToString());
            }
            return numeros;
        }


        public bool ValidarCnpj(string Cnpj, int[] pesoVerificador1, int[] pesoVerificador2)
        {
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


        public void AdicionarFornecedor(List<Supplier> suppliers)
        {
            try
            {
                string cnpj;
                string razaoSocial;
                string pais;
                DateOnly dataAbertura;
                DateOnly ultimoFornecimento;
                DateOnly dataCadastro;
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

                do {
                    ultimoFornecimento = InputHelper.RetornarData("Digite a data do ultimo fornecimento (no modelo: DDMMAAAA):", "Data do ultimo fornecimento inválida");
                } while (ultimoFornecimento == null || ultimoFornecimento.ToString() == string.Empty);


                do {
                    dataCadastro = InputHelper.RetornarData("Digite a data do cadastro (no modelo: DDMMAAAA):", "Data do ultimo cadastro inválida");
                } while (dataCadastro == null || dataCadastro.ToString() == string.Empty);



                this.suppliers.Add(new Supplier(Cnpj, RazaoSocial,
                Pais, DataAbertura,
                UltimoFornecimento, DataCadastro));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Supplier LocalizarFornecedor(string Cnpj)
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

        public void AlterarSituacao(List<Supplier> suppliers, char Situacao)
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

        public void AlterarDataUltimoFornecimento(List<Supplier> suppliers, DateOnly UltimoFornecimento)
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
