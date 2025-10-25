using SneezePharma.Exceptions;
using SneezePharma.Models.Sales;
using SneezePharma.Models.SalesItem;
using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public List<SalesModel> Venda { get; private set; }
        public List<SalesItemModel> ItensDeVenda { get; private set; }

        public Pharma()
        {
            // Realização da leitura de Itens de Venda
            SalesItemManipulate.CriarArquivo(SalesItemManipulate.DirectoryPath, SalesItemManipulate.FilePath);
            this.ItensDeVenda = SalesItemManipulate.LerItensDeVenda();
        }

        

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

        #region "Operações de CRUD da classe Sales"
        
        public void RegistrarVenda()
        {
            Console.WriteLine("Digite o ");
        }
        
        #endregion

        #region "Operações de CRUD do SalesItem"

        public void CadastrarItemDeVenda(int id)
        {

            int ConfirmacaoQuantidade()
            {
                var quantidade = 0;
                var repetir = true;

                do
                {
                    try
                    {
                        quantidade = InputHelper.RetornarNumeroInteiro("Digite a quantidade de medicamentos:");
                        GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade inválida! A quantidade deve estar entre 0 e 999");

                        repetir = false;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (repetir);

                return quantidade;
            }

            try
            {
                int idVenda = InputHelper.RetornarNumeroInteiro("Digite o número do Id da Venda:");

                string cbcMedicamento = InputHelper.RetornarSomenteNumeros("Digite o CBC do Medicamento:");
                GeneralException.VerificarTamanhoDiferente(cbcMedicamento, 13, "O CBC precisa possuir 13 digitos");

                var quantidade = ConfirmacaoQuantidade();

                //TODO: Puxar o valor unitário direto do Medicamento (Medicamento)
                decimal valorUnitario = InputHelper.RetornarNumeroDecimal("Digite o valor unitário do medicamento");

                decimal totalItem = quantidade * valorUnitario;
                GeneralException.VerificarQuantidadeMaximaDecimal(totalItem, 99999.99m, "Valor total ultrapassou limite máximo (R$ 99999,99)");

                this.ItensDeVenda.Add(new SalesItemModel(
                    ++id,
                    idVenda,
                    cbcMedicamento,
                    quantidade,
                    valorUnitario,
                    totalItem
                 ));
                Console.WriteLine("Cadastro de item de venda realizado com sucesso!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ListarItensDeVenda()
        {
            if (this.ItensDeVenda.Count > 0)
            {
                Console.WriteLine("===== Itens de Vendas =====");
                foreach (var iv in this.ItensDeVenda)
                {
                    Console.WriteLine(iv);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Não há itens de venda cadastrados!");
            }

            InputHelper.PressioneEnterParaContinuar();
        }

        public void AtualizarItemDaVenda()
        {
            bool ExisteItensDeVenda()
            {
                if (this.ItensDeVenda.Count > 0)
                    return true;

                return false;
            }

            SalesItemModel ProcurarItemDeVendaPorId(int id)
            {
                return this.ItensDeVenda.FirstOrDefault(sl => sl.Id == id);
            }

            void AlterarQuantidadeItensVenda(SalesItemModel itemDeVenda)
            {
                var quantidade = 0;
                try
                {
                    quantidade = InputHelper.RetornarNumeroInteiro("Digite a quantidade de medicamentos:");
                    GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade inválida! A quantidade deve estar entre 0 e 999");

                    var totalItem = itemDeVenda.ValorUnitario * quantidade;

                    GeneralException.VerificarQuantidadeMaximaDecimal(totalItem, 99999.99m, "Valor total ultrapassou limite máximo (R$ 99999,99)");

                    itemDeVenda.SetQuantidade(quantidade);
                    itemDeVenda.SetTotalItem(totalItem);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            void AlterarPropriedadesDoItemDeVendas(SalesItemModel itemDeVenda)
            {
                var repetir = true;
                do
                {
                    Console.Clear();
                    Console.WriteLine(itemDeVenda);
                    Console.WriteLine();

                    Console.WriteLine("1 - Alterar Id da venda");
                    Console.WriteLine("2 - Alterar CBC de Medicamento");
                    Console.WriteLine("3 - Quantidade de itens");
                    Console.WriteLine("0 - Sair e salvar");
                    var opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "3":
                            AlterarQuantidadeItensVenda(itemDeVenda);
                            InputHelper.PressioneEnterParaContinuar();
                            break;
                        case "0":
                            Console.WriteLine("Alterações realizadas com sucesso!");
                            repetir = false;
                            break;
                        default:
                            Console.WriteLine("Opção inválida! Tente novamente!");
                            break;
                    }
                }
                while (repetir);
            }

            if (ExisteItensDeVenda())
            {
                ListarItensDeVenda();
                var id = InputHelper.RetornarNumeroInteiro("Digite o Id do item de venda que deseja atualizar:");

                var itensDeVenda = ProcurarItemDeVendaPorId(id);

                if (itensDeVenda != null)
                {
                    AlterarPropriedadesDoItemDeVendas(itensDeVenda);
                }
                else
                {
                    Console.WriteLine("Item de venda não foi localizado!");
                    InputHelper.PressioneEnterParaContinuar();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Não há itens de venda cadastrados!");
            }
            InputHelper.PressioneEnterParaContinuar();
        }


        #endregion

    }
}
