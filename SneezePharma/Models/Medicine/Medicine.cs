using Microsoft.VisualBasic;
using SneezePharma.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SneezePharma;

namespace SneezePharma.Models
{
    public class Medicine
    {
        public string CDB { get; private set; }
        public string Nome { get; private set; }
        public char Categoria { get; private set; }
        public decimal ValorVenda { get; private set; }
        public DateOnly UltimaVenda { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public char Situacao { get; private set; }

        public Medicine(string cdb, string nome, char categoria, decimal valorVenda, DateOnly ultimaVenda, DateOnly dataCadastro, char situacao)
        {
            CDB = cdb;
            Nome = nome;
            Categoria = categoria;
            ValorVenda = valorVenda;
            UltimaVenda = ultimaVenda;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }
        public void setNome(string nome)
        {
            Nome = nome;
        }
        public void setSituacao(char situacao)
        {
            Situacao = situacao;
        }
        public void setUltimaVenda()
        {
            this.UltimaVenda = DateOnly.FromDateTime(DateTime.Now);
        }
        public bool ValidarCDB(string cdb)
        {
            int[] resultado = new int[13];
            resultado = cdb.Select(c => int.Parse(c.ToString())).ToArray();
            if (resultado[0] != 7 || resultado[1] != 8 || resultado[2] != 9)
            {
                Console.WriteLine("Os três primeiros dígitos devem ser especificamente: 789");
                return false;
            }
            else
            {
                int somaImpar = 0, somaPar = 0;
                for (int i = 0; i < 12; i++)
                {
                    if (i % 2 == 0)
                    {
                        somaImpar += resultado[i];
                    }
                    else
                    {
                        somaPar += resultado[i];
                    }
                }
                int somaTotal = somaImpar + somaPar * 3;
                int digitoVerificador = 10 - somaTotal % 10;
                if (digitoVerificador == 10)
                {
                    digitoVerificador = 0;
                }
                if (digitoVerificador == resultado[12])
                {
                    Console.WriteLine("Código de Barra válido");
                    return true;
                }
                else
                {
                    Console.WriteLine("Dígito verificador inválido.");
                    return false;
                }
            }
        }

        public override string ToString()
        {
            return $"Código de Barras: {CDB}\nNome: {Nome}\nCategoria: {Categoria}\nValor da Venda: {ValorVenda}\nUltima Venda: {UltimaVenda}\nData de cadastro: {DataCadastro}\n Situação: {Situacao}";
        }

        public string SalvarArquivo()
        {
            return $"{this.CDB}{this.Nome.PadRight(40, ' ')}{this.Categoria}{this.ValorVenda}{this.UltimaVenda}{this.DataCadastro}{this.Situacao}";
        }
    }
}
