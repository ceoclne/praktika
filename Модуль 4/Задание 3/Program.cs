using System;

interface IStudent
{
    double GetAverageGrade();
    int GetCourse();
}

class FirstYearStudent : IStudent
{
    private string name;
    private int[] grades;

    public FirstYearStudent(string name, int[] grades)
    {
        this.name = name;
        this.grades = grades;
    }

    public double GetAverageGrade()
    {
        double sum = 0;

        foreach (int grade in grades)
            sum += grade;

        return sum / grades.Length;
    }

    public int GetCourse()
    {
        return 1;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Студент: {name}");
        Console.WriteLine($"Курс: {GetCourse()}");
        Console.WriteLine($"Средний балл: {GetAverageGrade():F2}");
    }
}

class SecondYearStudent : IStudent
{
    private string name;
    private int[] grades;

    public SecondYearStudent(string name, int[] grades)
    {
        this.name = name;
        this.grades = grades;
    }

    public double GetAverageGrade()
    {
        double sum = 0;

        foreach (int grade in grades)
            sum += grade;

        return sum / grades.Length;
    }

    public int GetCourse()
    {
        return 2;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Студент: {name}");
        Console.WriteLine($"Курс: {GetCourse()}");
        Console.WriteLine($"Средний балл: {GetAverageGrade():F2}");
    }
}

class ThirdYearStudent : IStudent
{
    private string name;
    private int[] grades;

    public ThirdYearStudent(string name, int[] grades)
    {
        this.name = name;
        this.grades = grades;
    }

    public double GetAverageGrade()
    {
        double sum = 0;

        foreach (int grade in grades)
            sum += grade;

        return sum / grades.Length;
    }

    public int GetCourse()
    {
        return 3;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Студент: {name}");
        Console.WriteLine($"Курс: {GetCourse()}");
        Console.WriteLine($"Средний балл: {GetAverageGrade():F2}");
    }
}

class Program
{
    static void Main()
    {
        FirstYearStudent nastya = new FirstYearStudent(
            "Настя",
            new int[] { 8, 9, 7, 10, 9 }
        );

        SecondYearStudent vika = new SecondYearStudent(
            "Вика",
            new int[] { 9, 8, 10, 9, 8 }
        );

        ThirdYearStudent dasha = new ThirdYearStudent(
            "Даша",
            new int[] { 10, 9, 9, 8, 10 }
        );

        nastya.ShowInfo();

        Console.WriteLine();

        vika.ShowInfo();

        Console.WriteLine();

        dasha.ShowInfo();
    }
}