using SneezePharma;
using SneezePharma.Models;

List <Produce> listamanipulacoes = new List<Produce> ();
int opcao;












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















