using System;
class Figure
{
    public virtual double Area() => 0;
}
class Circle : Figure
{
    private double r;
    public Circle(double r) => this.r = r;
    public override double Area() => Math.PI * r * r;
}
class Rectangle : Figure
{
    private double a, b;
    public Rectangle(double a, double b) { this.a = a; this.b = b; }
    public override double Area() => a * b;
}
class Triangle : Figure
{
    private double b, h;
    public Triangle(double b, double h) { this.b = b; this.h = h; }
    public override double Area() => b * h / 2;
}
class Program
{
    delegate double AreaDelegate(Figure figure);
    static void Main()
    {
        Figure[] figures = { new Circle(5), new Rectangle(4, 6), new Triangle(8, 3) };
        AreaDelegate calculateArea = figure => figure.Area();

        foreach (Figure figure in figures)
            Console.WriteLine($"Площадь: {calculateArea(figure):F2}");
    }
}