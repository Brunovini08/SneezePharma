using SneezePharma;
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
        public List<ProduceItem> ItemProducao { get; set; }
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
        #region Medicamento CRUD
        /*Medicamento CRUD*/
        public void CriarMedicamento()
        {
            bool validadeCDB = false, validadeNome = false, validadeCategoria = false, validadeSituacao = false;
            string cdb, nome = "";
            char categoria = ' ', situacao = ' ';

            do
            {
                Console.WriteLine("Digite o código de barras do medicamento:");
                cdb = Console.ReadLine();
                validadeCDB = Medicine.ValidarCDB(cdb);
            } while (!validadeCDB);
            do
            {
                do
                {
                    Console.WriteLine("Digite o nome do medicamento:");
                    nome = Console.ReadLine();
                    validadeNome = nome.All(c => char.IsLetterOrDigit(c));
                    if (validadeNome == false)
                    {
                        Console.WriteLine("O nome do medicamento deve conter apenas letras e números.");
                    }
                } while (!validadeNome);
                try
                {
                    Exceptions.GeneralException.VerificarTamanhoMaiorString(nome, 40, "O nome do medicamento deve ter no máximo 100 caracteres.");
                    Exceptions.GeneralException.VerificarTamanhoMenorString(nome, 3, "O nome do medicamento deve ter no mínimo 3 caracteres.");
                    Nome = nome;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (Nome == "");

            do
            {
                Console.WriteLine("Digite a categoria do medicamento (A , B, I, V):");
                validadeCategoria = char.TryParse(Console.ReadLine().ToUpper(), out categoria);

                if (validadeCategoria == false)
                {
                    Console.WriteLine("O medicamento deve estar em uma das 4 categorias");
                }
            } while (categoria != 'A' || categoria != 'B' || categoria != 'I' || categoria != 'V');

            decimal valorVenda = 0;
            do
            {
                valorVenda = InputHelper.RetornarNumeroDecimal("Digite o valor de Venda:", "O valor deve ser maior que R$0,00 e menor que R$10000,00.");
            } while (valorVenda <= 0 || valorVenda >= 10000);

            Console.WriteLine("Digite a data da ultima venda (DD/MM/AAAA): ");
            DateOnly ultimaVenda = DateOnly.Parse(Console.ReadLine());

            Console.WriteLine("Digite a data do cadastro do medicamento (DD/MM/AAAA): ");
            DateOnly dataCadastro = DateOnly.Parse(Console.ReadLine());

            do
            {
                Console.WriteLine("Digite a situação do medicamento (A, I): ");
                validadeSituacao = char.TryParse(Console.ReadLine().ToUpper(), out situacao);
                if (validadeSituacao == false)
                {
                    Console.WriteLine("");
                }
            } while (situacao != 'A' || situacao != 'I');

            Medicine medicamento = new Medicine(cdb, nome, categoria, valorVenda, ultimaVenda, dataCadastro, situacao);

            this.Medicamentos.Add(medicamento);

        }

        public Medicine LocalizarMedicamento(string id)
        {
            Console.WriteLine("Digite o código de barras do medicamento que deseja procurar: ");
            string localizar = Console.ReadLine();
            return Medicamentos.Find(m => m.CDB == localizar);
        }

        public Medicine AlterarMedicamento()
        {
            Console.WriteLine("Digite o código de barras do medicamento que deseja alterar:");
            string alterar = Console.ReadLine() ?? "";


            Medicine medicine = LocalizarMedicamento(alterar);

            Console.WriteLine("Deseja alterar o nome do medicamento? (S , N):");
            char escolha = char.Parse(Console.ReadLine().ToUpper());

            if (escolha == 'S')
            {
                if (medicine is not null)
                {
                    Console.WriteLine("Digite o nome que deseja atribuir ao remédio: ");
                    medicine.setNome(Console.ReadLine());

                    Console.WriteLine("Nome atualizado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Não existe esse medicamento.");
                }

                return medicine;
            }
            Console.WriteLine("Deseja alterar a situação do medicamento? (S , N):");
            escolha = char.Parse(Console.ReadLine().ToUpper());
            if(escolha == 'S')
            {
                if(medicine is not null)
                {
                    if (medicine.Situacao == 'A')                 
                        medicine.setSituacao('I');
                    else
                        medicine.setSituacao('A');

                }
                return medicine;
            }
            else
            {
                return medicine;
            }
        }

        public void ListarMedicamentos()
        {
            Console.WriteLine("===Lista de medicamentos ===");
            foreach(var medicine in Medicamentos)
            {
                Console.WriteLine(medicine);
            }
        }

        #endregion

        #region Compra CRUD

        public void CriarPurchase()
        {
            string cnpj;
            do
            {
                cnpj = InputHelper.RetornarString("Digite o CNPJ do fornecedor:", "O CNPJ é inválido.");
                var fornecedor = this.Fonecedores.Find(f => f.Cnpj == cnpj);
                if(fornecedor != null)
                {
                    
                    Purchases compra = new Purchases(fornecedor.Cnpj);
                }
            }
            while (cnpj.Length != 14);
            decimal valorTotal = 0;
            do
            {
                valorTotal = InputHelper.RetornarNumeroDecimal("Digite o valor de Total da Compra:", "O valor deve ser maior que R$0,00 e menor que R$10000,00.");
            } while (valorTotal <= 0 || valorTotal >= 10000);

        }

        #endregion

        #region Item da Compra CRUD

        public void CriarItemCompra()
        {
            do
            {
                Console.WriteLine("Digite o ID do princípio ativo que deseja comprar: ");
                string ingrediente = Console.ReadLine();


                decimal quantidade = 0;
                do
                {
                    quantidade = InputHelper.RetornarNumeroDecimal("Digite a quantidade de itens em gramas (máx: 999,99): ", "O valor deve ser maior que 0 e menor que 10000");
                } while (quantidade <= 0 || quantidade >= 10000);

                decimal valorUnitario = 0;
                do
                {
                    valorUnitario = InputHelper.RetornarNumeroDecimal("Digite o valor unitário por grama (máx: 999,99):", "O valor digitado deve ser maior que 0 e menor que 1000");
                } while (valorUnitario <= 0 || valorUnitario >= 1000);

                decimal totalItem = quantidade * valorUnitario;
            }while(opcao )


        }

        #endregion
    }
}
