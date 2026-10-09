using System;

interface IProduct
{
    double GetCost();
    int GetStock();
}

class Food : IProduct
{
    private string name;
    private double price;
    private int stock;

    public Food(string name, double price, int stock)
    {
        this.name = name;
        this.price = price;
        this.stock = stock;
    }

    public double GetCost()
    {
        return price;
    }

    public int GetStock()
    {
        return stock;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Продукт: {name}");
        Console.WriteLine($"Цена: {GetCost():F2} руб.");
        Console.WriteLine($"Остаток: {GetStock()} шт.");
    }
}

class Clothing : IProduct
{
    private string name;
    private double price;
    private int stock;

    public Clothing(string name, double price, int stock)
    {
        this.name = name;
        this.price = price;
        this.stock = stock;
    }

    public double GetCost()
    {
        return price;
    }

    public int GetStock()
    {
        return stock;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Одежда: {name}");
        Console.WriteLine($"Цена: {GetCost():F2} руб.");
        Console.WriteLine($"Остаток: {GetStock()} шт.");
    }
}

class Electronics : IProduct
{
    private string name;
    private double price;
    private int stock;

    public Electronics(string name, double price, int stock)
    {
        this.name = name;
        this.price = price;
        this.stock = stock;
    }

    public double GetCost()
    {
        return price;
    }

    public int GetStock()
    {
        return stock;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Электроника: {name}");
        Console.WriteLine($"Цена: {GetCost():F2} руб.");
        Console.WriteLine($"Остаток: {GetStock()} шт.");
    }
}

class Program
{
    static void Main()
    {
        Food milk = new Food("Молоко", 2.50, 20);
        Clothing shirt = new Clothing("Футболка", 35, 10);
        Electronics headphones = new Electronics("Наушники", 50, 5);

        milk.ShowInfo();

        Console.WriteLine();

        shirt.ShowInfo();

        Console.WriteLine();

        headphones.ShowInfo();
    }
}