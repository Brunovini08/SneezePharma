using SneezePharma;
using SneezePharma.Models;




List<Produce> listaManipulacoes = new List<Produce>();
bool continuar = true;





while (continuar)
{
    Console.WriteLine("\n1 - Cadastrar Manipulação");
    Console.WriteLine("2 - Listar Manipulações");
    Console.WriteLine("3 - Alterar Pedido de Manipulação");
    Console.WriteLine("4 - Buscar manipulação pelo ID");
    Console.WriteLine("5 - Cadastrar Item Manipulado");
    Console.WriteLine("6 - Listar Itens manipulados");
    Console.WriteLine("7 - Alterar registro de item manipulado");
    Console.WriteLine("8 - Buscar item pelo ID");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");
    int opcao = int.Parse(Console.ReadLine());

    switch (opcao)
    {
        case 1:
            var manipulacao = new Produce().CadastrarManipulacao();
            if (manipulacao is not null)
            {
                listaManipulacoes.Add(manipulacao);
                Console.WriteLine("\nManipulação salva com sucesso!");
            }
            break;

        case 2:
            Produce.MostrarManipulacao(listaManipulacoes);
            break;

        case 3:
            Produce.AlterarManipulacao(listaManipulacoes);
            break;
        case 4:
            Produce.LocalizarManipulacao(listaManipulacoes);
            break;
        case 5:
            break;
        case 6:
            break;
        case 7:
            break;
        case 8:
            break;
        case 0:
            continuar = false;
            break;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}














