using SneezePharma;
using SneezePharma.Models;

Console.WriteLine("Digite o código de barras: ");
string cdb = Console.ReadLine();

Medicine medicine = new Medicine(cdb, "", 'A', 0, DateOnly.MinValue, DateOnly.MinValue, 'A');

bool valido = medicine.ValidarCDB(cdb);

