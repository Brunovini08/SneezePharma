using SneezePharma.Helpers;
using SneezePharma.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        Pharma pharma = new Pharma();
        int opcao;
        bool validar;
        do
        {
            Menu.MenuPrincipal();
            validar = int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
                case 1:
                    pharma.Cadastrar();
                    break;
                case 2:
                    pharma.VendasMedicamento();
                    break;
                case 3:
                    pharma.ComprarPrincipioAtivo();
                    break;
                case 4:
                    pharma.ManipularMedicamentos();
                    break;
                case 5:
                    pharma.ManipularClientes();
                    break;
                case 6:
                    pharma.ManipularFornecedores();
                    break;
                case 7:
                    pharma.ManipularPrincipioAtivo();
                    break;
                case 8:
                    pharma.Relatorios();
                    break;
                case 0:
                    Console.WriteLine("Saindo do sistema...");
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        } while (true);

    }
}