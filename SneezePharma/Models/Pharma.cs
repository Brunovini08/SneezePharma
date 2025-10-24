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
        public List<Supplier> Fornecedores { get; private set; }
        public List<Medicine> Medicamentos { get; private set; }
        public List<ProduceItem> ItemProducao { get; private set; }
        public List<RestrictedCustomer> ClientesRestritos { get; private set; }
        public List<SalesModel> Venda { get; private set; }
        public List<SalesItemModel> ItensDeVenda { get; private set; }

        public Pharma()
        {
            // Realização da leitura de Itens de Venda
            SalesItemManipulate.CriarArquivo();
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


        //Fornecedor CRUD
        public void AdicionarFornecedor()
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
                do
                {
                    dataAbertura = InputHelper.RetornarData("Digite a data de abertura (no modelo: DDMMAAAA): ", "Data de abertura inválida");
                } while (dataAbertura == null || dataAbertura.ToString() == string.Empty);

                do
                {
                    ultimoFornecimento = InputHelper.RetornarData("Digite a data do ultimo fornecimento (no modelo: DDMMAAAA):", "Data do ultimo fornecimento inválida");
                } while (ultimoFornecimento == null || ultimoFornecimento.ToString() == string.Empty);


                do
                {
                    dataCadastro = InputHelper.RetornarData("Digite a data do cadastro (no modelo: DDMMAAAA):", "Data do ultimo cadastro inválida");
                } while (dataCadastro == null || dataCadastro.ToString() == string.Empty);



                this.Fornecedores.Add(new Supplier(cnpj, razaoSocial,
                pais, dataAbertura,
                ultimoFornecimento, dataCadastro));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Supplier LocalizarFornecedor(string cnpj)
        {
            return Fornecedores.Find(c => c.Cnpj == cnpj);
        }

        public void AtualizarRazaoSocial()
        {
            Console.WriteLine("Digite o Cnpj que deseja alterar a razão social:");
            string cnpj = Console.ReadLine();
            var fornecedor = this.LocalizarFornecedor(cnpj);

            if (fornecedor is null)
            {
                Console.WriteLine("Fornecedor não encontrado");
            }
            else
            {
                Console.WriteLine("Digite a nova razão social: ");
                var novaRazaoSocial = Console.ReadLine();
                fornecedor.setRazaoSocial(novaRazaoSocial);
                Console.WriteLine(novaRazaoSocial);
            }
        }
        public void AlterarSituacaoFornecedor()
        {

            Console.WriteLine("Digite o Cnpj que deseja alterar a Situacao: ");
            var Cnpj = Console.ReadLine();
            LocalizarFornecedor(Cnpj);

            if (Cnpj != null)
            {
                string situacao;
                do
                {
                    situacao = InputHelper.RetornarString("Informe a situação que deseja: ", "Situação inválida, digite novamente: ");

                } while (situacao != "A" && situacao != "I");

            }
        }

        public void AlterarDataUltimoFornecimento()
        {
            Console.WriteLine("Digite o Cnpj que deseja alterar a data do ultimo fornecimento: ");
            var Cnpj = Console.ReadLine();
            LocalizarFornecedor(Cnpj);

            if (Cnpj != null)
            {
                DateOnly ultimoFornecimento;
                do
                {
                    ultimoFornecimento = InputHelper.RetornarData("Digite a nova data de fornecimento no formato DDMMAAAA: ", "Data do ultimo fornecimento inválida");
                } while (ultimoFornecimento == null || ultimoFornecimento.ToString() == string.Empty);
            }
        }

        public void ListarFornecedores()
        {
            foreach (var fornecedor in this.Fornecedores.ToList())
            {
                Console.WriteLine(fornecedor);
            }
        }

    }
}
