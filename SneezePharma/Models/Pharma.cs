using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class Pharma
    {
        public List<Customer> Clientes { get; private set; }
        public List<Supplier> Fonecedores { get; private set; }
        public List<Medicine> Medicamentos { get; private set; }
        public List<ProduceItem> ItemProducao { get; private set; }
        public List<RestrictedCustomer> ClientesRestritos { get; private set; }


        public void Cadastrar()
        {
            
        }
        public void VendasMedicamento()
        {

        }
        public void ComprarPrincipioAtivo()
        {

        }
        public void ManipularMedicamentos()
        {

        }
        public void ManipularClientes()
        {

        }
        public void ManipularFornecedores()
        {

        }
        public void ManipularPrincipioAtivo()
        {

        }
        public void Relatorios()
        {

        }

        //CUSTOMER
        
        public void RegistrarCliente()
        {
            try
            {
                Console.WriteLine("CADASTRANDO CLIENTE: ");
                Console.WriteLine();
                string cpf;
                do
                {
                    cpf = InputHelper.RetornarString("Digite o seu CPF (Sem pontuções): ", "Por favor, digite o CPF!");
                } while (cpf.Length != 13);

                Console.Clear();
                string nome;
                do
                {
                    nome = InputHelper.RetornarString("Digite seu nome: ", "Por favor, digite o nome!");
                } while (nome.Length > 50 || nome.Length <= 0);

                Console.Clear();
                DateOnly dataNascimento;
                do
                {

                    dataNascimento = InputHelper.RetornarData("Digite sua Data de Nascimento: ", "Por favor, digite a data de nascimento!");

                } while (dataNascimento == null || dataNascimento.ToString() == String.Empty);

                Console.Clear();
                string telefone;
                do
                {
                    telefone = InputHelper.RetornarString("Digite seu telefone com DDD: ", "Por favor, digite o número de telefone com DDD!");
                } while (telefone.Length != 11);
                Console.Clear();

                Customer customer = new Customer(cpf, nome, dataNascimento, telefone);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AtualizarCliente()
        {
            try
            {
                Console.WriteLine("ALTERANDO INFORMAÇÕES DO CLIENTE - Caso não queira modificar nenhuma informação, aperte ENTER nas propriedades");
                string nome;
                do
                {
                    nome = InputHelper.RetornarString("Digite seu nome: ", "Por favor, digite o nome!");
                } while (nome.Length > 50 || nome.Length <= 0);
                Console.Clear();
                string telefone;

                do
                {
                    telefone = InputHelper.RetornarString("Digite seu telefone com DDD: ", "Por favor, digite o número de telefon com DDD!");
                } while (telefone.Length != 11);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 
        public Customer BuscarPorId(string cpf)
        {
            return this.Clientes.Find(c => c.CPF == cpf);
        }
        public List<Customer> ListaClientes()
        {
            return this.Clientes.ToList();
        }

    }
}
