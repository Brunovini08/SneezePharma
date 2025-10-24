using SneezePharma.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public char Situacao { get; private set; } 

        public Supplier(string cnpj, string razaoSocial, 
            string pais, DateOnly dataAbertura, 
            DateOnly ultimoFornecimento, DateOnly dataCadastro, char situacao)
        {
            GeneralException.VerificarTamanhoDiferente(cnpj, 14, "O Cnpj deve ter 14 caracteres");
            GeneralException.VerificarTamanhoMaiorString(razaoSocial, 50, "A razão social deve ter até 50 caracteres");
            GeneralException.VerificarTamanhoMaiorString(pais, 50, "O país deve ter até 20 caracteres");

            this.Cnpj = cnpj;
            this.RazaoSocial = razaoSocial;
            this.Pais = pais;
            this.DataAbertura = dataAbertura;
            this.UltimoFornecimento = ultimoFornecimento;
            this.DataCadastro = dataCadastro;
            this.Situacao = situacao;
        }


        List<Supplier> suppliers = new List<Supplier>();

        int[] pesoVerificador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] pesoVerificador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        public char PrincipioAtivo(char Situacao)
        {
            do
            {
                Console.WriteLine("Digite a situação: (A ou I)");
                string TipoDaSituacao = Console.ReadLine();
                if (TipoDaSituacao.Length == 1 && (TipoDaSituacao[0] == 'A' || TipoDaSituacao[0] == 'I'))
                {
                    Situacao = TipoDaSituacao[0];
                }
                else
                {
                    Situacao = ' ';
                }
            } while (Situacao != 'A' && Situacao != 'I');
            return Situacao;
        }
        public int [] TransformarCnpjEmInt(string Cnpj)
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


            for (int i = 0; i < pesoVerificador1.Length ; i++)
            {
                soma += numeros[i] * pesoVerificador1[i];
                Console.WriteLine(soma);
            }
    
            resto = soma % 11;
            int digitoVerificador1 = 0;
            int digitoVerificador2 = 0;
            
            if(resto == 0 || resto == 1)
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


        public void Adicionar(List<Supplier> suppliers)
        {

            Console.WriteLine("Digite o Cnpj com 14 dígitos (Apenas numeros e sem caracteres especiais) ");
            Cnpj = Console.ReadLine();
            Console.WriteLine("Digite a razão social (até 50 caracteres)");
            RazaoSocial = Console.ReadLine();
            Console.WriteLine("Digite o país: ");
            Pais = Console.ReadLine();
            Console.WriteLine("Digite a data de abertura (no modelo: DDMMAAAA):");
            DataAbertura = DateOnly.Parse(Console.ReadLine());
            Console.WriteLine("Digite a data do ultimo fornecimento (no modelo: DDMMAAAA):");
            UltimoFornecimento = DateOnly.Parse(Console.ReadLine());
            Console.WriteLine("Digite a data do cadastro (no modelo: DDMMAAAA):");
            DataCadastro = DateOnly.Parse(Console.ReadLine());
            PrincipioAtivo(Situacao);



            this.suppliers.Add(new Supplier(Cnpj, RazaoSocial,
            Pais, DataAbertura,
            UltimoFornecimento, DataCadastro, Situacao));
        }

        public Supplier Localizar(string Cnpj)
        {
            return this.suppliers.Find(c => c.Cnpj == Cnpj);
        }

        public void atualizarRazaoSocial()
        {
            Console.WriteLine("Digite o Cnpj que deseja alterar a razão social:");
            var Cnpj = Console.ReadLine();
            var Supplier = Localizar(Cnpj);

            if(Supplier is null)
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
        public override string ToString()
        {
            return $"Cnpj: {Cnpj},Razão social: {RazaoSocial},País: {Pais},Data abertura: {DataAbertura}," +
                $"Ultimo fornecimento: {UltimoFornecimento},Data cadastro: {DataCadastro},Situação: {Situacao}";
        }
        
    }
}
