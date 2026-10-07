using System;

class Program
{
    delegate void TaskHandler(string task);

    static void Notify(string task)
    {
        Console.WriteLine($"Уведомление: {task}");
    }

    static void Log(string task)
    {
        Console.WriteLine($"Запись в журнал: {task}");
    }

    static void Main()
    {
        while (true)
        {
            Console.Write("\nВведите задачу (или 0 для выхода): ");
            string task = Console.ReadLine();

            if (task == "0") break;

            Console.Write("Выберите действие (1 - уведомление, 2 - журнал): ");
            string choice = Console.ReadLine();

            TaskHandler handler = choice == "1" ? Notify : Log;
            handler(task);
        }
    }
}