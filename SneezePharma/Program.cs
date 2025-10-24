using SneezePharma.Exceptions;
using SneezePharma.Models;
using SneezePharma.Utils;

string RetornarSomenteNumeros(string msgDeInput)
{
    var repetir = true;
    var stringDeRetorno = "";
    do
    {
        Console.WriteLine(msgDeInput);
        stringDeRetorno = Console.ReadLine();

        repetir = long.TryParse(stringDeRetorno, out long x);

        if (!repetir)
            Console.WriteLine("Entrada inválida! Digite somente números!");
    }
    while (!repetir);

    return stringDeRetorno;
}

int RetornarNumeroInteiro(string msgDeInput)
{
    var repetir = true;
    var numero = 0;

    do
    {
        Console.WriteLine(msgDeInput);
        repetir = int.TryParse(Console.ReadLine(), out numero);

        if (!repetir)
            Console.WriteLine("Entrada inválida! Tente novamente!");
    }
    while (!repetir);
    return numero;
}

decimal RetornarNumeroDecimal(string msgDeInput)
{
    var repetir = true;
    var numero = 0.0m;

    do
    {
        Console.WriteLine(msgDeInput);
        repetir = decimal.TryParse(Console.ReadLine(), out numero);

        if (!repetir)
            Console.WriteLine("Entrada inválida! Tente novamente!");
    }
    while (!repetir);
    return numero;
}

SalesItem CadastrarItemDeVenda(int id)
{
    try
    {
        int idVenda = RetornarNumeroInteiro("Digite o número do Id da Venda:");

        string cbcMedicamento = RetornarSomenteNumeros("Digite o CBC do Medicamento:");
        GeneralException.VerificarTamanhoDiferente(cbcMedicamento, 13, "O CBC precisa possuir 13 digitos");

        int quantidade = RetornarNumeroInteiro("Digite a quantidade de medicamentos:");
        GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade inválida! A quantidade deve estar entre 0 e 999");

        //TODO: Puxar o valor unitário direto do Medicamento (Medicamento)
        decimal valorUnitario = RetornarNumeroDecimal("Digite o valor unitário do medicamento");

        decimal totalItem = quantidade * valorUnitario;
        GeneralException.VerificarQuantidadeMaximaDecimal(totalItem, 99999.99m, "Valor total ultrapassou limite máximo (R$ 99999,99)");

        return new SalesItem(++id, idVenda, cbcMedicamento, quantidade, valorUnitario, totalItem);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
        return null;
    }
}

bool ExisteItensDeCompra(List<SalesItem> salesItems)
{
    if (salesItems.Count > 0)
        return true;

    return false;
}

void ListarItensDeVenda(List<SalesItem> salesItems)
{
    if (salesItems.Count > 0)
    {
        Console.WriteLine("===== Itens de Vendas =====");
        foreach (var si in salesItems)
        {
            Console.WriteLine(si);
            Console.WriteLine();
        }
    }
    else
    {
        Console.WriteLine("Não há itens de venda cadastrados!");
    }

    InputHelper.PressioneEnterParaContinuar();
}

SalesItem ProcurarItemDeVendaPorId(int id, List<SalesItem> salesItems)
{
    return salesItems.FirstOrDefault(sl => sl.Id == id);
}

void AlterarQuantidadeItensVenda(SalesItem itemDeVenda)
{
    var quantidade = 0;
    try
    {
        quantidade = RetornarNumeroInteiro("Digite a quantidade de medicamentos:");
        GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade inválida! A quantidade deve estar entre 0 e 999");

        itemDeVenda.SetQuantidade(quantidade);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void AlterarPropriedadesDoItemDeVendas(SalesItem salesItem)
{
    var repetir = true;
    do
    {
        Console.Clear();
        Console.WriteLine(salesItem);
        Console.WriteLine();

        Console.WriteLine("1 - Alterar Id da venda");
        Console.WriteLine("2 - Alterar CBC de Medicamento");
        Console.WriteLine("3 - Quantidade de itens");
        Console.WriteLine("0 - Sair e salvar");
        var opcao = Console.ReadLine();

        switch (opcao)
        {
            case "3":
                AlterarQuantidadeItensVenda(salesItem);
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

void AtualizarItemDaVenda(List<SalesItem> salesItems)
{
    if (ExisteItensDeCompra(salesItems))
    {
        ListarItensDeVenda(salesItems);
        var id = RetornarNumeroInteiro("Digite o Id do item de venda que deseja atualizar:");

        var salesItem = ProcurarItemDeVendaPorId(id, salesItems);

        if (salesItem != null)
        {
            AlterarPropriedadesDoItemDeVendas(salesItem);
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

void MenuSalesItem()
{
    SalesItemManipulate salesItemManipulate = new SalesItemManipulate();
    var salesItemsList = salesItemManipulate.LerItensDeVenda();
    var repetir = true;
    do
    {
        Console.Clear();

        Console.WriteLine("1 - Cadastrar Item de Venda");
        Console.WriteLine("2 - Listar Itens de Venda");
        Console.WriteLine("3 - Alterar um Item de Venda");
        Console.WriteLine("0 - Retornar para o próximo menu");
        var opcao = Console.ReadLine() ?? "-1";

        switch (opcao)
        {
            case "1":
                var idDoItemDeVenda = salesItemsList.LastOrDefault()?.Id ?? 0;
                var itemDaVenda = CadastrarItemDeVenda(idDoItemDeVenda);
                if (itemDaVenda is not null)
                {
                    Console.WriteLine("Item de venda cadastrado com sucesso!");
                    salesItemsList.Add(itemDaVenda);
                }
                InputHelper.PressioneEnterParaContinuar();
                break;
            case "2":
                ListarItensDeVenda(salesItemsList);
                break;
            case "3":
                AtualizarItemDaVenda(salesItemsList);
                break;
            case "0":
                repetir = false;
                break;
            default:

                break;
        }
    }
    while (repetir);

    salesItemManipulate.GravarItensDeVenda(salesItemsList);
}

MenuSalesItem();
