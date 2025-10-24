using SneezePharma.Exceptions;
using SneezePharma.Models;
using SneezePharma.Models.SalesItem;
using SneezePharma.Utils;

//int ConfirmacaoQuantidade()
//{
//    var quantidade = 0;
//    var repetir = true;

//    do
//    {
//        try
//        {
//            quantidade = InputHelper.RetornarNumeroInteiro("Digite a quantidade de medicamentos:");
//            GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade inválida! A quantidade deve estar entre 0 e 999");

//            repetir = false;
//        }
//        catch (ArgumentException ex)
//        {
//            Console.WriteLine(ex.Message);
//        }
//    }
//    while (repetir);

//    return quantidade;
//}

//SalesItemModel CadastrarItemDeVenda(int id)
//{
//    try
//    {
//        int idVenda = InputHelper.RetornarNumeroInteiro("Digite o número do Id da Venda:");

//        string cbcMedicamento = InputHelper.RetornarSomenteNumeros("Digite o CBC do Medicamento:");
//        GeneralException.VerificarTamanhoDiferente(cbcMedicamento, 13, "O CBC precisa possuir 13 digitos");

//        var quantidade = ConfirmacaoQuantidade();

//        //TODO: Puxar o valor unitário direto do Medicamento (Medicamento)
//        decimal valorUnitario = InputHelper.RetornarNumeroDecimal("Digite o valor unitário do medicamento");

//        decimal totalItem = quantidade * valorUnitario;
//        GeneralException.VerificarQuantidadeMaximaDecimal(totalItem, 99999.99m, "Valor total ultrapassou limite máximo (R$ 99999,99)");

//        return new SalesItemModel(++id, idVenda, cbcMedicamento, quantidade, valorUnitario, totalItem);
//    }
//    catch (ArgumentException ex)
//    {
//        Console.WriteLine(ex.Message);
//        return null;
//    }
//}

//bool ExisteItensDeCompra(List<SalesItemModel> salesItems)
//{
//    if (salesItems.Count > 0)
//        return true;

//    return false;
//}

//void ListarItensDeVenda(List<SalesItemModel> salesItems)
//{
//    if (salesItems.Count > 0)
//    {
//        Console.WriteLine("===== Itens de Vendas =====");
//        foreach (var si in salesItems)
//        {
//            Console.WriteLine(si);
//            Console.WriteLine();
//        }
//    }
//    else
//    {
//        Console.WriteLine("Não há itens de venda cadastrados!");
//    }

//    InputHelper.PressioneEnterParaContinuar();
//}

//SalesItemModel ProcurarItemDeVendaPorId(int id, List<SalesItemModel> salesItems)
//{
//    return salesItems.FirstOrDefault(sl => sl.Id == id);
//}

//void AlterarQuantidadeItensVenda(SalesItemModel itemDeVenda)
//{
//    var quantidade = 0;
//    try
//    {
//        quantidade = InputHelper.RetornarNumeroInteiro("Digite a quantidade de medicamentos:");
//        GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade inválida! A quantidade deve estar entre 0 e 999");

//        var totalItem = itemDeVenda.ValorUnitario * quantidade;

//        GeneralException.VerificarQuantidadeMaximaDecimal(totalItem, 99999.99m, "Valor total ultrapassou limite máximo (R$ 99999,99)");

//        itemDeVenda.SetQuantidade(quantidade);
//        itemDeVenda.SetTotalItem(totalItem);
//    }
//    catch (ArgumentException ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}

//void AlterarPropriedadesDoItemDeVendas(SalesItemModel salesItem)
//{
//    var repetir = true;
//    do
//    {
//        Console.Clear();
//        Console.WriteLine(salesItem);
//        Console.WriteLine();

//        Console.WriteLine("1 - Alterar Id da venda");
//        Console.WriteLine("2 - Alterar CBC de Medicamento");
//        Console.WriteLine("3 - Quantidade de itens");
//        Console.WriteLine("0 - Sair e salvar");
//        var opcao = Console.ReadLine();

//        switch (opcao)
//        {
//            case "3":
//                AlterarQuantidadeItensVenda(salesItem);
//                InputHelper.PressioneEnterParaContinuar();
//                break;
//            case "0":
//                Console.WriteLine("Alterações realizadas com sucesso!");
//                repetir = false;
//                break;
//            default:
//                Console.WriteLine("Opção inválida! Tente novamente!");
//                break;
//        }
//    }
//    while (repetir);
//}

//void AtualizarItemDaVenda(List<SalesItemModel> salesItems)
//{
//    if (ExisteItensDeCompra(salesItems))
//    {
//        ListarItensDeVenda(salesItems);
//        var id = InputHelper.RetornarNumeroInteiro("Digite o Id do item de venda que deseja atualizar:");

//        var salesItem = ProcurarItemDeVendaPorId(id, salesItems);

//        if (salesItem != null)
//        {
//            AlterarPropriedadesDoItemDeVendas(salesItem);
//        }
//        else
//        {
//            Console.WriteLine("Item de venda não foi localizado!");
//            InputHelper.PressioneEnterParaContinuar();
//            return;
//        }
//    }
//    else
//    {
//        Console.WriteLine("Não há itens de venda cadastrados!");
//    }
//    InputHelper.PressioneEnterParaContinuar();
//}

void MenuSalesItem()
{
    var sneezePharma = new Pharma();
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
                var idDoItemDeVenda = sneezePharma.ItensDeVenda.LastOrDefault()?.Id ?? 0;
                sneezePharma.CadastrarItemDeVenda(idDoItemDeVenda);
                InputHelper.PressioneEnterParaContinuar();
                break;
            case "2":
                sneezePharma.ListarItensDeVenda();
                break;
            case "3":
                sneezePharma.AtualizarItemDaVenda();
                break;
            case "0":
                repetir = false;
                break;
            default:

                break;
        }
    }
    while (repetir);

    SalesItemManipulate.GravarItensDeVenda(sneezePharma.ItensDeVenda);
}

MenuSalesItem();