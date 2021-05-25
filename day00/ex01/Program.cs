using System;

String[] dictNames = System.IO.File.ReadAllLines("./us.txt");
Console.Write("Enter name:");
try
{
    bool sign = true;
    ConsoleKeyInfo key;
    string name = Console.ReadLine();
    foreach (string dictName in dictNames)
    {
        if (dictName.CompareTo(name) == 0)
            Console.WriteLine($"Hello {name}");
        else if (LevenshteinDistance(name, dictName) < 3)
        {
            Console.Write("\ndid you mean: " + dictName + (" ? Y/N\n your answer: "));
            key = Console.ReadKey();
            if (key.KeyChar != 'Y' && key.KeyChar != 'y' && key.KeyChar != 'n' && key.KeyChar != 'N')
            {
                do
                {
                    Console.WriteLine("\nPlease type Y or N for your answer!");
                    key = Console.ReadKey();
                }
                while (key.KeyChar != 'Y' && key.KeyChar != 'y' && key.KeyChar != 'n' && key.KeyChar != 'N');

            }
            if (key.KeyChar == 'Y' || key.KeyChar == 'y')
            {
                Console.WriteLine($"\nHello {dictName}");
                sign = false;
                break;
            }
            else
                continue;

        }
    }
    if (sign)
        Console.WriteLine("\nYour name was not found.");
}
catch (Exception e)
{
    Console.WriteLine("Что то пошло не так!!!!! вот что говорит ошибка: " + e.Message);
}

static int LevenshteinDistance(string name, string dictName)
{
    int lenName = name.Length;
    int lenDictName = dictName.Length;
    int[,] dist = new int[lenName + 1, lenDictName + 1];
    if (lenName == 0)
        return lenDictName;
    if (lenDictName == 0)
        return lenName;
    for (int i = 0; i <= lenName; i++)
        dist[i, 0] = i;
    for (int j = 0; j <= lenDictName; j++)
        dist[0, j] = j;
    for (int i = 1; i <= lenName; i++)
    {
        for (int j = 1; j <= lenDictName; j++)
        {
            int cost = (dictName[j - 1] == name[i - 1]) ? 0 : 1;
            dist[i, j] = Math.Min(
                Math.Min(dist[i - 1, j] + 1, dist[i, j - 1] + 1),
                dist[i - 1, j - 1] + cost);
        }
    }
    return dist[lenName, lenDictName];
}