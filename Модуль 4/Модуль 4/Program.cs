using System;

interface IFigure
{
    double Area();
    double Perimeter();
}

class Circle : IFigure
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }

    public double Area()
    {
        return Math.PI * radius * radius;
    }

    public double Perimeter()
    {
        return 2 * Math.PI * radius;
    }
}

class Rectangle : IFigure
{
    private double width;
    private double height;

    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    public double Area()
    {
        return width * height;
    }

    public double Perimeter()
    {
        return 2 * (width + height);
    }
}

class Triangle : IFigure
{
    private double a;
    private double b;
    private double c;

    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public double Area()
    {
        double p = Perimeter() / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public double Perimeter()
    {
        return a + b + c;
    }
}

class Program
{
    static void Main()
    {
        Circle circle = new Circle(11);
        Rectangle rectangle = new Rectangle(6, 7);
        Triangle triangle = new Triangle(6, 2, 7);

        Console.WriteLine("Круг:");
        Console.WriteLine($"Площадь: {circle.Area():F2}");
        Console.WriteLine($"Периметр: {circle.Perimeter():F2}");

        Console.WriteLine("\nПрямоугольник:");
        Console.WriteLine($"Площадь: {rectangle.Area():F2}");
        Console.WriteLine($"Периметр: {rectangle.Perimeter():F2}");

        Console.WriteLine("\nТреугольник:");
        Console.WriteLine($"Площадь: {triangle.Area():F2}");
        Console.WriteLine($"Периметр: {triangle.Perimeter():F2}");
    }
}