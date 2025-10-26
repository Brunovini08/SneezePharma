using SneezePharma.Enums;
using SneezePharma.Exceptions;
using SneezePharma.Helpers;
using SneezePharma.Models;
using SneezePharma.Models.SalesItem;
using SneezePharma.Utils;

var pharma = new Pharma();
//var dataEmString = "19102024";
//var formatacao = $"{dataEmString[0..2]}/{dataEmString[2..4]}/{dataEmString[4..8]}";

//var dataEmDateOnly = DateOnly.Parse(formatacao);

//Console.WriteLine(dataEmDateOnly);

var med1 = new Medicine(
    cdb: "1234567890123",
    nome: "Paracetamol 500mg",
    categoria: 'A', // Analgésico
    valorVenda: 19.99m,
    ultimaVenda: new DateOnly(2025, 10, 20),
    dataCadastro: new DateOnly(2024, 12, 15),
    situacao: 'A' // Ativo
);

var med2 = new Medicine(
    cdb: "9876543210987",
    nome: "Amoxicilina 500mg",
    categoria: 'B', // Antibiótico
    valorVenda: 100321313100005.50m,
    ultimaVenda: new DateOnly(2025, 9, 30),
    dataCadastro: new DateOnly(2024, 11, 1),
    situacao: 'A'
);

var med3 = new Medicine(
    cdb: "1112223334445",
    nome: "Ibuprofeno 400mg",
    categoria: 'I', // Anti-inflamatório
    valorVenda: 32.75m,
    ultimaVenda: new DateOnly(2025, 8, 15),
    dataCadastro: new DateOnly(2023, 10, 25),
    situacao: 'I' // Inativo
);

var med4 = new Medicine(
    cdb: "5556667778889",
    nome: "Vitamina C 1g",
    categoria: 'V', // Vitamina
    valorVenda: 12.49m,
    ultimaVenda: new DateOnly(2025, 10, 5),
    dataCadastro: new DateOnly(2024, 5, 10),
    situacao: 'A'
);

var med5 = new Medicine(
    cdb: "0001112223334",
    nome: "Diclofenaco Sódico 50mg",
    categoria: 'I', // Anti-inflamatório
    valorVenda: 28.90m,
    ultimaVenda: new DateOnly(2025, 7, 10),
    dataCadastro: new DateOnly(2023, 9, 1),
    situacao: 'I'
);

pharma.Medicamentos.Add(med1);
pharma.Medicamentos.Add(med2);
pharma.Medicamentos.Add(med3);
pharma.Medicamentos.Add(med4);
pharma.Medicamentos.Add(med5);

var cliente1 = new Customer(
    "12345678901",
    "João da Silva",
    DateOnly.Parse("20/05/1988"),
    "11988776655",
    null,
    DateOnly.Parse("15/03/2020"),
    SituationCustomer.A
);

var cliente2 = new Customer(
    "98765432100",
    "Maria Oliveira",
    DateOnly.Parse("10/02/1995"),
    "21977771111",
    null,
    DateOnly.Parse("25/08/2021"),
    situacao: SituationCustomer.I
);

pharma.Clientes.Add(cliente1);
pharma.Clientes.Add(cliente2);

pharma.VendasMedicamento();