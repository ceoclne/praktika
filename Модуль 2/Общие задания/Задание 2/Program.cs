using System;
class Shape
{
    public virtual double Area()
    {
        return 0;
    }

    public virtual double Perimeter()
    {
        return 0;
    }
}
class Circle : Shape
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }

    // Площадь и периметр круга
    public override double Area()
    {
        return Math.PI * radius * radius;
    }

    public override double Perimeter()
    {
        return 2 * Math.PI * radius;
    }
}
class Rectangle : Shape
{
    private double width;
    private double height;

    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    // Площадь и периметр прямоугольника
    public override double Area()
    {
        return width * height;
    }

    public override double Perimeter()
    {
        return 2 * (width + height);
    }
}
class Program
{
    static void Main()
    {
        Shape shape = new Shape();
        Circle circle = new Circle(5);
        Rectangle rectangle = new Rectangle(4, 6);

        Console.WriteLine("Фигура:");
        Console.WriteLine($"Площадь: {shape.Area()}");
        Console.WriteLine($"Периметр: {shape.Perimeter()}");

        Console.WriteLine("\nКруг:");
        Console.WriteLine($"Площадь: {circle.Area():F2}");
        Console.WriteLine($"Периметр: {circle.Perimeter():F2}");

        Console.WriteLine("\nПрямоугольник:");
        Console.WriteLine($"Площадь: {rectangle.Area():F2}");
        Console.WriteLine($"Периметр: {rectangle.Perimeter():F2}");
    }
}