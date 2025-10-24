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
            Console.WriteLine("Entrada inv�lida! Digite somente n�meros!");
    }
    while (!repetir);


int RetornarNumeroInteiro(string msgDeInput)
{
    var repetir = true;
    var numero = 0;

    do
    {
        Console.WriteLine(msgDeInput);
        repetir = int.TryParse(Console.ReadLine(), out numero);

        if (!repetir)
            Console.WriteLine("Entrada inv�lida! Tente novamente!");
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
    Console.WriteLine("\n 1 - Cadastrar Manipula��o");
    Console.WriteLine("2 - Listar Manipula��es");
    Console.WriteLine("4 - Sair");
    Console.WriteLine("\n Escolha uma op��o: ");
    opcao = int.Parse(Console.ReadLine());

    switch (opcao) {
        case 1:
            var produce  = new Produce().CadastrarManipulacao();
            if (produce != null)
            {
                listamanipulacoes.Add(produce);
            }
            break;
        case 2:
            
            break;



} while (opcao != 5);


void AlterarQuantidadeItensVenda(SalesItem itemDeVenda)
{
    var quantidade = 0;
    try
    {
        quantidade = RetornarNumeroInteiro("Digite a quantidade de medicamentos:");
        GeneralException.VerificarQuantidadeInvalidaInteiro(1, 999, quantidade, "Quantidade inv�lida! A quantidade deve estar entre 0 e 999");

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
                Console.WriteLine("Altera��es realizadas com sucesso!");
                repetir = false;
                break;
            default:
                Console.WriteLine("Op��o inv�lida! Tente novamente!");
                break;
        }
    }
    while (repetir);
}

