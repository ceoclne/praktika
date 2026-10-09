using System;

interface IDrawing
{
    void DrawLine();
    void DrawCircle();
    void DrawRectangle();
}

class Canvas : IDrawing
{
    public void DrawLine()
    {
        Console.WriteLine("Нарисована линия.");
    }

    public void DrawCircle()
    {
        Console.WriteLine("Нарисован круг.");
    }

    public void DrawRectangle()
    {
        Console.WriteLine("Нарисован прямоугольник.");
    }
}

class Program
{
    static void Main()
    {
        Canvas canvas = new Canvas();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Нарисовать линию");
            Console.WriteLine("2 - Нарисовать круг");
            Console.WriteLine("3 - Нарисовать прямоугольник");
            Console.WriteLine("0 - Выход");

            Console.Write("\nВыберите действие: ");
            string choice = Console.ReadLine();

            if (choice == "0")
                break;

            Console.WriteLine();

            if (choice == "1")
            {
                canvas.DrawLine();
            }
            else if (choice == "2")
            {
                canvas.DrawCircle();
            }
            else if (choice == "3")
            {
                canvas.DrawRectangle();
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}