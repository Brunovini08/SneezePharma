using SneezePharma.Enums;
using SneezePharma.Exceptions;
using SneezePharma.Helpers;
using SneezePharma.Models.Produce;
using SneezePharma.Models.Produce.Produce;
using SneezePharma.Models.Sales;
using SneezePharma.Models.SalesItem;
using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SneezePharma.Models
{
    public class Pharma
    {
        public List<Customer> Clientes { get; private set; }
        public List<Supplier> Fornecedores { get; private set; }
        public List<Medicine> Medicamentos { get; private set; }
        //public List<Produce> Producao { get; set; }
        public List<ProduceItem> ItemProducao { get; private set; }
        public List<RestrictedCustomer> ClientesRestritos { get; private set; }
        public List<RestrictedSupplier> FornecedoresRestritos { get; private set; }
        public List<SalesModel> Venda { get; private set; }
        public List<SalesItemModel> ItensDeVenda { get; private set; }

        private SalesManipulate SalesManipulate = new SalesManipulate();
        private SalesItemManipulate SalesItemManipulate = new SalesItemManipulate();

        public Pharma()
        {
            this.Clientes = new List<Customer>();
            this.ClientesRestritos = new List<RestrictedCustomer>();
            this.Fornecedores = new List<Supplier>();
            this.FornecedoresRestritos = new List<RestrictedSupplier>();
            this.Medicamentos = new List<Medicine>();

            this.Venda = SalesManipulate.Ler();
            this.ItensDeVenda = SalesItemManipulate.Ler();
        }

        public void Cadastrar()
        {
            int opcao;
            bool validar;
            do
            {
                Menu.MenuCadastros();
                validar = int.TryParse(Console.ReadLine(), out opcao);
                switch (opcao)
                {
                    case 1:
                        RegistrarCliente();
                        break;
                }
            } while (validar == false);
        }

        public void VendasMedicamento()
        {
            var repetir = true;
            do
            {
                Console.Clear();

                Menu.MenuVendasMedicamentos();
                var opcao = Console.ReadLine() ?? "-1";

                switch (opcao)
                {
                    case "1":
                        RegistrarVenda();
                        break;
                    case "2":
                        AlterarItensDaVenda();
                        break;
                    case "3":
                        BuscarVendaPorId();
                        break;
                    case "4":
                        ListarVendas();
                        break;
                    case "0":
                        repetir = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Escolha uma das opções do menu!"); 
                        break;
                }
            }
            while (repetir);
        }

        public void ComprarPrincipioAtivo()
        {

        }

        public void ManipularMedicamentos()
        {

        }

        public void ManipularClientes()
        {
            int opcao;
            bool validar;
            do
            {
                Menu.MenuManipulacaoCliente();
                validar = int.TryParse(Console.ReadLine(), out opcao);
                switch (opcao)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        AdicionarClienteRestrito();
                        break;
                    case 4:
                        RemoverClienteRestrito();
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 0:
                        break;

                }
            } while (validar == false);
        }

        public void ManipularFornecedores()
        {
            int opcao;
            bool validar;
            do
            {
                Menu.MenuManipulacaoFornecedor();
                validar = int.TryParse(Console.ReadLine(), out opcao);
                switch (opcao)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        AdicionarFornecedorRestrito();
                        break;
                    case 4:
                        RemoverFornecedorRestrito();
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 0:
                        break;

                }
            } while (validar == false);
        }

        public void ManipularPrincipioAtivo()
        {

        }

        #region Operações de CRUD da classe Customer
        public void RegistrarCliente()
        {
            try
            {
                Console.WriteLine("CADASTRANDO CLIENTE: ");
                Console.WriteLine();
                string cpf;
                bool validar = false;
                do
                {
                    try
                    {
                        Console.Clear();
                        cpf = InputHelper.RetornarString("Digite o seu CPF (Sem pontuções): ", "Por favor, digite o CPF!");
                        if (cpf.Length != 11)
                        {
                            InputHelper.ExibirErro("CPF deve conter 11 caracteres!");
                            InputHelper.PressioneEnterParaContinuar();
                            Console.Clear();
                        }
                        else if (cpf.Length == 11)
                        {
                            validar = InputHelper.ValidarCpf(cpf);
                            if (!validar)
                            {
                                InputHelper.ExibirErro("CPF inválido!");
                                InputHelper.PressioneEnterParaContinuar();
                                Console.Clear();
                            }
                            else
                            {
                                Customer cliente = this.Clientes.Find(c => c.CPF == cpf);
                                if (cliente != null)
                                {
                                    InputHelper.ExibirErro("CPF já cadastrado, tente com outro CPF!");
                                    InputHelper.PressioneEnterParaContinuar();
                                    Console.Clear();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                } while (cpf.Length != 11 || validar != true);
                Console.Clear();
                string nome;
                string padrao = @"^-?[0-9]+(?:\.[0-9]+)?$";
                Regex regex = new Regex(padrao);
                do
                {
                    try
                    {
                        nome = InputHelper.RetornarString("Digite seu nome: ", "Por favor, digite o nome!");
                        if (regex.IsMatch(nome))
                        {
                            InputHelper.ExibirErro("Nome não pode conter número!");
                            InputHelper.PressioneEnterParaContinuar();
                            Console.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                } while (nome.Length > 50 || nome.Length <= 0 || regex.IsMatch(nome) == true);

                Console.Clear();

                string dataNascimento;

                do
                {
                    try
                    {
                        dataNascimento = InputHelper.RetornarData("Digite sua Data de Nascimento: ", "Por favor, digite a data de nascimento!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                } while (dataNascimento is null || dataNascimento == String.Empty);

                Console.Clear();
                string telefone;
                do
                {
                    telefone = InputHelper.RetornarString("Digite seu telefone com DDD: ", "Por favor, digite o número de telefone com DDD!");
                } while (telefone.Length != 11);
                Console.Clear();

                Customer customer = new Customer(cpf, nome, DateOnly.Parse(dataNascimento), telefone);
                this.Clientes.Add(customer);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Cliente cadastrado com sucesso");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
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
        #endregion

        #region "Operações de CRUD da classe Sales"
        private string EntradaCdbECpf(string msgEntrada, string msgException, int tamanhoDaString)
        {
            const int TAMANHOCPF = 11;
            var saida = "";
            var repetir = true;
            do
            {
                try
                {
                    saida = InputHelper.RetornarSomenteNumeros(msgEntrada);
                    GeneralException.VerificarTamanhoDiferente(saida, tamanhoDaString, msgException);
                    repetir = false;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    InputHelper.PressioneEnterParaContinuar();
                }
            }
            while (repetir);

            return saida;
        }

        private bool ClienteRestrito(string cpf)
        {
            return BuscarClienteRestrito(cpf) is not null;
        }

        private bool RealizarPerguntaDeConfirmacao(string msgEntrada)
        {
            var opcao = "";
            var repetir = true;
            do
            {
                Console.WriteLine(msgEntrada);
                opcao = Console.ReadLine();

                repetir = opcao != "1" && opcao != "0";

                if (repetir)
                    Console.WriteLine("Entrada inválida! Escolha uma opção válida!");
            }
            while (repetir);

            return opcao == "1";
        }

        public void RegistrarVenda()
        {
            var id = this.Venda.LastOrDefault()?.Id ?? 0;

            var cpf = EntradaCdbECpf(
                "Digite o CPF do Cliente:",
                "Total de dígitos do CPF não bate! (CPF possui 11 dígitos)",
                11
            );

            var cliente = BuscarPorId(cpf);

            if (cliente is null)
            {
                Console.WriteLine("Este cliente não está cadastrado no sistema");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }
            else if (cliente.Situacao == SituationCustomer.I)
            {
                Console.WriteLine("Este cliente está inativo no sistema");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }
            else if (ClienteRestrito(cpf))
            {
                Console.WriteLine("Este cliente está inadimplente!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }

            var venda = new SalesModel(++id, cpf);

            var listaDeCdbs = new List<string>(3);
            var valorTotal = 0.0m;
            var repetir = false;
            var medicamentosAtivos = this.Medicamentos.FindAll(m => m.Situacao == 'A');
            do
            {
                Console.Clear();

                Console.WriteLine("======= MEDICAMENTOS DISPONÍVEIS =======");
                foreach (var m in medicamentosAtivos)
                {
                    Console.WriteLine(m);
                    Console.WriteLine();
                }

                Console.WriteLine($"SOMENTE 3 MEDICAMENTOS POR VENDA! VOCÊ ADICIONOU {listaDeCdbs.Count} A VENDA");
                var cdbDoMedicamento = EntradaCdbECpf(
                    "Digite o CDB do medicamento:",
                    "Total de dígitos do CDB não bate! (CDB possui 13 dígitos)",
                    13
                );

                var medicamento = medicamentosAtivos.Find(m => m.CDB == cdbDoMedicamento);

                if (medicamento is null)
                {
                    Console.WriteLine("Medicamento não está disponível!");
                }
                else
                {
                    var itemDeVenda = CadastrarItemDeVenda(venda, medicamento);
                    if (itemDeVenda is not null)
                    {
                        listaDeCdbs.Add(itemDeVenda.Medicamento);

                        medicamento.setUltimaVenda();

                        valorTotal += itemDeVenda.TotalItem;

                        medicamentosAtivos.Remove(medicamento);

                        this.ItensDeVenda.Add(itemDeVenda);
                    }
                }

                if (listaDeCdbs.Count == 3)
                    continue;

                repetir = RealizarPerguntaDeConfirmacao("Deseja continuar vendendo? (1 - SIM|0 - NÃO)");
            }
            while (repetir && listaDeCdbs.Count < 3);

            if (listaDeCdbs.Count == 0)
                Console.WriteLine("Venda cancelada!!");
            else
            {
                Console.WriteLine("Venda realizada com sucesso!");
                venda.setValorTotal(valorTotal);
                this.Venda.Add(venda);
                this.SalesManipulate.Gravar(this.Venda);
                this.SalesItemManipulate.Gravar(this.ItensDeVenda);
            }
            InputHelper.PressioneEnterParaContinuar();
        }

        private void AlterarItensDaVenda()
        {
            if (this.Venda.Count == 0)
            {
                Console.WriteLine("Não há vendas realizadas até o momento!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }

            var cpf = EntradaCdbECpf(
                "Digite o CPF do Cliente:",
                "Total de dígitos do CPF não bate! (CPF possui 11 dígitos)",
                11
            );

            var cliente = BuscarPorId(cpf);

            if (cliente is null)
            {
                Console.WriteLine("Este cliente não está cadastrado no sistema");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }
            else if (cliente.Situacao == SituationCustomer.I)
            {
                Console.WriteLine("Este cliente está inativo no sistema");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }
            else if (ClienteRestrito(cpf))
            {
                Console.WriteLine("Este cliente está inadimplente!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }

            var vendas = BuscarVendasPorCpfDoCliente(cpf);

            if (vendas.Count == 0)
            {
                Console.WriteLine("O ciente ainda não realizou nenhuma compra!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }

            Console.Clear();
            Console.WriteLine("====== VENDAS DO CLIENTE ======");
            foreach (var v in vendas)
            {
                Console.WriteLine(v);
                Console.WriteLine();
            }

            var idVenda = InputHelper.RetornarNumeroInteiro("Digite o Id da venda:");

            var itensDeVenda = BuscarItensDeVenda(idVenda);

            if (itensDeVenda.Count == 0)
            {
                Console.WriteLine("Não itens de venda do Id digitado!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }

            var venda = BuscarVendaPorId(idVenda);

            var repetir = true;
            do
            {
                Console.Clear();

                if (itensDeVenda.Count == 0)
                    return;

                Console.WriteLine("====== ITENS DA VENDA ======");
                foreach (var iv in itensDeVenda)
                {
                    Console.WriteLine(iv);
                    Console.WriteLine();
                }

                Console.WriteLine("1 - Excluir item");
                Console.WriteLine("2 - Alterar quantidade do item");
                Console.WriteLine("0 - Voltar ");
                var opcao = Console.ReadLine() ?? "-1";

                switch (opcao)
                {
                    case "1":
                        ExcluirItemDaVenda(itensDeVenda, venda);
                        break;
                    case "2":
                        AlterarItemDaVenda(itensDeVenda);
                        break;
                    case "0":
                        repetir = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Digite uma das opções do menu!");
                        break;
                }
                venda.setValorTotal(itensDeVenda.Sum(iv => iv.TotalItem));
            }
            while (repetir);
            SalesManipulate.Gravar(this.Venda);
            SalesItemManipulate.Gravar(this.ItensDeVenda);
        }

        private void ExcluirItemDaVenda(List<SalesItemModel> itensDaVenda, SalesModel venda)
        {
            var id = InputHelper.RetornarNumeroInteiro("Digite o Id do item que deseja excluir:");

            var itemDaVenda = BuscarItemDaVenda(id, itensDaVenda);

            if (itemDaVenda is null)
            {
                Console.WriteLine("Esse item não está nessa venda!");
            }
            else
            {
                var confirmarExclusaoDoItem = RealizarPerguntaDeConfirmacao("Deseja realmente excluir o item? (1 - Sim|0 - Não):");

                if (confirmarExclusaoDoItem && itensDaVenda.Count > 1)
                {
                    Console.WriteLine("Item da venda excluído com sucesso!");
                    itensDaVenda.Remove(itemDaVenda);
                    this.ItensDeVenda.Remove(itemDaVenda);
                }
                else if (confirmarExclusaoDoItem && itensDaVenda.Count == 1)
                {
                    var confirmarExclusaoDaVenda = RealizarPerguntaDeConfirmacao("Ao excluir esse item estará cancelando a venda! Deseja continuar (1- Sim|0 - Não)");
                    if (confirmarExclusaoDaVenda)
                    {
                        itensDaVenda.Remove(itemDaVenda);
                        this.ItensDeVenda.Remove(itemDaVenda);
                        this.Venda.Remove(venda);
                    }
                }
                else
                {
                    Console.WriteLine("A operação de exclusão foi cancelada!");
                }
            }
            InputHelper.PressioneEnterParaContinuar();
        }


        private void AlterarItemDaVenda(List<SalesItemModel> itensDaVenda)
        {
            var id = InputHelper.RetornarNumeroInteiro("Digite o Id do item que deseja alterar a quantidade:");

            var itemDaVenda = BuscarItemDaVenda(id, itensDaVenda);

            if (itemDaVenda is null)
            {
                Console.WriteLine("Esse item não está nessa venda!");
            }
            else
            {
                var novaQuantidade = ConfirmacaoQuantidade();

                try
                {
                    decimal novoTotalItem = novaQuantidade * itemDaVenda.ValorUnitario;
                    GeneralException.VerificarQuantidadeMaximaDecimal(novoTotalItem, 99999999.99m, "Valor total ultrapassou limite máximo (R$ 99999999,99)");

                    itemDaVenda.SetQuantidade(novaQuantidade);
                    itemDaVenda.SetTotalItem(novoTotalItem);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    InputHelper.PressioneEnterParaContinuar();
                }
            }
        }

        private SalesItemModel? BuscarItemDaVenda(int id, List<SalesItemModel> itensDaVenda)
        {
            return itensDaVenda.Find(iv => iv.Id == id);
        }

        private SalesModel? BuscarVendaPorId(int id)
        {
            return this.Venda.Find(v => v.Id == id);
        }

        private List<SalesModel> BuscarVendasPorCpfDoCliente(string cpf)
        {
            return this.Venda.FindAll(v => v.Cliente == cpf);
        }

        private List<SalesItemModel> BuscarItensDeVenda(int idVenda)
        {
            return this.ItensDeVenda.FindAll(iv => iv.IdVenda == idVenda);
        }

        public void BuscarVendaPorId()
        {
            if (this.Venda.Count == 0)
            {
                Console.WriteLine("Nenhuma venda foi realizada ainda!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }

            var id = InputHelper.RetornarNumeroInteiro("Digite o Id da Venda que deseja consultar: ");

            var vendaLocalizada = BuscarVendaPorId(id);

            if (vendaLocalizada is not null)
            {
                var itensDeVenda = BuscarItensDeVenda(vendaLocalizada.Id);

                Console.WriteLine("======= VENDA LOCALIZADA =======\n");
                Console.WriteLine(vendaLocalizada);
                Console.WriteLine("\n======= Itens de Venda =======");
                foreach (var iv in itensDeVenda)
                {
                    Console.WriteLine(iv);
                    Console.WriteLine("==============================");
                }
            }
            else
            {
                Console.WriteLine("Não foi possível localizar essa venda!");
            }
            InputHelper.PressioneEnterParaContinuar();
        }

        public void ListarVendas()
        {
            if (this.Venda.Count > 0)
            {
                Console.WriteLine("======= VENDAS =======");
                foreach (var v in this.Venda)
                {
                    Console.WriteLine(v);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Nenhuma venda foi realizada ainda!");
            }

            InputHelper.PressioneEnterParaContinuar();
        }

        #endregion

        #region "Operações de CRUD do SalesItem"

        private int ConfirmacaoQuantidade()
        {
            var quantidade = 0;
            var repetir = true;

            do
            {
                try
                {
                    quantidade = InputHelper.RetornarNumeroInteiro("Digite a quantidade de medicamentos:");
                    GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade inválida! A quantidade deve ser 1 a 999");

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

        private SalesItemModel CadastrarItemDeVenda(SalesModel venda, Medicine medicamento)
        {
            var id = this.ItensDeVenda.LastOrDefault()?.Id ?? 0;

            try
            {
                var idVenda = venda.Id;
                var cdbMedicamento = medicamento.CDB;
                var quantidade = ConfirmacaoQuantidade();

                decimal valorUnitario = medicamento.ValorVenda;

                decimal totalItem = quantidade * valorUnitario;
                GeneralException.VerificarQuantidadeMaximaDecimal(totalItem, 99999999.99m, "Valor total ultrapassou limite máximo (R$ 99999999,99)");

                Console.WriteLine("Item de venda realizado com sucesso!");
                return (new SalesItemModel(
                     ++id,
                     idVenda,
                     cdbMedicamento,
                     quantidade,
                     valorUnitario,
                     totalItem
                  ));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
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

        #endregion


        #region Operações de CRUD da classe Supplier
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
                    dataAbertura = DateOnly.Parse(InputHelper.RetornarData("Digite a data de abertura (no modelo: DDMMAAAA): ", "Data de abertura inválida"));
                } while (dataAbertura == null || dataAbertura.ToString() == string.Empty);

                do
                {
                    ultimoFornecimento = DateOnly.Parse(InputHelper.RetornarData("Digite a data do ultimo fornecimento (no modelo: DDMMAAAA):", "Data do ultimo fornecimento inválida"));
                } while (ultimoFornecimento == null || ultimoFornecimento.ToString() == string.Empty);


                do
                {
                    dataCadastro = DateOnly.Parse(InputHelper.RetornarData("Digite a data do cadastro (no modelo: DDMMAAAA):", "Data do ultimo cadastro inválida"));
                } while (dataCadastro == null || dataCadastro.ToString() == string.Empty);



                this.Fornecedores.Add(new Supplier(cnpj, razaoSocial,
                pais, dataAbertura));

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
                    ultimoFornecimento = DateOnly.Parse(InputHelper.RetornarData("Digite a nova data de fornecimento no formato DDMMAAAA: ", "Data do ultimo fornecimento inválida"));
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
        #endregion

        #region Operação de CD da classe RestrictedSupplier

        public void AdicionarFornecedorRestrito()
        {
            try
            {
                RestrictedSupplier fornecedorBloquear;
                Supplier fornecedor;
                do
                {
                    string cnpj = InputHelper.RetornarString("Digite o CNPJ do Fornecedor que deseja retirar da lista de bloqueados: ", "Por favor, digite o CNPJ do Fornecedor");
                    fornecedor = this.Fornecedores.Find(c => c.Cnpj == cnpj);
                    fornecedorBloquear = this.FornecedoresRestritos.Find(f => f.Cnpj == cnpj);
                    if (fornecedor != null)
                    {
                        if (fornecedorBloquear is not null)
                        {
                            this.FornecedoresRestritos.Remove(fornecedorBloquear);
                            Console.WriteLine($"Fornecedor retirado da lista de restritos: {fornecedor.RazaoSocial}");
                        }
                        else
                        {
                            Console.WriteLine("Fornecedor não está na lista de bloqueado!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Fornecedor não encontrado");
                    }
                } while (fornecedor == null || fornecedorBloquear is null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void RemoverFornecedorRestrito()
        {
            try
            {
                RestrictedSupplier fornecedorLiberar;
                Supplier fornecedor;
                do
                {
                    string cnpj = InputHelper.RetornarString("Digite o CNPJ do Fornecedor que deseja adicionar na lista de bloqueados: ", "Por favor, digite o CNPJ do Fornecedor");
                    fornecedor = this.Fornecedores.Find(c => c.Cnpj == cnpj);
                    fornecedorLiberar = this.FornecedoresRestritos.Find(f => f.Cnpj == cnpj);
                    if (fornecedor != null)
                    {
                        if (fornecedorLiberar is null)
                        {
                            this.FornecedoresRestritos.Add(fornecedorLiberar);
                            Console.WriteLine($"Fornecedor adicionado da lista de restritos: {fornecedor.RazaoSocial}");
                            InputHelper.PressioneEnterParaContinuar();
                        }
                        else
                        {
                            Console.WriteLine("Fornecedor está na lista de bloqueado!");
                            InputHelper.PressioneEnterParaContinuar();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Fornecedor não encontrado");
                    }
                } while (fornecedor is null || fornecedorLiberar is null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public RestrictedSupplier BuscarFornecedorRestrito(string cnpj)
        {
            return this.FornecedoresRestritos.Find(f => f.Cnpj == cnpj);
        }
        public void ListarFornecedoresRestritos()
        {
            Console.WriteLine("LISTA DE FORNECEDORES BLOQUEADOS COM BASE NO CNPJ");
            foreach (var fornecedor in this.FornecedoresRestritos)
            {
                Console.WriteLine(fornecedor.Cnpj);
            }
        }
        #endregion

        #region Operação de CD da classe RestrictedCustomer
        public void AdicionarClienteRestrito()
        {
            try
            {
                RestrictedCustomer clienteBloquear;
                Customer cliente;
                do
                {

                    string cpf = InputHelper.RetornarString("Digite o CPF do cliente que deseja adicionar na lista de bloqueados: ", "Por favor, digite o CPF do cliente");
                    cliente = this.Clientes.Find(c => c.CPF == cpf);
                    clienteBloquear = this.ClientesRestritos.Find(c => c.CPF == cpf);
                    if (cliente != null)
                    {
                        if (clienteBloquear is null)
                        {
                            this.ClientesRestritos.Add(clienteBloquear);
                            Console.WriteLine($"Cliente adicionado da lista de restritos: {cliente.Nome}");
                            InputHelper.PressioneEnterParaContinuar();
                        }
                        else
                        {
                            Console.WriteLine("Cliente está na lista de bloqueado!");
                            InputHelper.PressioneEnterParaContinuar();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cliente não encontrado");
                        InputHelper.PressioneEnterParaContinuar();
                    }
                } while (cliente == null || clienteBloquear is null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void RemoverClienteRestrito()
        {
            try
            {
                RestrictedCustomer clienteLiberar;
                Customer cliente;
                do
                {
                    string cpf = InputHelper.RetornarString("Digite o CPF do cliente que deseja retirar da lista de bloqueados: ", "Por favor, digite o CNPJ do Fornecedor");
                    cliente = this.Clientes.Find(c => c.CPF == cpf);
                    clienteLiberar = this.ClientesRestritos.Find(c => c.CPF == cpf);
                    if (cliente != null)
                    {
                        if (clienteLiberar is not null)
                        {
                            this.ClientesRestritos.Remove(clienteLiberar);
                            Console.WriteLine($"Cliente retirado da lista de restritos: {cliente.Nome}");
                            InputHelper.PressioneEnterParaContinuar();
                        }
                        else
                        {
                            Console.WriteLine("Cliente não está na lista de bloqueado!");
                            InputHelper.PressioneEnterParaContinuar();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cliente não encontrado");
                        InputHelper.PressioneEnterParaContinuar();
                    }
                } while (cliente == null || clienteLiberar is null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public RestrictedCustomer BuscarClienteRestrito(string cpf)
        {
            return this.ClientesRestritos.Find(c => c.CPF == cpf);
        }
        public void ListarClientesRestritos()
        {
            Console.WriteLine("LISTA DE CLIENTES BLOQUEADOS COM BASE NO CPF");
            foreach (var cliente in this.ClientesRestritos)
            {
                Console.WriteLine($"{cliente.CPF}");
            }
        }
        #endregion

        #region  Operações de CRUD da classe Producer
        //public void MenuProducao()
        //{

        //    var manipulador = new FileProduce();
        //    List<Produce> listaManipulacoes = new List<Produce>();
        //    bool continuar = true;





        //    while (continuar)
        //    {
        //        Console.WriteLine("\n1 - Cadastrar Manipulação");
        //        Console.WriteLine("2 - Listar Manipulações");
        //        Console.WriteLine("3 - Alterar Pedido de Manipulação");
        //        Console.WriteLine("4 - Buscar pelo ID");
        //        Console.WriteLine("0 - Sair");
        //        Console.Write("Escolha uma opção: ");
        //        int opcao = int.Parse(Console.ReadLine());

        //        switch (opcao)
        //        {
        //            case 1:
        //                var manipulacao = new Produce().CadastrarManipulacao();
        //                if (manipulacao is not null)
        //                {
        //                    listaManipulacoes.Add(manipulacao);
        //                    Console.WriteLine("\nManipulação salva com sucesso!");
        //                }
        //                break;

        //            case 2:
        //                //var produtos = manipulador.LerManipulacao();
        //                manipulador.GravarManipulacao(listaManipulacoes);
        //                Produce.MostrarManipulacao(listaManipulacoes);
        //                break;

        //            case 3:

        //                Produce.AlterarManipulacao(listaManipulacoes);
        //                break;
        //            case 4:
        //                Produce.LocalizarManipulacao(listaManipulacoes);
        //                break;
        //            case 0:
        //                manipulador.GravarManipulacao(listaManipulacoes);
        //                continuar = false;
        //                break;

        //            default:
        //                Console.WriteLine("Opção inválida.");
        //                break;
        //        }
        //    }
        //}
        //public bool ValidarDataProducao(DateOnly data)
        //{
        //    DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);


        //    if (data > hoje)
        //    {
        //        Console.WriteLine("Não pode adicionar uma data no futuro.");
        //        return false;
        //    }
        //    if (data < hoje.AddMonths(-1))
        //    {
        //        Console.WriteLine("Data de produção muito antiga.");
        //        return false;
        //    }

        //    DataProducao = data;
        //    Console.WriteLine($"Data da produção registrada: {DataProducao: dd/MM/yyyy}");
        //    return true;
        //}
        //public void ValidarID()
        //{

        //    ID = ultimoID;
        //    Console.WriteLine($"ID: " + ID);
        //    ultimoID++;
        //}
        //public void CadastrarManipulacao()
        //{

        //    try
        //    {

        //        Console.WriteLine("Digite a data de produção (DD/MM/AAAA): ");
        //        string Data = Console.ReadLine();


        //        if (DateOnly.TryParseExact(Data, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out DateOnly data))
        //        {
        //            if (ValidarDataProducao(data))
        //            {


        //            }
        //            else
        //            {
        //                Console.WriteLine("Falha ao validar a data");

        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Formato de data errado. Use o DDMMAAAA");

        //        }



        //        Console.WriteLine("Digite a quantidade que deseja fazer: ");
        //        string Qntd = Console.ReadLine();

        //        if (!int.TryParse(Qntd, out int quantidade))
        //        {
        //            Console.WriteLine("Quantidade invalida. Digite um numero inteiro");

        //        }
        //        GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade de medicamento excedida");

        //        ValidarID();
        //        Console.WriteLine("Manipulação Cadastrada!");
        //        Console.WriteLine($"ID: {ID}, Data: {data:dd/MM/yyyy}, Quantidade: {quantidade}");
        //        Produce produce = new Produce(ID, data, quantidade);
        //        this.ItemProducao.Add(produce);


        //    }
        //    catch (ArgumentException ex)
        //    {

        //        Console.WriteLine(ex.Message);


        //    }

        //}

        //public static void MostrarManipulacao(List<Produce> listaManipulacoes)
        //{
        //    if (listaManipulacoes.Count == 0)
        //    {
        //        Console.WriteLine("Nenhuma manipulação cadastrada.");
        //        return;
        //    }
        //    Console.WriteLine("Manipulações cadastradas: ");
        //    foreach (var item in listaManipulacoes)
        //    {
        //        Console.WriteLine($"ID: {item.ID}, Data: {item.DataProducao: dd/MM/yyyy}, Quantidade: {item.Quantidade}");
        //    }
        //}
        //public Produce LocalizarIDmanipulacao(string id)
        //{

        //    return listaManipulacoes.Find(m => m.ID == id);

        //}

        //public static void AlterarManipulacao(List<Produce> lista)
        //{
        //    Console.WriteLine("Informe o ID que quer alterar: ");
        //    string id = Console.ReadLine() ?? "";
        //    Produce alterar = lista.Find(m => m.ID == id);

        //    if (alterar is not null)
        //    {
        //        Console.WriteLine("Informe a quantidade para alterar: ");
        //        if (int.TryParse(Console.ReadLine(), out int novaQnt))
        //        {
        //            alterar.Quantidade = novaQnt;
        //            Console.WriteLine("Quantidade atualizada");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("ID não encontrado");
        //    }

        //}

        //public static void LocalizarManipulacao(List<Produce> lista)
        //{
        //    Console.WriteLine("Digite o ID que gostaria de localizar: ");
        //    string id = Console.ReadLine() ?? "";

        //    Produce encontrado = lista.Find(m => m.ID == id);
        //    if (encontrado is not null)
        //    {
        //        Console.WriteLine("ID Encontrado!");
        //        Console.WriteLine(encontrado);
        //    }
        //    else
        //    {
        //        Console.WriteLine("ID não encontrado.");
        //    }
        //}
        //public override string ToString()
        //{
        //    return $"ID: {ID}, Data: {DataProducao:dd/MM/yyyy}, Quantidade: {Quantidade}";
        //}
        #endregion

        #region Operações de CRUD da classe ProducerItem
        //public void CadastrarItemProducao()
        //{
        //    try
        //    {
        //        Console.WriteLine("Digite a quantidade de itens produzidos: ");
        //        string Qntd = Console.ReadLine();

        //        if (!int.TryParse(Qntd, out int quantidade))
        //        {
        //            Console.WriteLine("Quantidade invalida. Digite um numero inteiro");
        //            return null;
        //        }
        //        GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade de itens excedida");

        //        ValidarID();
        //        Console.WriteLine("Item Cadastrado!");
        //        Console.WriteLine($"ID: {IDproducao}, Quantidade: {quantidade}");
        //        return new ProduceItem(IDproducao, quantidade);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //}
        //public static void MostrarItensManipulados(List<ProduceItem> listaitens)
        //{
        //    if (listaitens.Count == 0)
        //    {
        //        Console.WriteLine("Nenhuma manipulação cadastrada.");
        //        return;
        //    }
        //    Console.WriteLine("Manipulações cadastradas: ");
        //    foreach (var item in listaitens)
        //    {
        //        Console.WriteLine($"ID: {item.IDproducao},Quantidade: {item.QuantidadeItens}");
        //    }
        //}
        //public ProduceItem LocalizarIDitens(string id)
        //{

        //    return listaitens.Find(i => i.IDproducao == id);
        //}

        //public static void AlterarItemManipulado(List<ProduceItem> listaitem)
        //{
        //    Console.WriteLine("Informe o ID do item que quer alterar: ");
        //    string id = Console.ReadLine() ?? "";
        //    ProduceItem alterar = listaitem.Find(m => m.IDproducao == id);

        //    if (alterar is not null)
        //    {
        //        Console.WriteLine("Informe a quantidade para alterar: ");
        //        if (int.TryParse(Console.ReadLine(), out int novaQnt))
        //        {
        //            alterar.QuantidadeItens = novaQnt;
        //            Console.WriteLine("Quantidade atualizada");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("ID não encontrado");
        //    }

        //}
        #endregion

        #region Operações de CRUD da classe Medicine
        /*Medicamento CRUD*/
        //public void CriarMedicamento()
        //{
        //    bool validadeCDB = false, validadeNome = false, validadeCategoria = false, validadeSituacao = false;
        //    string cdb, nome = "";
        //    char categoria = ' ', situacao = ' ';

        //    do
        //    {
        //        Console.WriteLine("Digite o código de barras do medicamento:");
        //        cdb = Console.ReadLine();
        //    } while (!validadeCDB);
        //    do
        //    {
        //        do
        //        {
        //            Console.WriteLine("Digite o nome do medicamento:");
        //            nome = Console.ReadLine();
        //            validadeNome = nome.All(c => char.IsLetterOrDigit(c));
        //            if (validadeNome == false)
        //            {
        //                Console.WriteLine("O nome do medicamento deve conter apenas letras e números.");
        //            }
        //        } while (!validadeNome);
        //        try
        //        {
        //            Exceptions.GeneralException.VerificarTamanhoMaiorString(nome, 40, "O nome do medicamento deve ter no máximo 100 caracteres.");
        //            Exceptions.GeneralException.VerificarTamanhoMenorString(nome, 3, "O nome do medicamento deve ter no mínimo 3 caracteres.");
        //            Nome = nome;
        //        }
        //        catch (ArgumentException ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    } while (Nome == "");

        //    do
        //    {
        //        Console.WriteLine("Digite a categoria do medicamento (A , B, I, V):");
        //        validadeCategoria = char.TryParse(Console.ReadLine().ToUpper(), out categoria);

        //        if (validadeCategoria == false)
        //        {
        //            Console.WriteLine("O medicamento deve estar em uma das 4 categorias");
        //        }
        //    } while (categoria != 'A' || categoria != 'B' || categoria != 'I' || categoria != 'V');

        //    decimal valorVenda = 0;
        //    do
        //    {
        //        valorVenda = InputHelper.RetornarNumeroDecimal("Digite o valor de Venda:", "O valor deve ser maior que R$0,00 e menor que R$10000,00.");
        //    } while (valorVenda <= 0 || valorVenda >= 10000);

        //    Console.WriteLine("Digite a data da ultima venda (DD/MM/AAAA): ");
        //    DateOnly ultimaVenda = DateOnly.Parse(Console.ReadLine());

        //    Console.WriteLine("Digite a data do cadastro do medicamento (DD/MM/AAAA): ");
        //    DateOnly dataCadastro = DateOnly.Parse(Console.ReadLine());

        //    do
        //    {
        //        Console.WriteLine("Digite a situação do medicamento (A, I): ");
        //        validadeSituacao = char.TryParse(Console.ReadLine().ToUpper(), out situacao);
        //        if (validadeSituacao == false)
        //        {
        //            Console.WriteLine("");
        //        }
        //    } while (situacao != 'A' || situacao != 'I');

        //    Medicine medicamento = new Medicine(cdb, nome, categoria, valorVenda, ultimaVenda, dataCadastro, situacao);

        //    this.Medicamentos.Add(medicamento);

        //}

        public Medicine LocalizarMedicamento(string id)
        {
            Console.WriteLine("Digite o código de barras do medicamento que deseja procurar: ");
            string localizar = Console.ReadLine();
            return Medicamentos.Find(m => m.CDB == localizar);
        }

        private Medicine LocalizarMedicamentoAtivo(string cdb)
        {
            return Medicamentos.Find(m => m.CDB == cdb && m.Situacao == 'A');
        }

        //public Medicine AlterarMedicamento()
        //{
        //    Console.WriteLine("Digite o código de barras do medicamento que deseja alterar:");
        //    string alterar = Console.ReadLine() ?? "";


        //    Medicine medicine = LocalizarMedicamento(alterar);

        //    Console.WriteLine("Deseja alterar o nome do medicamento? (S , N):");
        //    char escolha = char.Parse(Console.ReadLine().ToUpper());

        //    if (escolha == 'S')
        //    {
        //        if (medicine is not null)
        //        {
        //            Console.WriteLine("Digite o nome que deseja atribuir ao remédio: ");
        //            medicine.setNome(Console.ReadLine());

        //            Console.WriteLine("Nome atualizado com sucesso.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Não existe esse medicamento.");
        //        }

        //        return medicine;
        //    }
        //    Console.WriteLine("Deseja alterar a situação do medicamento? (S , N):");
        //    escolha = char.Parse(Console.ReadLine().ToUpper());
        //    if (escolha == 'S')
        //    {
        //        if (medicine is not null)
        //        {
        //            if (medicine.Situacao == 'A')
        //                medicine.setSituacao('I');
        //            else
        //                medicine.setSituacao('A');

        //        }
        //        return medicine;
        //    }
        //    else
        //    {
        //        return medicine;
        //    }
        //}

        public void ListarMedicamentos()
        {
            Console.WriteLine("===Lista de medicamentos ===");
            foreach (var medicine in Medicamentos)
            {
                Console.WriteLine(medicine);
            }
        }

        #endregion

        #region Operações de CRUD da classe Purchase

        public void CriarPurchase()
        {
            string cnpj;
            do
            {
                cnpj = InputHelper.RetornarString("Digite o CNPJ do fornecedor:", "O CNPJ é inválido.");
                var fornecedor = this.Fornecedores.Find(f => f.Cnpj == cnpj);
                if (fornecedor != null)
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

        #region Operações de CRUD da classe PurchaseItem

        //public void CriarItemCompra()
        //{
        //    do
        //    {
        //        Console.WriteLine("Digite o ID do princípio ativo que deseja comprar: ");
        //        string ingrediente = Console.ReadLine();


        //        decimal quantidade = 0;
        //        do
        //        {
        //            quantidade = InputHelper.RetornarNumeroDecimal("Digite a quantidade de itens em gramas (máx: 999,99): ", "O valor deve ser maior que 0 e menor que 10000");
        //        } while (quantidade <= 0 || quantidade >= 10000);

        //        decimal valorUnitario = 0;
        //        do
        //        {
        //            valorUnitario = InputHelper.RetornarNumeroDecimal("Digite o valor unitário por grama (máx: 999,99):", "O valor digitado deve ser maior que 0 e menor que 1000");
        //        } while (valorUnitario <= 0 || valorUnitario >= 1000);

        //        decimal totalItem = quantidade * valorUnitario;
        //    } while (opcao)


        //}

        #endregion
    }
}
