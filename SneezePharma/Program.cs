using SneezePharma.Exceptions;
using SneezePharma.Models;
using SneezePharma.Models.SalesItem;
using SneezePharma.Utils;

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
                sneezePharma.CadastrarItemDeVenda();
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
}

MenuSalesItem();

void MenuSales()
{
    var sneezePharma = new Pharma();
    var repetir = true;
    do
    {
        Console.Clear();

        Console.WriteLine("1 - Realizar venda de medicamento");
        Console.WriteLine("2 - Listar vendas");
        Console.WriteLine("3 - Buscar uma venda por id e mostrar itens da venda");
        Console.WriteLine("0 - Retornar para o próximo menu");
        var opcao = Console.ReadLine() ?? "-1";

        switch (opcao)
        {
            case "1":
                break;
            case "2":
                break;
            case "3":
                break;
            case "0":
                repetir = false;
                break;
            default:

                break;
        }
    }
    while (repetir);
}