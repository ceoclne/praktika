using System;
class Person
{
    private string name;
    private int age;
    private string address;

    // Методы для установки значений
    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetAge(int age)
    {
        this.age = age;
    }

    public void SetAddress(string address)
    {
        this.address = address;
    }

    // Методы для получения значений
    public string GetName()
    {
        return name;
    }

    public int GetAge()
    {
        return age;
    }

    public string GetAddress()
    {
        return address;
    }
}
class Program
{
    static void Main()
    {
        Person person1 = new Person();
        person1.SetName("Вика");
        person1.SetAge(17);
        person1.SetAddress("Санкт-Петербург");

        Person person2 = new Person();
        person2.SetName("Даша");
        person2.SetAge(17);
        person2.SetAddress("Устье");

        Console.WriteLine("Человек 1:");
        Console.WriteLine($"Имя: {person1.GetName()}");
        Console.WriteLine($"Возраст: {person1.GetAge()}");
        Console.WriteLine($"Адрес: {person1.GetAddress()}");

        Console.WriteLine("\nЧеловек 2:");
        Console.WriteLine($"Имя: {person2.GetName()}");
        Console.WriteLine($"Возраст: {person2.GetAge()}");
        Console.WriteLine($"Адрес: {person2.GetAddress()}");
    }
}