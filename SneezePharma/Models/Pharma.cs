using SneezePharma.Enums;
using SneezePharma.Exceptions;
using SneezePharma.Helpers;
using SneezePharma.Models;
using SneezePharma.Models.Produce;
using SneezePharma.Models.Produce.Produce;
using SneezePharma.Models.Sales;
using SneezePharma.Models.SalesItem;
using SneezePharma.Utils;
using System.Text.RegularExpressions;

namespace SneezePharma.Models
{
    public class Pharma
    {
        public List<Customer> Clientes { get; private set; }
        public List<Supplier> Fornecedores { get; private set; }
        public List<MedicineModel> Medicamentos { get; private set; }
        public List<ProduceModel> Producao { get; set; }
        public List<ProduceItemModel> ItemProducao { get; private set; }
        public List<RestrictedCustomer> ClientesRestritos { get; private set; }
        public List<RestrictedSupplier> FornecedoresRestritos { get; private set; }
        public List<SalesModel> Venda { get; private set; }
        public List<SalesItemModel> ItensDeVenda { get; private set; }
        public List<IngredientModel> Ingredientes { get; private set; }
        public List<PurchasesModel> Compra {  get; private set; }
        public List<PurchaseItemModel> ItemDaCompra { get; private set; }

        private SalesManipulate SalesManipulate = new SalesManipulate();
        private SalesItemManipulate SalesItemManipulate = new SalesItemManipulate();

        /// <summary>
        /// 
        /// </summary>
        public Pharma()
        {
            this.Clientes = new List<Customer>();
            this.ClientesRestritos = new List<RestrictedCustomer>();
            this.Fornecedores = new List<Supplier>();
            this.FornecedoresRestritos = new List<RestrictedSupplier>();
            this.Ingredientes= new List<IngredientModel>();
            this.Medicamentos = new List<MedicineModel>();
            this.ItemDaCompra = new List<PurchaseItemModel>();
            this.Compra = new List<PurchasesModel>();
            this.Fornecedores = supplierManipulate.Ler();
            this.Ingredientes = ingredientManipulation.Ler();
            this.FornecedoresRestritos = restrictedManipulation.Ler();
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
                        IncluirIngrediente();
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
            int opcao;
            bool validar;
            Menu.MenuManipulacaoMedicamentos();
            validar = int.TryParse(Console.ReadLine(), out opcao);
            switch (opcao)
            {
                case 1:
                    AlterarMedicamento();
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

        #region Operações de CRUD de Ingredient

        string GerarProximoId(List<IngredientModel> ingrediente)
        {
            //var ingredientes = ingrediente.OrderBy(p => p.Id).ToList();
            //if (ingredientes.Count == 0)
            //    return 1;
            //return ingredientes.Last().Id + 1;
            var id = ingrediente.LastOrDefault()?.Id ?? "";
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
        public void IncluirIngrediente()
        {
            string novoId = GerarProximoId(this.Ingredientes);
            string nome;
            DateOnly ultimaCompra;
            do
            {
                nome = InputHelper.RetornarString("Digite o nome do ingrediente: ", "Nome do ingrediente inválido, o ingrediente deve ter até 20 caracteres e alfanumericos");
            } while (nome.Length > 20 || !VerificarNome(nome));

            do
            {
                try
                {
                    ultimaCompra = DateOnly.Parse(InputHelper.RetornarData("Digite a data de abertura (no modelo: DDMMAAAA):: ", "Data inválida, digite a data de abertura novamente: "));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            } while (ultimaCompra == null || ultimaCompra.ToString() == String.Empty);
            Console.Clear();

            Ingredientes.Add(new IngredientModel(novoId, nome, ultimaCompra));
        }
        public IngredientModel LocalizarIngrediente(string Id)
        {
            return Ingredientes.Find(i => i.Id == Id);
        }
       
        public void AlterarDataUltimaCompra()
        {
            Console.WriteLine("Digite o Id da ultima compra feita: ");
            var Id = Console.ReadLine();
            IngredientModel ingrediente = LocalizarIngrediente(Id);

            if (Id != null)
            {
                DateOnly novaDataUltimaCompra;
                do
                {
                    novaDataUltimaCompra = DateOnly.Parse(InputHelper.RetornarData("Digite a nova data da ultima compra no formato DDMMAAAA:", "Data da ultimo compra inválida"));
                } while (novaDataUltimaCompra.ToString() == string.Empty);

                ingrediente.setUltimaCompra(novaDataUltimaCompra);
                Console.WriteLine("Data da ultima compra alterada com sucesso!");
            }
            else
            {
                Console.WriteLine("Id da ultima compra nao encontrado");
            }
        }
        public void AlterarSituacao(List<Supplier> suppliers, char Situacao)
        {

            Console.WriteLine("Digite o Id que deseja alterar a Situacao: ");
            var Id = Console.ReadLine();
            LocalizarIngrediente(Id);

            if (Id != null)
            {
                Console.WriteLine("Digite a nova situação: ");
                Situacao = char.Parse(Console.ReadLine());
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

        #region "Operações de CRUD da classe Sales e SalesItem"
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
        private void BuscarVendaPorId()
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
        private void ListarVendas()
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
                    try
                    {
                        dataCadastro = DateOnly.Parse(InputHelper.RetornarData("Digite a data do cadastro (no modelo: DDMMAAAA):", "Data do cadastro inválida"));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                } while (dataCadastro == null || dataCadastro.ToString() == String.Empty);
                Console.Clear();

                this.Fornecedores.Add(new Supplier(cnpj, razaoSocial, pais, dataAbertura));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Fornecedor cadastrado com sucesso!");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
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

        //    List<ProduceModel> listaManipulacoes = new List<ProduceModel>();
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
        //                var manipulacao = new ProduceModel().CadastrarManipulacao();
        //                if (manipulacao is not null)
        //                {
        //                    listaManipulacoes.Add(manipulacao);
        //                    Console.WriteLine("\nManipulação salva com sucesso!");
        //                }
        //                break;

        //            case 2:
        //                //var produtos = manipulador.LerManipulacao();
        //                ProduceModel.MostrarManipulacao(listaManipulacoes);
        //                break;

        //            case 3:

        //                ProduceModel.AlterarManipulacao(listaManipulacoes);
        //                break;
        //            case 4:
        //                ProduceModel.LocalizarManipulacao(listaManipulacoes);
        //                break;
        //            case 0:
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
        //        Console.WriteLine($"ID:ID:{ID.ToString().PadLeft(5, '0')}, Data: {data:dd/MM/yyyy}, Quantidade: {quantidade}");
        //        ProduceModel produce = new ProduceModel(ID, data, quantidade);
        //        this.ItemProducao.Add(produce);


        //    }
        //    catch (ArgumentException ex)
        //    {

        //        Console.WriteLine(ex.Message);


        //    }

        //}

        //public static void MostrarManipulacao(List<ProduceModel> listaManipulacoes)
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
        //public ProduceModel LocalizarIDmanipulacao(string id)
        //{

        //    return listaManipulacoes.Find(m => m.ID == id);

        //}

        //public static void AlterarManipulacao(List<ProduceModel> lista)
        //{
        //    Console.WriteLine("Informe o ID que quer alterar: ");
        //    int id = Console.ReadLine() ?? "";
        //    ProduceModel alterar = lista.Find(m => m.ID == id);

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

        //public static void LocalizarManipulacao(List<ProduceModel> lista)
        //{
        //    Console.WriteLine("Digite o ID que gostaria de localizar: ");
        //    string id = Console.ReadLine() ?? "";

        //    ProduceModel encontrado = lista.Find(m => m.ID == id);
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
        //        using SneezePharma;
        //using SneezePharma.Models;
        //using SneezePharma.Models.Produce;




        //List<ProduceModel> listaManipulacoes = new List<ProduceModel>();
        //    bool continuar = true;

        //    FileProduce manipuladorArquivo = new FileProduce();

        //    listaManipulacoes = manipuladorArquivo.Ler();

        //while (continuar)
        //{
        //    Console.WriteLine("\n1 - Cadastrar Manipulação");
        //    Console.WriteLine("2 - Listar Manipulações");
        //    Console.WriteLine("3 - Alterar Pedido de Manipulação");
        //    Console.WriteLine("4 - Buscar pelo ID");
        //    Console.WriteLine("0 - Sair");
        //    Console.Write("Escolha uma opção: ");
        //    int opcao = int.Parse(Console.ReadLine());

        //    switch (opcao)
        //    {
        //        case 1:
        //            var manipulacao = new ProduceModel().CadastrarManipulacao();
        //            if (manipulacao is not null)
        //            {
        //                listaManipulacoes.Add(manipulacao);
        //                Console.WriteLine("\nManipulação salva com sucesso!");
        //            }
        //break;

        //        case 2:
        //    ProduceModel.MostrarManipulacao(listaManipulacoes);
        //    break;

        //case 3:

        //    ProduceModel.AlterarManipulacao(listaManipulacoes);
        //    break;
        //case 4:
        //    ProduceModel.LocalizarManipulacao(listaManipulacoes);
        //    break;
        //case 0:
        //    continuar = false;
        //    manipuladorArquivo.Gravar(listaManipulacoes);
        //    break;

        //default:
        //    Console.WriteLine("Opção inválida.");
        //    break;
        //}
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
        public void RegistrarMedicamento()
        {
            bool validadeCDB = false, validadeNome = false, validadeCategoria = false, validadeSituacao = false;
            string cdb, nome = "";
            char categoria = ' ', situacao = ' ';

            do
            {
                Console.WriteLine("Digite o código de barras do medicamento:");
                cdb = Console.ReadLine();

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
                }
            } while (nome is null);

            do
            {
                Console.WriteLine("Digite a categoria do medicamento (A , B, I, V):");
                validadeCategoria = char.TryParse(Console.ReadLine().ToUpper(), out categoria);

            } while (categoria != 'A' && categoria != 'B' && categoria != 'I' && categoria != 'V');

            decimal valorVenda = 0;
            do
            {
                valorVenda = InputHelper.RetornarNumeroDecimal("Digite o valor de Venda:", "O valor deve ser maior que R$0,00 e menor que R$10000,00.");
            } while (valorVenda <= 0 || valorVenda >= 10000);

            MedicineModel medicamento = new MedicineModel(cdb, nome, categoria, valorVenda);

            this.Medicamentos.Add(medicamento);

        }

        public Medicine LocalizarMedicamento(string id)
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

        private Medicine LocalizarMedicamentoAtivo(string cdb)
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

        public void CriarPurchase()
        {
            string cnpj;
            PurchasesModel compra = null;
            do
            {
                cnpj = InputHelper.RetornarString("Digite o CNPJ do fornecedor:", "O CNPJ é inválido.");
                var fornecedor = this.Fornecedores.Find(f => f.Cnpj == cnpj);
                var fornecedorBloqueado = this.FornecedoresRestritos.Find(fb => fb.Cnpj == cnpj);
                if (fornecedor != null && fornecedorBloqueado == null && fornecedor.Situacao != SituationSupplier.I)
                {
                    compra = new PurchasesModel(fornecedor.Cnpj);
                }
            }
            while (cnpj.Length != 14 || compra == null);

            var itensCompra = this.ItemDaCompra.FindAll(i => i.IdCompra == compra.IdCompra);

            decimal valorTotal = 0;

            foreach(var itens in itensCompra)
            {
                valorTotal += itens.TotalItem;
            }
            
            compra.setValorTotal(valorTotal);

            Compra.Add(compra);
            
        }
        public PurchasesModel LocalizarCompra()
        {
            var localizar = InputHelper.RetornarNumeroInteiro("Digite o ID da compra que deseja procurar:");
            var localizadoCompra = Compra.Find(i => i.IdCompra == localizar);

            return localizadoCompra;
        }

        public PurchasesModel AlterarCompra()
        {
            PurchasesModel compra = LocalizarCompra();

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
            return compra;
        }

        public void ListarCompra()
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
            var id = this.ItemDaCompra.LastOrDefault()?.IdCompra ?? 0;
            do
            {
                IngredientModel localizar ;
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

                ItemDaCompra.Add(itemDaCompra);

            } while (opcao != 2 || contador == 3);


        }

        public PurchaseItemModel LocalizarItemCompra()
        {

            var localizar = InputHelper.RetornarNumeroInteiro("Digite o ID da compra que deseja procurar:");
            var localizadoItemCompra = ItemDaCompra.Find(i => i.IdCompra == localizar);

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
            foreach (var itemCompra in ItemDaCompra)
            {
                Console.WriteLine(itemCompra);
            }
        }

        #endregion
    }
}
//ahhhh