using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    delegate bool Filter(string data);

    static bool FilterByKeyword(string data)
    {
        return data.ToLower().Contains("работа");
    }

    static bool FilterByDate(string data)
    {
        return data.Contains("2026");
    }

    static void Main()
    {
        List<string> data = new List<string>
        {
            "Работа с данными 2025",
            "Учёба C# 2026",
            "Работа над проектом 2026",
            "Отдых 2025",
            "Изучение делегатов 2026"
        };

        Console.WriteLine("Выберите фильтр:");
        Console.WriteLine("1 - По ключевому слову");
        Console.WriteLine("2 - По году");
        Console.Write("Ваш выбор: ");
        string choice = Console.ReadLine();

        Filter filter = choice == "1" ? FilterByKeyword : FilterByDate;

        Console.WriteLine("\nРезультат фильтрации:");

        foreach (string item in data.Where(x => filter(x)))
            Console.WriteLine(item);
    }
}