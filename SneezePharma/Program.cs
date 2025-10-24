using SneezePharma;
using SneezePharma.Models;



var manipulador = new FileProduce();
List<Produce> listaManipulacoes = new List<Produce>();
bool continuar = true;





while (continuar)
{
    Console.WriteLine("\n1 - Cadastrar Manipula��o");
    Console.WriteLine("2 - Listar Manipula��es");
    Console.WriteLine("3 - Alterar Pedido de Manipula��o");
    Console.WriteLine("4 - Buscar pelo ID");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma op��o: ");
    int opcao = int.Parse(Console.ReadLine());

    switch (opcao)
    {
        case 1:
            var manipulacao = new Produce().CadastrarManipulacao();
            if (manipulacao is not null)
            {
                listaManipulacoes.Add(manipulacao);
                Console.WriteLine("\nManipula��o salva com sucesso!");
            }
            break;

        case 2:
            //var produtos = manipulador.LerManipulacao();
            manipulador.GravarManipulacao(listaManipulacoes);
            Produce.MostrarManipulacao(listaManipulacoes);
            break;

        case 3:

            Produce.AlterarManipulacao(listaManipulacoes);
            break;
        case 4:
            Produce.LocalizarManipulacao(listaManipulacoes);
            break;
        case 0:
            manipulador.GravarManipulacao(listaManipulacoes);
            continuar = false;
            break;

        default:
            Console.WriteLine("Op��o inv�lida.");
            break;
    }
}