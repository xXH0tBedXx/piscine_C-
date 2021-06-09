using System;
using d02_ex00.Model;
using System.Text.Json;
using System.IO;

namespace d02_ex00
{
    class Program
    {
        static String jsonBook = File.ReadAllText("./book_reviews.json");
        static void Main(string[] args)
        {
            //Console.Write("Input search text: ");
            //String search = Console.ReadLine();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            results results = JsonSerializer.Deserialize<results>(jsonBook, options);
            //Console.WriteLine(results.List_Name);
        }
    }
}