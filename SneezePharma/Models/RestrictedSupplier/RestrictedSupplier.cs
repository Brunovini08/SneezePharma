using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.RestrictedSupplier
{
    public class RestrictedSupplier
    {
        public List<Supplier> RestrictedSuppliers { get; } = new List<Supplier>();
        public void AdicionarFornecedorRestrito(Supplier fornecedor)
        {
            Supplier encontrado;
            do
            {
            Console.WriteLine("Digite o Cnpj de fornecedores bloqueados: ");
            string cnpj = Console.ReadLine();
            encontrado = fornecedor.LocalizarFornecedor(cnpj);

                if (encontrado != null)
                {
                    RestrictedSuppliers.Add(encontrado);
                    Console.WriteLine($"Fornecedor adicionado à bloqueados: {encontrado}");
                }
                else
                {
                    Console.WriteLine("Fornecedor não encontrado");
                }
            } while (encontrado == null);
        }
        
        public void RetirarFornecedorDeBloqueados()
        {
            Supplier fornecedor;
            do
            {
                Console.WriteLine("Digite o Cnpj que deseja retirar da lista de fornecedores bloqueados: ");
                string cnpj = Console.ReadLine();

                fornecedor = RestrictedSuppliers.Find(f => f.Cnpj == cnpj);

                if (fornecedor != null)
                {
                    RestrictedSuppliers.Remove(fornecedor);
                    Console.WriteLine("Fornecedor removido da lista de restritos!");
                }
                else
                {
                    Console.WriteLine("Fornecedor não encontrado");
                }
            } while (fornecedor == null);
        }

        public void ListarrForncedoresRestritos()
        {
            if(RestrictedSuppliers.Count == 0)
            {
                Console.WriteLine("Não há nenhum fornecedor bloqueado no momento");
            }
            else
            {
                Console.WriteLine("Lista de fornecedores bloqueados: ");
                foreach (var fornecedor in RestrictedSuppliers)
                {
                    Console.WriteLine(fornecedor.ToString());
                }
            }
        }
    }
}
