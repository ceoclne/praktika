using System;

class MyClass
{
    private int number;
    private string text;

    public MyClass(int number, string text)
    {
        this.number = number;
        this.text = text;
    }

    public MyClass()
    {
        number = 0;
        text = "По умолчанию";
    }

    ~MyClass()
    {
        Console.WriteLine("Деструктор: объект удалён.");
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Число: {number}");
        Console.WriteLine($"Текст: {text}");
    }
}

class Program
{
    static void CreateObjects()
    {
        MyClass object1 = new MyClass(10, "Привет");
        object1.ShowInfo();

        Console.WriteLine();

        MyClass object2 = new MyClass();
        object2.ShowInfo();
    }

    static void Main()
    {
        CreateObjects();

        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine();
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}