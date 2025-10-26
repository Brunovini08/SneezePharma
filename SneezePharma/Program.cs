using SneezePharma.Helpers;
using SneezePharma.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        Pharma pharma = new Pharma();



        //do
        //{
        //    pharma.Cadastrar();

        //    Menu.MenuManipulacaoMedicamentos();
        //    int opcao = int.Parse(Console.ReadLine());

        //    switch(opcao)
        //    {
        //        case 1:
        //            pharma.AlterarMedicamento();
        //            break;
        //        case 2:
        //            pharma.AlterarSituacaoMedicamento();
        //            break;
        //        case 3:
        //            pharma.LocalizarMedicamento();
        //            break;
        //        case 4:
        //            pharma.ListarMedicamentos();
        //            break;
        //        case 0:
        //            break;
        //        default:
        //            break;
        //    }
        //} while (true);

        do
        {
            pharma.Cadastrar();

            Menu.MenuCompraPrincipioAtivo();
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    pharma.CriarItemCompra();
                    break;
                case 2:
                    pharma.ListarItemCompra();
                    break;
                case 3:
                    pharma.LocalizarItemCompra();
                    break;
                case 0:
                    break;
                default:
                    break;
            }
        } while (true);
    }
}