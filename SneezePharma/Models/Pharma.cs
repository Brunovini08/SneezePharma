using SneezePharma.Enums;
using SneezePharma.Exceptions;
using SneezePharma.Helpers;
using SneezePharma.Models.Customer_Manipulation_;
using SneezePharma.Models.Ingredient;
using SneezePharma.Models.Medicine;
using SneezePharma.Models.Produce;
using SneezePharma.Models.Produce.Produce;
using SneezePharma.Models.PurchaseItem;
using SneezePharma.Models.Purchases;
using SneezePharma.Models.RestrictedCustomers;
using SneezePharma.Models.Sales;
using SneezePharma.Models.SalesItem;
using SneezePharma.Models.Supplier_Manipulation_;
using SneezePharma.Utils;
using System.Text.RegularExpressions;

namespace SneezePharma.Models
{
    public class Pharma
    {
        public List<CustomerModel> Clientes { get; private set; }
        public List<SupplierModel> Fornecedores { get; private set; }
        public List<MedicineModel> Medicamentos { get; private set; }
        public List<ProduceModel> Producao { get; set; }
        public List<ProduceItemModel> ItensProducao { get; private set; }
        public List<RestrictedCustomerModel> ClientesRestritos { get; private set; }
        public List<RestrictedSupplierModel> FornecedoresRestritos { get; private set; }
        public List<SalesModel> Vendas { get; private set; }
        public List<SalesItemModel> ItensDeVenda { get; private set; }
        public List<IngredientModel> Ingredientes { get; private set; }
        public List<PurchaseModel> Compra { get; private set; }
        public List<PurchaseItemModel> ItensDaCompra { get; private set; }


        private SalesItemManipulate salesItemManipulate = new SalesItemManipulate();

        private CustomerManipulate customerManipulate = new CustomerManipulate();

        private RestrictedCustomerManipulate restrictedCustomerManipulate = new RestrictedCustomerManipulate();

        private SupplierManipulate supplierManipulate = new SupplierManipulate();

        private IngredientManipulation ingredientManipulation = new IngredientManipulation();

        private RestrictedSupplierManipulation restrictedManipulation = new RestrictedSupplierManipulation();

        private SalesManipulate salesManipulate = new SalesManipulate();

        private MedicineManipulate medicineManipulate = new MedicineManipulate();

        private PurchaseItemManipulate purchaseItemManipulate = new PurchaseItemManipulate();

        private PurchaseManipulate purchaseManipulate = new PurchaseManipulate();

        private ProduceManipulate produceManipulate = new ProduceManipulate();

        private ProduceItemManipulate produceItemManipulate = new ProduceItemManipulate();
        public Pharma()
        {
            this.Clientes = new List<CustomerModel>();
            this.Clientes = customerManipulate.Ler();

            this.Medicamentos = new List<MedicineModel>();
            this.Medicamentos = this.medicineManipulate.Ler();

            this.ItensDaCompra = new List<PurchaseItemModel>();
            this.ItensDaCompra = this.purchaseItemManipulate.Ler();

            this.Compra = new List<PurchaseModel>();
            this.Compra = this.purchaseManipulate.Ler();

            this.ClientesRestritos = new List<RestrictedCustomerModel>();
            this.ClientesRestritos = restrictedCustomerManipulate.Ler();

            this.Fornecedores = new List<SupplierModel>();
            this.Fornecedores = supplierManipulate.Ler();

            this.FornecedoresRestritos = new List<RestrictedSupplierModel>();
            this.FornecedoresRestritos = restrictedManipulation.Ler();

            this.Ingredientes = new List<IngredientModel>();
            this.Ingredientes = ingredientManipulation.Ler();

            this.Vendas = new List<SalesModel>();
            this.Vendas = salesManipulate.Ler();

            this.ItensDeVenda = new List<SalesItemModel>();
            this.ItensDeVenda = salesItemManipulate.Ler();

            this.Producao = new List<ProduceModel>();
            this.Producao = this.produceManipulate.Ler();

            this.ItensProducao = new List<ProduceItemModel>();
            this.ItensProducao = this.produceItemManipulate.Ler();
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
                    case 2:
                        AdicionarFornecedor();
                        break;
                    case 3:
                        RegistrarMedicamento();
                        break;
                    case 4:
                        RegistrarIngrediente();
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
            int opcao;
            bool validar;
            do
            {
                Menu.MenuCompraPrincipioAtivo();
                validar = int.TryParse(Console.ReadLine(), out opcao);
                switch (opcao)
                {
                    case 1:
                        RegistrarCompra();
                        break;
                    case 2:
                        AlterarCompra();
                        break;
                    case 3:
                        ListarCompras();
                        break;
                    case 4:
                        ListarCompraPorId();
                        break;
                    default:
                        break;
                }
            } while (validar == false);
        }
        public void ManipularMedicamentos()
        {
            int opcao;
            bool validar;
            Menu.MenuManipulacaoMedicamentos();
            validar = int.TryParse(Console.ReadLine(), out opcao);
            switch (opcao)
            {
                case 1:
                    LocalizarMedicamentoAtivo();
                    break;
                case 2:
                    AlterarSituacaoMedicamento();
                    break;
                case 3:
                    LocalizarMedicamento();
                    break;
                case 4:
                    ListarMedicamentos();
                    break;
                case 5:

                case 0:
                    break;
                default:
                    break;
            }
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
                        AtualizarCliente();
                        break;
                    case 2:
                        InativarCliente();
                        break;
                    case 3:
                        AdicionarClienteRestrito();
                        break;
                    case 4:
                        RemoverClienteRestrito();
                        break;
                    case 5:
                        ListarCliente();
                        break;
                    case 6:
                        ListarClientes();
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
                        AtualizarRazaoSocial();
                        break;
                    case 2:
                        AlterarSituacaoFornecedor();
                        break;
                    case 3:
                        AdicionarFornecedorRestrito();
                        break;
                    case 4:
                        RemoverFornecedorRestrito();
                        break;
                    case 5:
                        ListarFornecedorPorCNPJ();
                        break;
                    case 6:
                        ListarFornecedores();
                        break;
                    case 7:
                        ListarFornecedoresRestritos();
                        break;
                    case 8:
                        ListarFornecedorBloqueadoPorCNPJ();
                        break;
                    case 0:
                        break;

                }
            } while (validar == false);
        }
        public void ManipularPrincipioAtivo()
        {
            int opcao;
            bool validar;
            do
            {
                Menu.MenuManipulacaoPrincipioAtivo();
                validar = int.TryParse(Console.ReadLine(), out opcao);
                switch (opcao)
                {
                    case 1:
                        AlterarNomeIngrediente();
                        break;
                    case 2:
                        AlterarSituacao();
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            } while (validar == false);
        }
        public void ManipularProducao()
        {
            int opcao;
            bool validar;
            do
            {
                Menu.MenuProducao();
                validar = int.TryParse(Console.ReadLine(), out opcao);
                switch (opcao)
                {
                    case 1:
                        RegistrarProducao();
                        break;
                    case 2:
                        AlterarQuantidadeDaProducao();
                        break;
                    case 3:
                        BuscarProducaoPorId();
                        break;
                    case 4:
                        ListarProducao();
                        break;
                    default:
                        break;
                }
            } while (validar == false);
        }
        public void Relatorios()
        {
            int opcao;
            bool validar;
            do
            {
                Menu.MenuManipulacaoPrincipioAtivo();
                validar = int.TryParse(Console.ReadLine(), out opcao);
                switch (opcao)
                {
                    case 1:
                        RelatorioVendasPeriodo();
                        break;
                    case 2:
                        RelatorioMedicamentosMaisVendidos();
                        break;
                    case 3:
                        RelatorioComprasFornecedor();
                        break;
                    default:
                        Console.WriteLine("Digite um valor do menu!");
                        break;
                }
            } while (validar == false);
        }

        #region Operações de CRUD de Ingrediente

        string GerarProximoId()
        {
            var id = this.Ingredientes.LastOrDefault()?.Id ?? "";
            if (id == "")
            {
                return "AI0001";
            }
            else
            {
                var numero = id[2..6];
                var numeroConvertido = int.Parse(numero);
                numeroConvertido++;

                return $"AI{numeroConvertido.ToString().PadLeft(4, '0')}";
            }
        }
        public bool VerificarNome(string nome)
        {
            foreach (var caractere in nome)
            {
                if (!char.IsLetterOrDigit(caractere))
                {

                    return false;
                }
            }
            return true;
        }
        public void RegistrarIngrediente()
        {
            var id = this.Ingredientes.LastOrDefault().Id ?? "AI0001";
            string nome;
            DateOnly ultimaCompra;
            do
            {
                nome = InputHelper.RetornarString("Digite o nome do ingrediente: ", "Nome do ingrediente inválido, o ingrediente deve ter até 20 caracteres e alfanumericos");
            } while (nome.Length > 20 || !VerificarNome(nome));



            Ingredientes.Add(new IngredientModel(id, nome));
        }
        public IngredientModel LocalizarIngrediente(string Id)
        {
            return Ingredientes.Find(i => i.Id == Id);
        }
        public void AlterarNomeIngrediente()
        {
            string id;
            do
            {
                id = InputHelper.RetornarString("Digite o id do ingrediente que deseja alterar: ", "Por favor, digite um id");
                var ingrediente = LocalizarIngrediente(id);
                if (id != null)
                {
                    string nome;
                    do
                    {
                        nome = InputHelper.RetornarString("Digite o novo nome: ", "Por favor, digite o nome");
                    } while (nome == string.Empty);

                    ingrediente.setNome(nome);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Nome do ingrediente alterado com sucesso!");
                    Console.ResetColor();
                }
                else
                    Console.WriteLine("Ingrediente não existe");
            } while (id == string.Empty);
        }
        public void AlterarSituacao()
        {
            string id;
            do
            {
                id = InputHelper.RetornarString("Digite o id do ingrediente que deseja alterar a situação: ", "Por favor, digite um id");
                var ingrediente = LocalizarIngrediente(id);
                if (id != null)
                {
                    string situacao;
                    do
                    {
                        situacao = InputHelper.RetornarString("Digite a nova situação: ", "Por favor, digite a nova situação: A - Ativo | I - Inativo").ToUpper();
                    } while (situacao != "A" && situacao != "I");
                    situacao = Console.ReadLine();

                    if (situacao.ToString() == "A" && ingrediente.Situacao == SituationIngredient.A)
                    {
                        Console.WriteLine("O ingrediente já está ativo");
                    }
                    else if (situacao.ToString() == "I" && ingrediente.Situacao == SituationIngredient.I)
                    {
                        Console.WriteLine("O ingrediente já está inativo");
                    }
                    else if (situacao.ToString() == "A")
                        ingrediente.setSituacao(SituationIngredient.A);
                    else
                        ingrediente.setSituacao(SituationIngredient.I);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Situação do ingrediente alterado com sucesso!");
                    Console.ResetColor();
                }
                else
                    Console.WriteLine("Ingrediente não existe");
            } while (id == string.Empty);



        }
        public void ListarIngrediente()
        {
            if (Ingredientes.Count == 0)
            {
                Console.WriteLine("Não há nenhum ingrediente no momento");
            }
            else
            {
                Console.WriteLine("Lista de ingredientes: ");
                foreach (var ingrediente in Ingredientes)
                {
                    Console.WriteLine(ingrediente.ToString());
                }
            }
        }
        #endregion

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
                                CustomerModel cliente = this.Clientes.Find(c => c.CPF == cpf);
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
                DateOnly dataNascimento;
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
                } while (dataNascimento == null);
                Console.Clear();
                string telefone;
                do
                {
                    telefone = InputHelper.RetornarString("Digite seu telefone com DDD: ", "Por favor, digite o número de telefone com DDD!");
                } while (telefone.Length != 11);
                Console.Clear();
                CustomerModel customer = new CustomerModel(cpf, nome, dataNascimento, telefone);
                this.Clientes.Add(customer);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Cliente cadastrado com sucesso");
                customerManipulate.Gravar(this.Clientes);
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
        public CustomerModel BuscarPorCPF(string cpf)
        {
            return this.Clientes.Find(c => c.CPF == cpf);
        }
        public void ListarClientes()
        {
            Console.WriteLine("LISTAR CLIENTES: ");
            Console.WriteLine();
            foreach (var cliente in this.Clientes)
            {
                Console.WriteLine(cliente);
            }
        }
        public void InativarCliente()
        {
            string cpf;
            CustomerModel cliente;
            do
            {
                cpf = InputHelper.RetornarString("Digite o CPF do cliente que deseja inativar: ", "Por favor, Digite o CPF!");
                cliente = this.BuscarPorCPF(cpf);
                if (cliente is not null)
                {
                    cliente.setSituacao(Enums.SituationCustomer.I);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Cliente {cliente.Nome} está inadimplente");
                    Console.ResetColor();
                }
                else
                {
                    InputHelper.ExibirErro("Cliente não existe!");
                    InputHelper.PressioneEnterParaContinuar();
                    Console.Clear();
                }
            } while (cpf is null || cliente is null);
        }
        public void ListarCliente()
        {
            Console.WriteLine("LISTAR CLIENTE");
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
                            CustomerModel cliente = this.Clientes.Find(c => c.CPF == cpf);
                            if (cliente != null)
                            {
                                Console.WriteLine(cliente);
                            }
                            else
                            {
                                InputHelper.ExibirErro("Cliente não encontrado!");
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
        }
        #endregion

        #region Operações de CRUD da classe Sales e SalesItem
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

        private void RegistrarVenda()
        {
            var id = this.Vendas.LastOrDefault()?.Id ?? 0;

            var cpf = EntradaCdbECpf(
                "Digite o CPF do Cliente:",
                "Total de dígitos do CPF não bate! (CPF possui 11 dígitos)",
                11
            );

            var cliente = BuscarPorCPF(cpf);

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
                this.Vendas.Add(venda);
                this.salesManipulate.Gravar(this.Vendas);
                this.salesItemManipulate.Gravar(this.ItensDeVenda);
            }
            InputHelper.PressioneEnterParaContinuar();
        }

        private void AlterarItensDaVenda()
        {
            if (this.Vendas.Count == 0)
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

            var cliente = BuscarPorCPF(cpf);

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
            salesManipulate.Gravar(this.Vendas);
            salesItemManipulate.Gravar(this.ItensDeVenda);
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
                        this.Vendas.Remove(venda);
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
                var novaQuantidade = ConfirmacaoQuantidade("Digite a nova quantidade:", 999);

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
            return this.Vendas.Find(v => v.Id == id);
        }

        private List<SalesModel> BuscarVendasPorCpfDoCliente(string cpf)
        {
            return this.Vendas.FindAll(v => v.Cliente == cpf);
        }

        private List<SalesItemModel> BuscarItensDeVenda(int idVenda)
        {
            return this.ItensDeVenda.FindAll(iv => iv.IdVenda == idVenda);
        }
        private void BuscarVendaPorId()
        {
            if (this.Vendas.Count == 0)
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
        private void ListarVendas()
        {
            if (this.Vendas.Count > 0)
            {
                Console.WriteLine("======= VENDAS =======");
                foreach (var v in this.Vendas)
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

        private int ConfirmacaoQuantidade(string msgEntrada, int quantidadeMax)
        {
            var quantidade = 0;
            var repetir = true;

            do
            {
                try
                {
                    quantidade = InputHelper.RetornarNumeroInteiro(msgEntrada);
                    GeneralException.VerificarQuantidadeInvalidaInteiro(1, quantidadeMax, quantidade, "Quantidade inválida! A quantidade deve ser 1 a 999");

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

        private SalesItemModel CadastrarItemDeVenda(SalesModel venda, MedicineModel medicamento)
        {
            var id = this.ItensDeVenda.LastOrDefault()?.Id ?? 0;

            try
            {
                var idVenda = venda.Id;
                var cdbMedicamento = medicamento.CDB;
                var quantidade = ConfirmacaoQuantidade("Digite a quantidade:", 999);

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
                DateOnly dataAbertura;
                do
                {
                    dataAbertura = InputHelper.RetornarData("Digite a data de abertura (no modelo: DDMMAAAA): ", "Data de abertura inválida");
                } while (dataAbertura == null);

                
                this.Fornecedores.Add(new SupplierModel(cnpj, razaoSocial,
                pais, dataAbertura));
                supplierManipulate.Gravar(this.Fornecedores);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public SupplierModel LocalizarFornecedor(string cnpj)
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
            var fornecedor = LocalizarFornecedor(Cnpj);

            if (Cnpj != null)
            {
                string situacao;
                do
                {
                    situacao = InputHelper.RetornarString("Informe a situação que deseja: ", "Situação inválida, digite novamente: ");

                } while (situacao != "A" && situacao != "I");

                fornecedor.setSituacao(situacao == "A" ? Enums.SituationSupplier.A : Enums.SituationSupplier.I);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Fornecedor alterado com sucesso!");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
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
            foreach (var fornecedor in this.Fornecedores)
            {
                Console.WriteLine(fornecedor);
            }
        }
        public void ListarFornecedorPorCNPJ()
        {
            string cnpj;
            do
            {
                Console.WriteLine("Digite o Cnpj que deseja alterar a razão social:");
                cnpj = Console.ReadLine();
                var fornecedor = this.LocalizarFornecedor(cnpj);

                if (fornecedor != null)
                {
                    Console.WriteLine(fornecedor);
                }
                else
                {
                    Console.WriteLine("Fornecedor não encontrado");
                }
            } while (cnpj.Length != 14 || cnpj is null);
        }
        #endregion

        #region Operação de CDR da classe RestrictedSupplier

        public void AdicionarFornecedorRestrito()
        {
            try
            {
                RestrictedSupplierModel fornecedorBloquear;
                SupplierModel fornecedor;
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
                RestrictedSupplierModel fornecedorLiberar;
                SupplierModel fornecedor;
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
        public RestrictedSupplierModel BuscarFornecedorRestrito(string cnpj)
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
        public void ListarFornecedorBloqueadoPorCNPJ()
        {
            string cnpj;
            do
            {
                cnpj = InputHelper.RetornarString("Digite o CNPJ do Fornecedor Bloqueado que deseja buscar :", "CNPJ Inválido");
                var fornecedor = this.LocalizarFornecedor(cnpj);

                if (fornecedor != null)
                {
                    Console.WriteLine(fornecedor);
                }
                else
                {
                    Console.WriteLine("Fornecedor não encontrado");
                }
            } while (cnpj.Length != 14 || cnpj is null);
        }
        #endregion

        #region Operação de CDR da classe RestrictedCustomer
        public void AdicionarClienteRestrito()
        {
            try
            {
                RestrictedCustomerModel clienteBloquear;
                CustomerModel cliente;
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
                RestrictedCustomerModel clienteLiberar;
                CustomerModel cliente;
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
        public RestrictedCustomerModel BuscarClienteRestrito(string cpf)
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

        private void RegistrarProducao()
        {
            var id = this.Producao.LastOrDefault()?.ID ?? 0;

            var cdb = EntradaCdbECpf(
                "Digite o CDB do Medicamento:",
                "Total de dígitos do CDB não bate! (CDB possui 13 dígitos)",
                13
            );

            var medicamento = this.Medicamentos.Find(m => m.CDB == cdb && m.Situacao == 'A');

            if (medicamento is null)
            {
                Console.WriteLine("Não consta esse medicamento no sistema!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }

            var quantidade = ConfirmacaoQuantidade("Digite a quantidade:", 999);

            var producao = new ProduceModel(++id, medicamento.CDB, quantidade);

            var idItemDeProducao = ItensProducao.LastOrDefault()?.IdProducao ?? 0;
            var ingredientesAtivos = Ingredientes.FindAll(i => i.Situacao == SituationIngredient.A);
            var repetir = true;
            do
            {
                Console.WriteLine("====== INGREDIENTES ATIVOS ======");
                foreach (var i in ingredientesAtivos)
                {
                    Console.WriteLine(i);
                    Console.WriteLine();
                }

                Console.WriteLine("Digite o ID do ingrediente para gerar a produção do medicamento:");
                var idIngrediente = Console.ReadLine() ?? "";

                var localizar = ingredientesAtivos.Find(i => i.Id == idIngrediente);

                if (localizar is null)
                {
                    Console.WriteLine("Não existe esse ingrediente em nosso sistema!");
                    continue;
                }
                else
                {
                    var quantidadeIngrediente = ConfirmacaoQuantidade("Digite a quantidade:", 9999);

                    var itemDeProducao = new ProduceItemModel(
                        ++idItemDeProducao,
                        producao.ID,
                        localizar.Id,
                        quantidadeIngrediente
                    );

                    ItensProducao.Add(itemDeProducao);
                    ingredientesAtivos.Remove(localizar);
                }

                repetir = RealizarPerguntaDeConfirmacao("Deseja cadastrar mais um principio ativo? (1 - Sim|0 - Não");
            }
            while (repetir);

            //TODO: Salvar no arquivo
        }

        private void AlterarQuantidadeDaProducao()
        {
            if (!TemProducoesCadastradas())
            {
                Console.WriteLine("Não há produções cadastradas!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }

            ListarProducao();

            var id = InputHelper.RetornarNumeroInteiro("Digite o ID da produção que deseja alterar a quantidade:");

            var producao = BuscarProducaoPeloId(id);

            if (producao is null)
            {
                Console.WriteLine($"Não foi encontrada nenhuma produção com este ID: {id}");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }
            else
            {
                var quantidade = ConfirmacaoQuantidade("Digite a nova quantidade:", 999);

                producao.setQuantidade(quantidade);
            }

            //TODO: Salvar no arquivo

        }

        private void BuscarProducaoPorId()
        {
            var id = InputHelper.RetornarNumeroInteiro("Digite o ID da produção que deseja consultar:");

            var producao = BuscarProducaoPeloId(id);

            if (producao is null)
            {
                Console.WriteLine($"Não foi encontrada nenhuma produção com este ID: {id}");
            }
            else
            {
                Console.WriteLine("======= PRODUÇÃO =======");
                Console.WriteLine(producao);
            }
            InputHelper.PressioneEnterParaContinuar();
        }

        private ProduceModel BuscarProducaoPeloId(int id)
        {
            return Producao.Find(p => p.ID == id);
        }

        private void ListarProducao()
        {
            if (TemProducoesCadastradas())
            {
                Console.WriteLine("========= PRODUÇÕES =========");
                foreach (var p in Producao)
                {
                    Console.WriteLine(p);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Não há nenhuma produção cadastrada no momento!");
            }
            InputHelper.PressioneEnterParaContinuar();
        }

        private bool TemProducoesCadastradas()
        {
            return Producao.Count > 0;
        }

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
        public void RegistrarMedicamento()
        {
            bool validadeCDB = false, validadeNome = false, validadeCategoria = false, validadeSituacao = false;
            string cdb, nome = "";
            char categoria = ' ', situacao = ' ';

            do
            {
                Console.Clear();
                Console.WriteLine("Digite o código de barras do medicamento:");
                cdb = Console.ReadLine();

                validadeCDB = InputHelper.ValidarCDB(cdb);
                InputHelper.PressioneEnterParaContinuar();
            } while (!validadeCDB);
            do
            {
                do
                {
                    nome = InputHelper.RetornarString("Digite o nome do medicamento", "O campo de nome deve ser preenchido");
                    validadeNome = nome.All(c => char.IsLetterOrDigit(c));
                    if (validadeNome == false)
                    {
                        Console.WriteLine("O nome do medicamento deve conter apenas letras e números.");
                        InputHelper.PressioneEnterParaContinuar();
                    }
                } while (!validadeNome);
                try
                {
                    Exceptions.GeneralException.VerificarTamanhoMaiorString(nome, 40, "O nome do medicamento deve ter no máximo 100 caracteres.");
                    Exceptions.GeneralException.VerificarTamanhoMenorString(nome, 3, "O nome do medicamento deve ter no mínimo 3 caracteres.");

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    InputHelper.PressioneEnterParaContinuar();
                    Console.Clear();
                }
                
            } while (nome is null);

            do
            {
                Console.WriteLine("Digite a categoria do medicamento (A , B, I, V):");
                validadeCategoria = char.TryParse(Console.ReadLine().ToUpper(), out categoria);
                if(validadeCategoria == false)
                {
                    Console.WriteLine("A categoria de remédio deve ser (A, B, I , V): ");
                    InputHelper.PressioneEnterParaContinuar();
                    Console.Clear();
                }

            } while (categoria != 'A' && categoria != 'B' && categoria != 'I' && categoria != 'V');

            decimal valorVenda = 0;
            do
            {
                valorVenda = InputHelper.RetornarNumeroDecimal("Digite o valor de Venda:", "O valor deve ser maior que R$0,00 e menor que R$10000,00.");
            } while (valorVenda <= 0 || valorVenda >= 10000);

            MedicineModel medicamento = new MedicineModel(cdb, nome, categoria, valorVenda);

            this.Medicamentos.Add(medicamento);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Medicamento cadastrado com sucesso");
            medicineManipulate.Gravar(this.Medicamentos);
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }

        public MedicineModel LocalizarMedicamento(string id)
        {
            Console.WriteLine("Digite o código de barras do medicamento que deseja procurar: ");
            string localizar = Console.ReadLine();
            return Medicamentos.Find(m => m.CDB == localizar);
        }

        public MedicineModel LocalizarMedicamento()
        {
            Console.WriteLine("Digite o código de barras do medicamento que deseja procurar: ");
            string localizar = Console.ReadLine();

            MedicineModel medicamento = Medicamentos.Find(m => m.CDB == localizar);

            Console.WriteLine(medicamento);

            return medicamento;
        }

        private void LocalizarMedicamentoAtivo()
        {
            MedicineModel medicine = LocalizarMedicamento();

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
            }
        }

        public void AlterarSituacaoMedicamento()
        {
            MedicineModel medicineSituacao = LocalizarMedicamento();

            Console.WriteLine("Deseja alterar a situação do medicamento? (S , N):");
            char escolhaSituacao = char.Parse(Console.ReadLine().ToUpper());
            if (escolhaSituacao == 'S')
            {
                if (medicineSituacao is not null)
                {
                    if (medicineSituacao.Situacao == 'A')
                    {
                        medicineSituacao.setSituacao('I');
                        Console.WriteLine("Situação alterada para Inativo!");
                    }
                    else
                    {
                        medicineSituacao.setSituacao('A');
                        Console.WriteLine("Situação alterada para Ativo!");
                    }
                }
            }
        }

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

        public void RegistrarCompra()
        {
            string cnpj;
            PurchaseModel compra = null;
            do
            {
                cnpj = InputHelper.RetornarString("Digite o CNPJ do fornecedor:", "O CNPJ é inválido.");
                var fornecedor = this.Fornecedores.Find(f => f.Cnpj == cnpj);
                var fornecedorBloqueado = this.FornecedoresRestritos.Find(fb => fb.Cnpj == cnpj);

                TimeSpan tempoEmpresa = fornecedor.DataAbertura - DateOnly.FromDateTime(DateTime.Now);

                //aaaa

                if (fornecedor != null && fornecedorBloqueado == null && fornecedor.Situacao != SituationSupplier.I)
                {
                    compra = new PurchaseModel(fornecedor.Cnpj);
                }
            }
            while (cnpj.Length != 14 || compra == null);

            var itensCompra = this.ItensDaCompra.FindAll(i => i.IdCompra == compra.IdCompra);

            decimal valorTotal = 0;

            foreach (var itens in itensCompra)
            {
                valorTotal += itens.TotalItem;
            }

            compra.setValorTotal(valorTotal);


            Compra.Add(compra); 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Compra cadastrada com sucesso");
            purchaseManipulate.Gravar(this.Compra);
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();

        }
        public PurchaseModel BuscarCompra()
        {
            var localizar = InputHelper.RetornarNumeroInteiro("Digite o ID da compra que deseja procurar:");
            var localizadoCompra = Compra.Find(i => i.IdCompra == localizar);

            if (localizadoCompra != null)
                return localizadoCompra;
            else
                return null;
        }
        public void ListarCompraPorId()
        {
            var localizar = InputHelper.RetornarNumeroInteiro("Digite o ID da compra que deseja procurar:");
            var localizadoCompras = Compra.FindAll(i => i.IdCompra == localizar);

            if (localizadoCompras != null)
                Console.WriteLine("Não existe compra para esse ingrediente");
            else
            {
                foreach (var compra in localizadoCompras)
                {
                    Console.WriteLine(compra);
                }
            }
        }
        public void AlterarCompra()
        {
            PurchaseModel compra = BuscarCompra();
            Console.WriteLine("Deseja alterar a quantidade de item? (S , N):");
            char escolha = char.Parse(Console.ReadLine().ToUpper());
            if (escolha == 'S')
            {
                if (compra is not null)
                {
                    do
                    {
                        Console.WriteLine("Digite o valor total da compra corrigido (máx: 99999999,99): ");
                        compra.setValorTotal(decimal.Parse(Console.ReadLine()));
                    } while (compra.ValorTotal <= 0 || compra.ValorTotal >= 10000);
                    Console.WriteLine("Valor atualizado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Não existe esse item da compra.");
                }
            }
        }
        public void ListarCompras()
        {
            Console.WriteLine("===Lista de Itens da Compra ===");
            foreach (var compra in Compra)
            {
                Console.WriteLine(compra);
            }
        }
        #endregion

        #region Operações de CRUD da classe PurchaseItem

        public void CriarItemCompra()
        {
            int opcao = 0;
            int contador = 0;
            string idIngrediente = "";
            decimal totalItem = 0;
            var id = this.ItensDaCompra.LastOrDefault()?.IdCompra ?? 0;
            do
            {
                IngredientModel localizar;
                do
                {
                    Console.WriteLine("Digite o ID do principio ativo da compra: ");
                    idIngrediente = Console.ReadLine();

                    localizar = LocalizarIngrediente(idIngrediente);

                    if (localizar is null)
                    {
                        Console.WriteLine("O ID digitado não existe no sistema");
                    }

                } while (localizar == null);

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

                totalItem = (quantidade * valorUnitario);

                Console.WriteLine("Deseja adicionar comprar outro item? (1 - Sim, 2 - Não): ");
                opcao = int.Parse(Console.ReadLine());
                contador++;
                if (contador == 3)
                {
                    Console.WriteLine("O limite de compras (3 itens) foi atingido!");
                }
                PurchaseItemModel itemDaCompra = new PurchaseItemModel(++id, idIngrediente, quantidade, valorUnitario, totalItem);

                ItensDaCompra.Add(itemDaCompra);

            } while (opcao != 2 || contador == 3);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Item da compra cadastrado com sucesso");
            purchaseItemManipulate.Gravar(this.ItensDaCompra);
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();

        }

        public PurchaseItemModel LocalizarItemCompra()
        {

            var localizar = InputHelper.RetornarNumeroInteiro("Digite o ID da compra que deseja procurar:");
            var localizadoItemCompra = ItensDaCompra.Find(i => i.IdCompra == localizar);

            return localizadoItemCompra;
        }

        public PurchaseItemModel AlterarItemCompra()
        {
            PurchaseItemModel itemCompra = LocalizarItemCompra();

            Console.WriteLine("Deseja alterar a quantidade de item? (S , N):");
            char escolha = char.Parse(Console.ReadLine().ToUpper());

            if (escolha == 'S')
            {
                if (itemCompra is not null)
                {
                    do
                    {
                        Console.WriteLine("Digite a quantidade que deseja alterar em gramas (máx: 9999,99): ");
                        itemCompra.setQuantidade(decimal.Parse(Console.ReadLine()));
                    } while (itemCompra.Quantidade <= 0 || itemCompra.Quantidade >= 10000);

                    Console.WriteLine("Quantidade alterada com sucesso.");
                }
                else
                {
                    Console.WriteLine("Não existe esse item da compra.");
                }

            }
            return itemCompra;
        }
        public void ListarItemCompra()
        {
            Console.WriteLine("===Lista de Itens da Compra ===");
            foreach (var itemCompra in ItensDaCompra)
            {
                Console.WriteLine(itemCompra);
            }
        }

        #endregion

        #region Relatorio de Vendas por periodo

        public void RelatorioVendasPeriodo()
        {
            Console.WriteLine("RELATÓRIO DE VENDAS POR PERÍODO");

            Console.WriteLine();
            DateOnly dataInicial = InputHelper.RetornarData("Digite a data inicial: ", "Digite uma data válida");
            Console.Clear();

            DateOnly dataFinal = InputHelper.RetornarData("Digite a data final: ", "Digite uma data válida");

            var relatorio = this.Vendas.Where(v => v.DataVenda >= dataInicial && v.DataVenda <= dataFinal).ToList();

            foreach (var item in relatorio)
            {
                Console.WriteLine(item);
            }
        }

        #endregion

        #region Relatorio Medicamentos mais vendidos
        public void RelatorioMedicamentosMaisVendidos()
        {
            Console.WriteLine("RELATÓRIO DE MEDICAMENTOS MAIS VENDIDOS");


            var relatorio = this.ItensDeVenda.OrderBy(itens => itens.Medicamento).ToList().Take(3);

            foreach (var item in relatorio)
            {
                Console.WriteLine(item);
            }
        }

        #endregion

        #region Relatorio de Compras por fornecedor
        public void RelatorioComprasFornecedor()
        {
            Console.WriteLine("RELATÓRIO DE COMPRAS POR FORNECEDOR");
            Console.WriteLine();

            string cpnj;
            do
            {
                cpnj = InputHelper.RetornarString("Digite o CNPJ do Fornecedor que deseja consultar vendas: ", "CNPJ inválido");
                if (!InputHelper.ValidarCnpj(cpnj))
                {
                    InputHelper.ExibirErro("CNPJ Inválido!");
                }
            } while (!InputHelper.ValidarCnpj(cpnj));

            var relatorio = this.Compra.FindAll(c => c.Fornecedor == cpnj).ToList();

            foreach (var item in relatorio)
            {
                Console.WriteLine(item);
            }
        }
        #endregion
    }
}
