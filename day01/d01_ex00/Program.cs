using System;
namespace d01_ex00
{
    class Program
    {
        private static double value1;
        private static double value2;
        static void Main(string[] args)
        {
            Models.ExchangeRate rate = new Models.ExchangeRate();
            try
            {
                if (args.Length != 2)
                    throw new Exception();
                String[] sum = args[0].Split(' ');
                String ratesDirectory = args[1];
                bool suc = double.TryParse(sum[0], out double value);
                if (!suc)
                    throw new Exception();
                if (sum[1] != "RUB" && sum[1] != "EUR" && sum[1] != "USD")
                    throw new Exception();
                String[] valuev = System.IO.File.ReadAllLines(ratesDirectory + "/" + sum[1] + ".txt");
            valuev[0] = valuev[0].Remove(0, 4).Replace(',', '.');
            valuev[1] = valuev[1].Remove(0, 4).Replace(',', '.');
            suc = double.TryParse(valuev[0], out value1);
            if (!suc)
                throw new Exception();
            suc = double.TryParse(valuev[1], out value2);
            if (!suc)
                throw new Exception();
                Exchanger exchg = new Exchanger();
                rate = exchg.convert(sum[1], value, value1, value2, rate);
                if (sum[1] == "RUB")
                    Console.WriteLine("Исходная сумма = {0:F2}\nСумма в EUR = {1:F2}\nСумма в USD = {2:F2}", Models.ExchangeRate.RUB, Models.ExchangeRate.EUR, Models.ExchangeRate.USD);
                else if (sum[1] == "USD")
                    Console.WriteLine("Исходная сумма = {0:F2}\nСумма в RUB = {1:F2}\nСумма в EUR = {2:F2}", Models.ExchangeRate.USD, Models.ExchangeRate.RUB, Models.ExchangeRate.EUR);
                else if (sum[1] == "EUR")
                    Console.WriteLine("Исходная сумма = {0:F2}\nСумма в RUB = {1:F2}\nСумма в USD = {2:F2}", Models.ExchangeRate.EUR, Models.ExchangeRate.RUB, Models.ExchangeRate.USD);
            }
            catch
            {
                Console.WriteLine("Ошибка ввода.Проверьте входные данные и повторите запрос.");
                Environment.Exit(0);
            }
        }
    }
}