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
using System.ComponentModel.DataAnnotations;
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
            bool repetir = true;

            do
            {
                Menu.MenuCadastros();
                var opcao = Console.ReadLine() ?? "-1";

                switch (opcao)
                {
                    case "1":
                        RegistrarCliente();
                        break;
                    case "2":
                        RegistrarFornecedor();
                        break;
                    case "3":
                        RegistrarMedicamento();
                        break;
                    case "4":
                        RegistrarIngrediente();
                        break;
                    case "0":
                        repetir = false; // Voltar para o menu principal
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Selecione uma opção do menu!");
                        InputHelper.PressioneEnterParaContinuar();
                        break;
                }
            } while (repetir);
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
                        Console.WriteLine("Opção inválida! Selecione uma opção do menu!");
                        InputHelper.PressioneEnterParaContinuar();
                        break;
                }
            }
            while (repetir);
        }
        public void ComprarPrincipioAtivo()
        {
            bool repetir = true;
            do
            {
                Menu.MenuCompraPrincipioAtivo();
                var opcao = Console.ReadLine() ?? "-1";

                switch (opcao)
                {
                    case "1":
                        RegistrarCompra();
                        break;
                    case "2":
                        AlterarCompra();
                        break;
                    case "3":
                        ListarCompras();
                        break;
                    case "4":
                        ListarCompraPorId();
                        break;
                    case "0":
                        repetir = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Selecione uma opção do menu!");
                        InputHelper.PressioneEnterParaContinuar();
                        break;
                }
            } while (repetir);
        }
        public void ManipularMedicamentos()
        {
            bool repetir = false;
            do
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
                        AtualizarSituacaoMedicamento();
                        break;
                    case 3:
                        LocalizarMedicamento();
                        break;
                    case 4:
                        ListarMedicamentos();
                        break;
                    case 5:
                        ManipularProducao();
                        break;
                    case 0:
                        repetir = false; // Sai do menu de clientes
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Selecione uma opção do menu!");
                        InputHelper.PressioneEnterParaContinuar();
                        break;
                }
            } while (repetir == true);
        }
        public void ManipularClientes()
        {
            bool repetir = true;
            do
            {
                Menu.MenuManipulacaoCliente();
                var opcao = Console.ReadLine() ?? "-1";

                switch (opcao)
                {
                    case "1":
                        AtualizarCliente();
                        break;
                    case "2":
                        AlterarSituacaoCliente();
                        break;
                    case "3":
                        AdicionarClienteRestrito();
                        break;
                    case "4":
                        RemoverClienteRestrito();
                        break;
                    case "5":
                        ListarClientePorCPF();
                        break;
                    case "6":
                        ListarClientes();
                        break;
                    case "7":
                        ListarClientesRestritos();
                        break;
                    case "0":
                        repetir = false; // Sai do menu de clientes
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Selecione uma opção do menu!");
                        InputHelper.PressioneEnterParaContinuar();
                        break;
                }
            }
            while (repetir);
        }
        public void ManipularFornecedores()
        {
            bool repetir = true;
            do
            {
                Menu.MenuManipulacaoFornecedor();
                var opcao = Console.ReadLine() ?? "-1";
                switch (opcao)
                {
                    case "1":
                        AtualizarRazaoSocial();
                        break;
                    case "2":
                        AlterarSituacaoFornecedor();
                        break;
                    case "3":
                        AdicionarFornecedorRestrito();
                        break;
                    case "4":
                        RemoverFornecedorRestrito();
                        break;
                    case "5":
                        ListarFornecedorPorCNPJ();
                        break;
                    case "6":
                        ListarFornecedores();
                        break;
                    case "7":
                        ListarFornecedoresRestritos();
                        break;
                    case "8":
                        ListarFornecedorBloqueadoPorCNPJ();
                        break;
                    case "0":
                        repetir = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Selecione uma opção do menu!");
                        InputHelper.PressioneEnterParaContinuar();
                        break;
                }
            } while (repetir);
        }
        public void ManipularPrincipioAtivo()
        {
            bool repetir = true;
            do
            {
                Menu.MenuManipulacaoPrincipioAtivo();
                var opcao = Console.ReadLine() ?? "-1";
                switch (opcao)
                {
                    case "1":
                        AlterarNomeIngrediente();
                        break;
                    case "2":
                        AlterarSituacao();
                        break;
                    case "3":
                        BuscarIngredientePorId();
                        break;
                    case "4":
                        ListarIngrediente();
                        break;
                    case "0":
                        repetir = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Selecione uma opção do menu!");
                        InputHelper.PressioneEnterParaContinuar();
                        break;
                }
            } while (repetir);
        }
        public void ManipularProducao()
        {
            bool repetir = true;
            do
            {
                Menu.MenuProducao();
                var opcao = Console.ReadLine() ?? "-1";
                switch (opcao)
                {
                    case "1":
                        RegistrarProducao();
                        break;
                    case "2":
                        AlterarQuantidadeDaProducao();
                        break;
                    case "3":
                        AlterarQuantidadeDeItensProduzidos();
                        break;
                    case "4":
                        ListarProducaoPorId();
                        break;
                    case "5":
                        ListarProducoes();
                        break;
                    case "0":
                        repetir = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Selecione uma opção do menu!");
                        InputHelper.PressioneEnterParaContinuar();
                        break;
                }
            } while (repetir);
        }
        public void Relatorios()
        {
            bool repetir = true;
            do
            {
                Menu.MenuRelatorio();
                var opcao = Console.ReadLine() ?? "-1";

                switch (opcao)
                {
                    case "1":
                        RelatorioVendasPeriodo();
                        break;
                    case "2":
                        RelatorioMedicamentosMaisVendidos();
                        break;
                    case "3":
                        RelatorioComprasFornecedor();
                        break;
                    case "0":
                        repetir = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Selecione uma opção do menu!");
                        InputHelper.PressioneEnterParaContinuar();
                        break;
                }
            } while (repetir);
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
                if (!char.IsLetter(caractere) && !char.IsWhiteSpace(caractere))
                {
                    Console.WriteLine($"Caractere inválido: '{caractere}'");
                    return false;
                }
            }
            return true;
        }
        public void RegistrarIngrediente()
        {
            var ultimoIngrediente = this.Ingredientes.LastOrDefault();
            var id = GerarProximoId();
            string nome;
            DateOnly ultimaCompra;
            do
            {
                nome = InputHelper.RetornarString("Digite o nome do ingrediente: ", "Nome do ingrediente inválido, o ingrediente deve ter até 20 caracteres e alfanumericos");
            } while (nome.Length > 20 || nome.Length < 1 || !VerificarNome(nome));


            Ingredientes.Add(new IngredientModel(id, nome));
            ingredientManipulation.Gravar(this.Ingredientes);
            InputHelper.PressioneEnterParaContinuar();
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
                if (ingrediente != null)
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
            ingredientManipulation.Gravar(this.Ingredientes);
        }
        public void AlterarSituacao()
        {
            string id;
            do
            {
                id = InputHelper.RetornarString("Digite o id do ingrediente que deseja alterar a situação: ", "Por favor, digite um id");
                var ingrediente = LocalizarIngrediente(id);
                if (ingrediente != null)
                {
                    string situacao;
                    do
                    {
                        situacao = InputHelper.RetornarString("Digite a nova situação: ", "Por favor, digite a nova situação: A - Ativo | I - Inativo").ToUpper();
                    } while (situacao != "A" && situacao != "I");
                    //situacao = Console.ReadLine();

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
                    ingredientManipulation.Gravar(this.Ingredientes);
                }
                else
                    Console.WriteLine("Ingrediente não existe");
            } while (id == string.Empty);
        }
        private void BuscarIngredientePorId()
        {
            var id = InputHelper.RetornarString("Digite o id do ingrediente que deseja consultar: ", "Por favor, digite um id");
            var ingrediente = LocalizarIngrediente(id);
            if (ingrediente != null)
            {
                Console.WriteLine("Ingrediente localizado:");
                Console.WriteLine(ingrediente);
                InputHelper.PressioneEnterParaContinuar();
            }
            else
            {
                Console.WriteLine("Ingrediente não existe");
                InputHelper.PressioneEnterParaContinuar();
            }
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
            InputHelper.PressioneEnterParaContinuar();
        }
        #endregion

        #region Operações de CRUD da classe Customer
        public void RegistrarCliente()
        {

            Console.WriteLine("CADASTRANDO CLIENTE: ");
            Console.WriteLine();
            string cpf;
            bool validar = false;
            CustomerModel cliente = null;
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
                            cliente = this.Clientes.Find(c => c.CPF == cpf);
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
            } while (cpf.Length != 11 || validar != true || cliente != null);
            Console.Clear();
            string nome;
            string padrao = @"^-?[0-9]+(?:\.[0-9]+)?$";
            Regex regex = new Regex(padrao);
            do
            {
                nome = InputHelper.RetornarString("Digite seu nome: ", "Por favor, digite o nome!");
                if (regex.IsMatch(nome))
                {
                    InputHelper.ExibirErro("Nome não pode conter número!");
                    InputHelper.PressioneEnterParaContinuar();
                    Console.Clear();
                }
            } while (nome.Length > 50 || nome.Length <= 0 || regex.IsMatch(nome) == true);
            Console.Clear();
            DateOnly dataNascimento;
            do
            {
                dataNascimento = InputHelper.RetornarData("Digite sua Data de Nascimento: ", "Por favor, digite a data de nascimento!");
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
            InputHelper.PressioneEnterParaContinuar();
        }
        public void AtualizarCliente()
        {
            string cpf;
            do
            {
                cpf = InputHelper.RetornarString("Digite o CPF do cliente que deseja modificar: ", "Por favor, informe um CPF");
            } while (cpf == string.Empty || cpf == null);

            var cliente = BuscarPorCPF(cpf);
            if (cliente != null)
            {
                Console.WriteLine("ALTERANDO INFORMAÇÕES DO CLIENTE - Caso não queira modificar nenhuma informação, aperte ENTER nas propriedades");
                string nome;
                nome = InputHelper.RetornarString("Digite seu nome: ", "Por favor, digite o nome!");
                Console.Clear();

                string telefone;
                telefone = InputHelper.RetornarString("Digite seu telefone com DDD: ", "Por favor, digite o número de telefon com DDD!");

                if (telefone != string.Empty)
                    cliente.setTelefone(telefone);
                if (nome != string.Empty)
                    cliente.setNome(nome);

                customerManipulate.Gravar(Clientes);
                Console.Clear();

            }
        }
        public CustomerModel BuscarPorCPF(string cpf)
        {
            return this.Clientes.Find(c => c.CPF == cpf);
        }
        public void ListarClientes()
        {
            Console.Clear();
            Console.WriteLine("LISTAR CLIENTES: ");
            Console.WriteLine();
            if (this.Clientes == null)
            {
                Console.WriteLine("Não existe clientes cadastrados");
                InputHelper.PressioneEnterParaContinuar();
            }
            foreach (var cliente in this.Clientes)
            {
                Console.WriteLine(cliente);
            }
            InputHelper.PressioneEnterParaContinuar();
        }
        public void ListarClientePorCPF()
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
        public void AlterarSituacaoCliente()
        {
            string CPF;
            bool validar = false;
            CustomerModel cliente = null;
            do
            {

                Console.Clear();
                CPF = InputHelper.RetornarString("Digite o seu CPF (Sem pontuções): ", "Por favor, digite o CPF!");
                if (CPF.Length != 11)
                {
                    InputHelper.ExibirErro("CPF deve conter 11 caracteres!");
                    InputHelper.PressioneEnterParaContinuar();
                    Console.Clear();
                }
            } while (CPF.Length != 11);

            if (CPF.Length == 11)
            {
                validar = InputHelper.ValidarCpf(CPF);
                if (!validar)
                {
                    InputHelper.ExibirErro("CPF inválido!");
                    InputHelper.PressioneEnterParaContinuar();
                    Console.Clear();
                }
                else
                {
                    cliente = this.Clientes.Find(c => c.CPF == CPF);
                    if (cliente != null)
                    {
                        cliente = BuscarPorCPF(CPF);
                        string situacao;

                        do
                        {
                            situacao = InputHelper.RetornarString("Informe a situação que deseja: ", "Situação inválida, digite novamente: ");

                        } while (situacao != "A" && situacao != "I");
                        if (situacao == "A" && cliente.Situacao == SituationCustomer.A)
                        {
                            Console.WriteLine("Cliente já está ativo!");
                            InputHelper.PressioneEnterParaContinuar();
                            Console.Clear();
                        }
                        else if (situacao == "I" && cliente.Situacao == SituationCustomer.I)
                        {
                            Console.WriteLine("Cliente já está inativo!");
                            InputHelper.PressioneEnterParaContinuar();
                            Console.Clear();
                        }
                        else
                        {
                            cliente.setSituacao(situacao == "A" ? Enums.SituationCustomer.A : Enums.SituationCustomer.I);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Cliente alterado com sucesso!");
                            Console.ResetColor();
                            InputHelper.PressioneEnterParaContinuar();
                            Console.Clear();

                        }
                    }
                }
            }
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
            var medicamentosDisponiveisParaVenda = this.Medicamentos.FindAll(m => m.Situacao == 'A' && Producao.Exists(p => p.Medicamento == m.CDB));
            if (medicamentosDisponiveisParaVenda.Count == 0)
            {
                Console.WriteLine("Não medicamentos disponíveis para venda!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }
            do
            {
                Console.Clear();

                Console.WriteLine("======= MEDICAMENTOS DISPONÍVEIS =======");
                foreach (var m in medicamentosDisponiveisParaVenda)
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

                var medicamento = medicamentosDisponiveisParaVenda.Find(m => m.CDB == cdbDoMedicamento);

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

                        medicamentosDisponiveisParaVenda.Remove(medicamento);

                        this.ItensDeVenda.Add(itemDeVenda);
                    }
                }

                if (medicamentosDisponiveisParaVenda.Count == 0)
                    continue;

                repetir = RealizarPerguntaDeConfirmacao("Deseja continuar vendendo? (1 - SIM|0 - NÃO)");
            }
            while (repetir && listaDeCdbs.Count < 3);

            if (listaDeCdbs.Count == 0)
                Console.WriteLine("Venda cancelada!!");
            else if (valorTotal > 99999999.99m)
                Console.WriteLine("Esta venda passou do valor limite! Portanto a venda será cancelada!");
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
        public void RegistrarFornecedor()
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
        public SupplierModel BuscarPorCNPJ(string cnpj)
        {
            return Fornecedores.Find(c => c.Cnpj == cnpj);
        }
        public void AtualizarRazaoSocial()
        {
            Console.WriteLine("Digite o Cnpj que deseja alterar a razão social:");
            string cnpj = Console.ReadLine();
            var fornecedor = this.BuscarPorCNPJ(cnpj);

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
            var fornecedor = BuscarPorCNPJ(Cnpj);

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
        public void ListarFornecedores()
        {
            Console.Clear();
            foreach (var fornecedor in this.Fornecedores)
            {
                Console.WriteLine(fornecedor);
            }
            InputHelper.PressioneEnterParaContinuar();
        }
        public void ListarFornecedorPorCNPJ()
        {
            string cnpj;
            do
            {
                Console.WriteLine("Digite o Cnpj do fornecedor que deseja buscar:");
                cnpj = Console.ReadLine();
                var fornecedor = this.BuscarPorCNPJ(cnpj);

                if (fornecedor != null)
                {
                    Console.Clear();
                    Console.WriteLine(fornecedor);
                    InputHelper.PressioneEnterParaContinuar();
                }
                else
                {
                    Console.WriteLine("Fornecedor não encontrado");
                    InputHelper.PressioneEnterParaContinuar();
                }
            } while (cnpj.Length != 14 || cnpj is null);
        }
        #endregion

        #region Operação de CDR da classe RestrictedSupplier

        public void AdicionarFornecedorRestrito()
        {
            try
            {
                SupplierModel fornecedor;
                RestrictedSupplierModel fornecedorBloquear;
                string cnpj;
                while (true)
                {
                    do
                    {
                        cnpj = InputHelper.RetornarString("Digite o Cnpj com 14 dígitos (Apenas numeros e sem caracteres especiais) ", "Cnpj inválido, digite novamente:");
                    } while (cnpj.Length != 14);
                    fornecedor = this.Fornecedores.Find(c => c.Cnpj == cnpj);
                    if (fornecedor == null)
                    {
                        Console.WriteLine("Fornecedor não encontrado! Tente novamente: ");
                        continue;
                    }
                    fornecedorBloquear = this.FornecedoresRestritos.Find(f => f.Cnpj == cnpj);

                    if (fornecedorBloquear != null)
                    {
                        Console.WriteLine("Forecedor já está na lista de restrito!");
                    }
                    else
                    {
                        this.FornecedoresRestritos.Add(new RestrictedSupplierModel(cnpj)
                        {
                            Cnpj = fornecedor.Cnpj
                        });
                        Console.WriteLine($"Fornecedor foi adicionado à lista de restritos!");
                    }
                    break;
                }
                InputHelper.PressioneEnterParaContinuar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            restrictedManipulation.Gravar(this.FornecedoresRestritos);
        }
        public void RemoverFornecedorRestrito()
        {
            try
            {
                SupplierModel fornecedor;
                RestrictedSupplierModel fornecedorRestrito;

                do
                {
                    string cnpj = InputHelper.RetornarString("Digite o CNPJ do Fornecedor que deseja remover da lista de bloqueados: ", "Cnpj inválido, digite outro CNPJ para Fornecedor");
                    fornecedor = this.Fornecedores.Find(c => c.Cnpj == cnpj);

                    if (fornecedor == null)
                    {
                        Console.WriteLine("Fornecedor não encontrado!");
                        continue;
                    }

                    fornecedorRestrito = this.FornecedoresRestritos.Find(f => f.Cnpj == cnpj);

                    if (fornecedorRestrito == null)
                    {
                        Console.WriteLine("Fornecedor não está na lista de restritos!");
                    }
                    else
                    {
                        this.FornecedoresRestritos.Remove(fornecedorRestrito);
                        Console.WriteLine("Fornecedor foi removido de restritos com sucesso!");
                    }
                } while (fornecedor is null);
                InputHelper.PressioneEnterParaContinuar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            restrictedManipulation.Gravar(this.FornecedoresRestritos);
        }
        public RestrictedSupplierModel BuscarFornecedorRestrito(string cnpj)
        {
            return this.FornecedoresRestritos.Find(f => f.Cnpj == cnpj);
        }
        public void ListarFornecedoresRestritos()
        {
            Console.WriteLine("LISTA DE FORNECEDORES BLOQUEADOS COM BASE NO CNPJ");
            if(this.FornecedoresRestritos.Count == 0)
            {
                Console.WriteLine("Nenhum Fornecedor bloqueado");
                InputHelper.PressioneEnterParaContinuar();
            } else
            {
                foreach (var fornecedor in this.FornecedoresRestritos)
                {
                    Console.WriteLine(fornecedor.Cnpj);
                }
                InputHelper.PressioneEnterParaContinuar();
            }
        }
        public void ListarFornecedorBloqueadoPorCNPJ()
        {
            string cnpj;
            do
            {
                cnpj = InputHelper.RetornarString("Digite o CNPJ do Fornecedor Bloqueado que deseja buscar :", "CNPJ Inválido");
                var fornecedor = this.BuscarPorCNPJ(cnpj);

                if (fornecedor != null)
                {
                    Console.WriteLine(fornecedor);
                    InputHelper.PressioneEnterParaContinuar();
                }
                else
                {
                    Console.WriteLine("Fornecedor não encontrado");
                    InputHelper.PressioneEnterParaContinuar();
                }
            } while (cnpj.Length != 14 || cnpj is null);
        }
        #endregion

        #region Operação de CDR da classe RestrictedCustomer
        public void AdicionarClienteRestrito()
        {

            RestrictedCustomerModel clienteBloquear;
            CustomerModel cliente = null;
            bool validar = false;
            string CPF;
            do
            {

                Console.Clear();
                CPF = InputHelper.RetornarString("Digite o seu CPF (Sem pontuções): ", "Por favor, digite o CPF!");
                if (CPF.Length != 11)
                {
                    InputHelper.ExibirErro("CPF deve conter 11 caracteres!");
                    InputHelper.PressioneEnterParaContinuar();
                    Console.Clear();
                }
                else if (CPF.Length == 11)
                {
                    validar = InputHelper.ValidarCpf(CPF);
                    if (!validar)
                    {
                        InputHelper.ExibirErro("CPF inválido!");
                        InputHelper.PressioneEnterParaContinuar();
                        Console.Clear();
                    }
                    else
                    {
                        cliente = this.Clientes.Find(c => c.CPF == CPF);
                        if (cliente != null)
                        {
                            cliente = this.Clientes.Find(c => c.CPF == CPF);
                            clienteBloquear = this.ClientesRestritos.Find(c => c.CPF == CPF);
                            if (cliente != null)
                            {
                                if (clienteBloquear is null)
                                {
                                    this.ClientesRestritos.Add(new RestrictedCustomerModel(cliente.CPF));
                                    this.restrictedCustomerManipulate.Gravar(this.ClientesRestritos);
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
                        }
                    }
                }
            } while (CPF.Length != 11);
            Console.Clear();

        }
        public void RemoverClienteRestrito()
        {

            RestrictedCustomerModel clienteLiberar;
            CustomerModel cliente;
            do
            {
                string cpf = InputHelper.RetornarString("Digite o CPF do cliente que deseja retirar da lista de bloqueados: ", "Por favor, digite o CNPJ do Fornecedor");
                cliente = this.Clientes.Find(c => c.CPF == cpf);
                clienteLiberar = this.ClientesRestritos.Find(c => c.CPF == cpf);
                if (cliente is not null)
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
            } while (cliente == null || clienteLiberar == null);
        }
        public RestrictedCustomerModel BuscarClienteRestrito(string cpf)
        {
            return this.ClientesRestritos.Find(c => c.CPF == cpf);
        }
        public void ListarClientesRestritos()
        {
            Console.WriteLine("LISTA DE CLIENTES BLOQUEADOS COM BASE NO CPF");
            if (this.ClientesRestritos == null)
            {
                Console.WriteLine("Não existe clientes cadastrados");
            }
            foreach (var cliente in this.ClientesRestritos)
            {
                Console.WriteLine($"{cliente.CPF}");
            }
            InputHelper.PressioneEnterParaContinuar();
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

                    if (ingredientesAtivos.Count == 0)
                        break;
                }


                repetir = RealizarPerguntaDeConfirmacao("Deseja cadastrar mais um principio ativo? (1 - Sim|0 - Não");
            }
            while (repetir);

            Producao.Add(producao);
            produceItemManipulate.Gravar(ItensProducao);
            produceManipulate.Gravar(Producao);

            //TODO: Salvar no arquivo
        }
        private void AlterarQuantidadeDeItensProduzidos()
        {
            if (!ExisteProducoes())
            {
                Console.WriteLine("Não há produções cadastradas!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }
            ListarProducoes();

            var idProd = InputHelper.RetornarNumeroInteiro("Digite o ID do item manipulado que deseja alterar: ");
            var producao = BuscarProducaoPorId(idProd);

            if (producao == null)
            {
                Console.WriteLine($"Não foi encontrada nenhuma produção com este ID: {idProd}");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }
            var itensProducao = this.ItensProducao.FindAll(i => i.IdProducao == idProd);
            foreach (var item in itensProducao)
            {
                Console.WriteLine(item);
            }

            int iditemProducao = InputHelper.RetornarNumeroInteiro("Digite o ID do item da produção que deseja alterar: ");
            if (this.ItensProducao.Find(i => i.Id == iditemProducao) == null)
            {
                Console.WriteLine("O ID do item da produção não existe");
            }
            var itemProducao = this.ItensProducao.Find(i => i.Id == iditemProducao);
            int novaQuantidade = InputHelper.RetornarNumeroInteiro("Digite a nova quantidade de princípio ativo (máximo 9999): ");

            if (novaQuantidade <= 0 || novaQuantidade > 9999)
            {
                Console.WriteLine("Valor inválido! A quantidade deve estar entre 0 e 9999 gramas.");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }
            itemProducao.SetQuantidade(novaQuantidade);

            Console.WriteLine("Quantidade de princípio ativo atualizada com sucesso!");
            InputHelper.PressioneEnterParaContinuar();

            produceItemManipulate.Gravar(ItensProducao);
        }

        private void AlterarQuantidadeDaProducao()
        {
            if (!ExisteProducoes())
            {
                Console.WriteLine("Não há produções cadastradas!");
                InputHelper.PressioneEnterParaContinuar();
                return;
            }

            ListarProducoes();

            var id = InputHelper.RetornarNumeroInteiro("Digite o ID da produção que deseja alterar a quantidade:");

            var producao = BuscarProducaoPorId(id);

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
            produceManipulate.Gravar(Producao);
        }

        private void ListarProducaoPorId()
        {
            var id = InputHelper.RetornarNumeroInteiro("Digite o ID da produção que deseja consultar:");

            var producao = BuscarProducaoPorId(id);

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

        private ProduceModel BuscarProducaoPorId(int id)
        {
            return Producao.Find(p => p.ID == id);
        }

        private void ListarProducoes()
        {
            if (ExisteProducoes())
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

        private bool ExisteProducoes()
        {
            return this.Producao.Count > 0 ? true : false;
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

                if (Medicamentos.Exists(m => m.CDB == cdb))
                {
                    Console.WriteLine("Esse medicamento ja existe no sistema");
                    InputHelper.PressioneEnterParaContinuar();
                    return;
                }

                validadeCDB = InputHelper.ValidarCDB(cdb);
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
                    Console.Clear();
                }

            } while (nome is null);

            do
            {
                Console.WriteLine("Digite a categoria do medicamento (A , B, I, V):");
                validadeCategoria = char.TryParse(Console.ReadLine().ToUpper(), out categoria);
                if (validadeCategoria == false)
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
        public MedicineModel LocalizarMedicamento()
        {
            Console.WriteLine("Digite o código de barras do medicamento que deseja procurar: ");
            string localizar = Console.ReadLine();

            MedicineModel medicamento = Medicamentos.Find(m => m.CDB == localizar);

            Console.WriteLine(medicamento);

            InputHelper.PressioneEnterParaContinuar();

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
            InputHelper.PressioneEnterParaContinuar();
        }
        public void AtualizarSituacaoMedicamento()
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
            InputHelper.PressioneEnterParaContinuar();
        }
        public void ListarMedicamentos()
        {
            Console.WriteLine("===Lista de medicamentos ===");
            foreach (var medicine in Medicamentos)
            {
                Console.WriteLine(medicine);
            }
            InputHelper.PressioneEnterParaContinuar();
        }
        #endregion

        #region Operações de CRUD da classe Purchase

        public void RegistrarCompra()
        {
            string cnpj;
            PurchaseModel compra = null;
            int id = this.Compra.LastOrDefault()?.Id ?? 0;
            do
            {
                cnpj = InputHelper.RetornarString("Digite o CNPJ do fornecedor:", "O CNPJ é inválido.");
                var fornecedor = this.Fornecedores.Find(f => f.Cnpj == cnpj);
                var fornecedorBloqueado = this.FornecedoresRestritos.Find(fb => fb.Cnpj == cnpj);

                var date = DateOnly.FromDateTime(DateTime.Now);

                int tempoEmpresa = date.Year - fornecedor.DataAbertura.Year;

                if (fornecedor != null && fornecedorBloqueado == null && fornecedor.Situacao != SituationSupplier.I && tempoEmpresa >= 2)
                {
                    Console.WriteLine("Fornecedor disponível para compra");


                    compra = new PurchaseModel(++id, cnpj);

                    RegistrarItemCompra(compra.Id);

                }
                else
                {
                    Console.WriteLine("Não é possível comprar com esse fornecedor");
                }
            }
            while (cnpj.Length != 14 || compra == null);



            var itensCompra = this.ItensDaCompra.FindAll(i => i.IdCompra == compra.Id);

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
            var localizadoCompra = Compra.Find(i => i.Id == localizar);

            if (localizadoCompra != null)
            {
                InputHelper.PressioneEnterParaContinuar();
                return localizadoCompra;
            }
            else
            {
                InputHelper.PressioneEnterParaContinuar();
                return null;
            }

        }
        public void ListarCompraPorId()
        {
            var localizar = InputHelper.RetornarNumeroInteiro("Digite o ID da compra que deseja procurar:");
            var localizadoCompras = Compra.FindAll(i => i.Id == localizar);

            if (localizadoCompras != null)
            {
                Console.WriteLine("Não existe compra para esse ingrediente");
            }
            else
            {
                foreach (var compra in localizadoCompras)
                {
                    Console.WriteLine(compra);
                }
            }
            InputHelper.PressioneEnterParaContinuar();
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
            InputHelper.PressioneEnterParaContinuar();
        }
        public void ListarCompras()
        {
            Console.WriteLine("===Lista de Itens da Compra ===");
            foreach (var compra in Compra)
            {
                Console.WriteLine(compra);
            }
            InputHelper.PressioneEnterParaContinuar();
        }
        #endregion

        #region Operações de CRUD da classe PurchaseItem

        public void RegistrarItemCompra(int idCompra)
        {
            int opcao = 0;
            int contador = 0;
            string idIngrediente = "";
            decimal totalItem = 0;
            var id = this.ItensDaCompra.LastOrDefault()?.Id ?? 0;
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
                } while (quantidade <= 0 || quantidade >= 1000);

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
                PurchaseItemModel itemDaCompra = new PurchaseItemModel(++id, idCompra, idIngrediente, quantidade, valorUnitario, totalItem);

                ItensDaCompra.Add(itemDaCompra);

            } while (opcao != 2 || contador == 3);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Item da compra cadastrado com sucesso");
            purchaseItemManipulate.Gravar(this.ItensDaCompra);
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();

        }

        public PurchaseItemModel BuscarItemCompraPorId()
        {

            var localizar = InputHelper.RetornarNumeroInteiro("Digite o ID da compra que deseja procurar:");
            var localizadoItemCompra = ItensDaCompra.Find(i => i.IdCompra == localizar);


            return localizadoItemCompra;
        }

        public PurchaseItemModel AlterarItemCompra()
        {
            PurchaseItemModel itemCompra = BuscarItemCompraPorId();

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
                    } while (itemCompra.Quantidade <= 0 || itemCompra.Quantidade >= 1000);

                    Console.WriteLine("Quantidade alterada com sucesso.");
                }
                else
                {
                    Console.WriteLine("Não existe esse item da compra.");
                }

            }
            InputHelper.PressioneEnterParaContinuar();
            return itemCompra;
        }
        public void ListarItemCompra()
        {
            Console.WriteLine("===Lista de Itens da Compra ===");
            foreach (var itemCompra in ItensDaCompra)
            {
                Console.WriteLine(itemCompra);
            }
            InputHelper.PressioneEnterParaContinuar();
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

            InputHelper.PressioneEnterParaContinuar();
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

            InputHelper.PressioneEnterParaContinuar();
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
                cpnj = InputHelper.RetornarSomenteNumeros("Digite o CNPJ do Fornecedor que deseja consultar vendas: ");
            } while (cpnj.Length != 14 || !InputHelper.ValidarCnpj(cpnj));

            var relatorio = this.Compra.FindAll(c => c.Fornecedor == cpnj).ToList();

            if (relatorio.Count == 0)
            {
                Console.WriteLine("Não há compras desse fornecedor!");
            }
            else
            {
                if(relatorio is null)
                {
                    Console.WriteLine("Não existe compras para fornecedor!");
                }
                foreach (var item in relatorio)
                {
                    Console.WriteLine(item);
                }
            }
            InputHelper.PressioneEnterParaContinuar();
        }
        #endregion
    }
}
