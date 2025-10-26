using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Helpers
{
    public static class Menu
    {
        public static void MenuPrincipal()
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("1. MENU - CADASTROS");
            Console.WriteLine("2. MENU - VENDAS MEDICAMENTOS");
            Console.WriteLine("3. MENU - COMPRAS PRINCÍPIOS ATIVOS");
            Console.WriteLine("4. MENU - MANIPULAÇÃO DE MEDICAMENTOS");
            Console.WriteLine("5. MENU - MANIPULAÇÃO DE CLIENTES");
            Console.WriteLine("6. MENU - MANIPULAÇÃO DE FORNECEDORES");
            Console.WriteLine("7. MENU - MANIPULAÇÃO DE PRINCÍPIO ATIVOS");
            Console.WriteLine("8. MENU - RELATÓRIOS");
            Console.WriteLine("0. SAIR");
            Console.WriteLine("-------------------------------------------");
        }

        public static void MenuCadastros()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("MENU - CADASTROS");
            Console.WriteLine("1. MENU - CADASTRO CLIENTE");
            Console.WriteLine("2. MENU - CADASTRO FORNECEDOR");
            Console.WriteLine("3. MENU - CADASTRO MEDICAMENTO");
            Console.WriteLine("4. MENU - CADASTRO PRINCÍPIOS ATIVOS");
            Console.WriteLine("0. SAIR");
            Console.WriteLine("---------------------------------------");
        }

        public static void MenuVendasMedicamentos()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("MENU - VENDAS MEDICAMENTO");
            Console.WriteLine("1. MENU - VENDER MEDICAMENTO");
            Console.WriteLine("2. MENU - ALTERAR ITENS DA VENDA DE MEDICAMENTOS");
            Console.WriteLine("3. MENU - LISTAR VENDA POR ID");
            Console.WriteLine("4. MENU - LISTAR TODAS AS VENDAS");
            Console.WriteLine("0. SAIR");
            Console.WriteLine("--------------------------------------");
        }

        public static void MenuManipulacaoMedicamentos()
        { 
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("MENU - MANIPULAÇÃO DE MEDICAMENTOS");
            Console.WriteLine("1. MENU - ALTERAR MEDICAMENTO");
            Console.WriteLine("2. MENU - INATIVAR MEDICAMENTO");
            Console.WriteLine("3. MENU - LISTAR MEDICAMENTO POR ID");
            Console.WriteLine("4. MENU - LISTAR TODOS OS MEDICAMENTOS");
            Console.WriteLine("0. SAIR");
            Console.WriteLine("--------------------------------------");
        }

        public static void MenuManipulacaoCliente()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("MENU - MANIPULAÇÃO DE CLIENTES");
            Console.WriteLine("1. MENU - ALTERAR CLIENTE");
            Console.WriteLine("2. MENU - INATIVAR CLIENTE");
            Console.WriteLine("3. MENU - BLOQUEAR CLIENTE");
            Console.WriteLine("4. MENU - DESBLOQUEAR CLIENTE");
            Console.WriteLine("5. MENU - LISTAR CLIENTE POR ID");
            Console.WriteLine("6. MENU - LISTAR TODOS OS CLIENTES");
            Console.WriteLine("0. SAIR");
            Console.WriteLine("--------------------------------------");
        }

        public static void MenuManipulacaoPrincipioAtivo()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("MENU - MANIPULAÇÃO DE PRINCÍPIO ATIVO");
            Console.WriteLine("1. MENU - ALTERAR PRINCÍPIO ATIVO");
            Console.WriteLine("2. MENU - INATIVAR PRINCÍPIO ATIVO");
            Console.WriteLine("3. MENU - LISTAR PRINCÍPIO ATIVO POR ID");
            Console.WriteLine("4. MENU - LISTAR TODOS OS PRINCÍPIOS ATIVOS");
            Console.WriteLine("0. SAIR");
            Console.WriteLine("--------------------------------------");
        }

        public static void MenuManipulacaoFornecedor()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("MENU - MANIPULAÇÃO DE FORNECEDORES");
            Console.WriteLine("1. MENU - ALTERAR FORNECEDOR");
            Console.WriteLine("2. MENU - ALTERAR SITUAÇÃO FORNECEDOR");
            Console.WriteLine("3. MENU - BLOQUEAR FORNECEDOR");
            Console.WriteLine("4. MENU - DESBLOQUEAR FORNECEDOR");
            Console.WriteLine("5. MENU - LISTAR FORNECEDOR POR ID");
            Console.WriteLine("6. MENU - LISTAR TODOS OS FORNECEDORES");
            Console.WriteLine("7. MENU - LISTAR TODOS OS FORNECEDORES BLOQUEADOS");
            Console.WriteLine("8. MENU - LISTAR FORNECEDOR BLOQUEADO POR CNPJ");
            Console.WriteLine("0. SAIR");
            Console.WriteLine("--------------------------------------");
        }

        public static void MenuCompraPrincipioAtivo()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("MENU - COMPRA DE PRINCÍPIO ATIVO");
            Console.WriteLine("1. MENU - COMPRAR PRINCÍPIO ATIVO");
            Console.WriteLine("2. MENU - LISTAR TODOS OS PRINCÍPIOS ATIVOS");
            Console.WriteLine("3. MENU - LISTAR PRINCÍPIO ATIVO POR ID");
            Console.WriteLine("0. SAIR");
            Console.WriteLine("--------------------------------------");
        }

        public static void MenuRelatorio()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("MENU - RELATÓRIOS");
            Console.WriteLine("1. MENU - RELATÓRIO DE VENDAS POR PERÍODO");
            Console.WriteLine("2. MENU - RELATÓRIO DE MEDICAMENTOS MAIS VENDIDOS");
            Console.WriteLine("3. MENU - RELATÓRIO DE COMPRAS POR FORNECEDOR");
            Console.WriteLine("0. SAIR");
            Console.WriteLine("--------------------------------------");
        }
    }
}
