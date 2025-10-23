using SneezePharma;
using SneezePharma.Exceptions;
using SneezePharma.ProduceItem;

List <Produce> listamanipulacoes = new List<Produce> ();
int opcao;












do
{
    Console.WriteLine("\n 1 - Cadastrar Manipulação");
    Console.WriteLine("2 - Listar Manipulações");
    Console.WriteLine("4 - Sair");
    Console.WriteLine("\n Escolha uma opção: ");
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















