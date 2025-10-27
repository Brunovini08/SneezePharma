using SneezePharma.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharma.Models.Produce
{
    public class ProduceModel
    {

        public int ID { get; private set; }
        public DateOnly DataProducao { get; private set; }
        public string Medicamento { get; private set; }
        public int Quantidade { get; private set; }

        public ProduceModel(int id, string medicamento, int quantidade)
        {

            ID = id;
            DataProducao = DateOnly.FromDateTime(DateTime.Now);
            Medicamento = medicamento;
            Quantidade = quantidade;
        }

        public ProduceModel(int iD, DateOnly dataProducao, string medicamento, int quantidade)
        {
            ID = iD;
            DataProducao = dataProducao;
            Medicamento = medicamento;
            Quantidade = quantidade;
        }

        public void setQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }

        public bool ValidarDataProducao(DateOnly data)
        {
            DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);


            if (data > hoje)
            {
                Console.WriteLine("Não pode adicionar uma data no futuro.");
                return false;
            }
            if (data < hoje.AddMonths(-1))
            {
                Console.WriteLine("Data de produção muito antiga.");
                return false;
            }

            DataProducao = data;
            Console.WriteLine($"Data da produção registrada: {DataProducao: dd/MM/yyyy}");
            return true;
        }

        public override string ToString()
        {
            return $"ID: {ID.ToString().PadLeft(5, '0')}, CDB: {Medicamento}, Data: {DataProducao:dd/MM/yyyy}, Quantidade: {Quantidade}";
        }
        public string SalvarArquivo()
        {
            return $"{this.ID.ToString().PadLeft(5, '0')}{this.DataProducao:ddMMyyyy}{this.Medicamento}{this.Quantidade.ToString().PadLeft(3, '0')}";
        }


    }
}
