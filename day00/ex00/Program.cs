using System;

double overpayingWithReductionTerms = 0;
double overpayingWithDecreasePayment = 0;
double sum; //args[0]
double rate; //args[1]
int selectedMonth; //args[2]
double i;
int term; //args[3]
double payment; //args[4]

if (!Double.TryParse(args[0], out sum))
    wrong_input();
if (!Double.TryParse(args[1], out rate))
    wrong_input();
i = rate / 12 / 100;
if (!Int32.TryParse(args[2], out term))
    wrong_input();
if (!Int32.TryParse(args[3], out selectedMonth))
    wrong_input();
if (!double.TryParse(args[4], out payment))
    wrong_input();
if (selectedMonth < 1 || selectedMonth > term || sum < 1 || rate < 0 || term < 1 || payment < 1 || payment > sum)
    wrong_input();
double annuityPayment = (sum * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);
double cpSum = sum;
double actGiv = 0;
String[] cap = { "Дата", "Платеж", "ОД", "Проценты", "Остаток долга" };
Console.Write($"Ваш кредит равен ");
Console.ForegroundColor = ConsoleColor.DarkRed;
Console.Write($"{sum}p.\n");
Console.ResetColor();
Console.Write("С годовой ставкой в ");
Console.ForegroundColor = ConsoleColor.DarkRed;
Console.Write($"{ rate}%");
Console.ResetColor();
Console.Write($" и сроком в ");
Console.ForegroundColor = ConsoleColor.DarkRed;
Console.Write($"{term} месяцев");
Console.ResetColor();
Console.Write("\nтаблица погашения вашего кредита");
Console.Write(" будет выглядеть следующим образом:\n");
Console.WriteLine("{0,-20}|{1,-20}|{2,-20}|{3,-20}|{4,-20}|", cap[0], cap[1], cap[2], cap[3], cap[4]);
Console.WriteLine("--------------------|--------------------|--------------------|--------------------|--------------------|");
var data = new DateTime(2021, 5, 1);
//data = DateTime.Now;
double percent;
for (int counter2 = 0; counter2 < term; counter2++)
{
    percent = (sum * rate * DateTime.DaysInMonth(data.Year, data.Month)) / (100 * (DateTime.IsLeapYear(data.Year) ? 366 : 365));
    data = data.AddMonths(1);
    sum -= annuityPayment - percent;
    actGiv += annuityPayment;
    Console.Write("{0,-20}|", data.ToString("d"));
    Console.Write("{0,-18:F2}p.|", annuityPayment);
    Console.Write("{0,-18:F2}p.|", annuityPayment - percent);
    Console.Write("{0,-18:F2}p.|", percent);
    if (sum > 1)
        Console.Write("{0,-18:F2}p.|\n", sum);
    else
    {
        Console.Write("0                 p.|\n");
        sum += annuityPayment;
    }
    Console.WriteLine("--------------------|--------------------|--------------------|--------------------|--------------------|");
}
Console.Write("Фактическая переплата: ");
Console.ForegroundColor = ConsoleColor.DarkRed;
Console.Write("{0:F2}p.\n", actGiv - cpSum + sum);
Console.ResetColor();
Console.Write("\nтаблица погашения вашего кредита c ");
Console.ForegroundColor = ConsoleColor.DarkRed;
Console.Write("уменьшением суммы платежа");
Console.ResetColor();
Console.WriteLine(":\n{0,-20}|{1,-20}|{2,-20}|{3,-20}|{4,-20}|", cap[0], cap[1], cap[2], cap[3], cap[4]);
Console.WriteLine("--------------------|--------------------|--------------------|--------------------|--------------------|");
data = new DateTime(2021, 5, 1);
sum = cpSum;
annuityPayment = (sum * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);
//data = DateTime.Now;
for (int counter2 = 0; counter2 < term; counter2++)
{
    if (counter2 == selectedMonth)
    {
        sum -= payment;
        overpayingWithDecreasePayment += payment;
        annuityPayment = (sum * i * Math.Pow((1 + i), term - counter2)) / (Math.Pow((1 + i), term - counter2) - 1);
        percent = (sum * rate * DateTime.DaysInMonth(data.Year, data.Month)) / (100 * (DateTime.IsLeapYear(data.Year) ? 366 : 365));
        data = data.AddMonths(1);
        sum -= (annuityPayment - percent);
        overpayingWithDecreasePayment += annuityPayment;

    }
    else
    {
        percent = (sum * rate * DateTime.DaysInMonth(data.Year, data.Month)) / (100 * (DateTime.IsLeapYear(data.Year) ? 366 : 365));
        data = data.AddMonths(1);
        sum -= annuityPayment - percent;
        overpayingWithDecreasePayment += annuityPayment;
    }
    Console.Write("{0,-20}|", data.ToString("d"));
    Console.Write("{0,-18:F2}p.|", annuityPayment);
    Console.Write("{0,-18:F2}p.|", annuityPayment - percent);
    Console.Write("{0,-18:F2}p.|", percent);
    if (sum > 1)
        Console.Write("{0,-18:F2}p.|\n", sum);
    else
    {
        Console.Write("0                 p.|\n");
        sum += annuityPayment;
    }
    Console.WriteLine("--------------------|--------------------|--------------------|--------------------|--------------------|");
}
Console.Write("Фактическая переплата: ");
Console.ForegroundColor = ConsoleColor.DarkRed;
Console.Write("{0:F2}p.\n", overpayingWithDecreasePayment - cpSum + sum);
Console.ResetColor();
Console.Write("\nтаблица погашения вашего кредита c ");
Console.ForegroundColor = ConsoleColor.DarkRed;
Console.Write("уменьшением количество месяцев");
Console.ResetColor();
Console.WriteLine(":\n{0,-20}|{1,-20}|{2,-20}|{3,-20}|{4,-20}|", cap[0], cap[1], cap[2], cap[3], cap[4]);
Console.WriteLine("--------------------|--------------------|--------------------|--------------------|--------------------|");
data = new DateTime(2021, 5, 1);
sum = cpSum;
annuityPayment = (sum * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);
//data = DateTime.Now;
for (int counter2 = term; counter2 > 0; counter2--)
{
    if (counter2 == selectedMonth)
    {
        sum -= payment;
        overpayingWithReductionTerms += payment;
        counter2 = (int)Math.Log(payment/(payment - i * sum), i + 1);
        percent = (sum * rate * DateTime.DaysInMonth(data.Year, data.Month)) / (100 * (DateTime.IsLeapYear(data.Year) ? 366 : 365));
        data = data.AddMonths(1);
        sum -= (annuityPayment - percent);
        overpayingWithReductionTerms += annuityPayment;

    }
    else
    {
        percent = (sum * rate * DateTime.DaysInMonth(data.Year, data.Month)) / (100 * (DateTime.IsLeapYear(data.Year) ? 366 : 365));
        data = data.AddMonths(1);
        sum -= annuityPayment - percent;
        overpayingWithReductionTerms += annuityPayment;
    }
    Console.Write("{0,-20}|", data.ToString("d"));
    Console.Write("{0,-18:F2}p.|", annuityPayment);
    Console.Write("{0,-18:F2}p.|", annuityPayment - percent);
    Console.Write("{0,-18:F2}p.|", percent);
    if (sum > 1)
        Console.Write("{0,-18:F2}p.|\n", sum);
    else
    {
        Console.Write("0                 p.|\n");
        sum += annuityPayment;
    }
    Console.WriteLine("--------------------|--------------------|--------------------|--------------------|--------------------|");
}
Console.Write("Фактическая переплата: ");
Console.ForegroundColor = ConsoleColor.DarkRed;
Console.Write("{0:F2}p.\n", overpayingWithReductionTerms - cpSum + sum);
Console.ResetColor();


Console.ForegroundColor = ConsoleColor.DarkRed;
if (overpayingWithReductionTerms > overpayingWithDecreasePayment)
    Console.WriteLine("Уменьшение платежа выгоднее уменьшения срока на {0:F2}p.", overpayingWithReductionTerms - overpayingWithDecreasePayment);
else if (overpayingWithReductionTerms == overpayingWithDecreasePayment)
    Console.WriteLine("Переплата одинакова в обоих вариантах.");
else
    Console.WriteLine("Уменьшение срока выгоднее уменьшения платежа на {0:F2}p.",overpayingWithDecreasePayment - overpayingWithReductionTerms);
Console.ResetColor();

static void wrong_input()
{
    Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
    Environment.Exit(0);
}