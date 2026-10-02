using System;

class Employee
{
    // Поля класса
    private string name;
    private int age;
    private string position;
    private double salary;

    // Конструктор с параметрами
    public Employee(string name, int age, string position, double salary)
    {
        this.name = name;
        this.age = age;
        this.position = position;
        this.salary = salary;
    }

    // Методы для получения значений полей
    public string GetName()
    {
        return name;
    }

    public int GetAge()
    {
        return age;
    }

    public string GetPosition()
    {
        return position;
    }

    public double GetSalary()
    {
        return salary;
    }

    // Методы для изменения значений полей
    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetAge(int age)
    {
        this.age = age;
    }

    public void SetPosition(string position)
    {
        this.position = position;
    }

    public void SetSalary(double salary)
    {
        this.salary = salary;
    }

    // Расчет годового дохода
    public double GetAnnualIncome()
    {
        return salary * 12;
    }

    // Вывод информации о сотруднике
    public void ShowInfo()
    {
        Console.WriteLine($"Имя: {name}");
        Console.WriteLine($"Возраст: {age}");
        Console.WriteLine($"Должность: {position}");
        Console.WriteLine($"Месячная зарплата: {salary} рублей ");
        Console.WriteLine($"Годовой доход: {GetAnnualIncome()} рублей ");
    }
}

class Program
{
    static void Main()
    {
        // Создаем объекты класса Employee
        Employee employee1 = new Employee(
            "Вика",
            25,
            "Программист",
            80000
        );

        Employee employee2 = new Employee(
            "Даша",
            30,
            "Дизайнер",
            70000
        );

        // Выводим информацию о сотрудниках
        employee1.ShowInfo();

        Console.WriteLine();

        employee2.ShowInfo();

        // Изменяем зарплату первого сотрудника
        employee1.SetSalary(90000);

        Console.WriteLine("\nПосле изменения зарплаты:");
        Console.WriteLine($"Новая зарплата: {employee1.GetSalary()} рублей ");
        Console.WriteLine($"Новый годовой доход: {employee1.GetAnnualIncome()} рублей ");

        Console.ReadKey();
    }
}