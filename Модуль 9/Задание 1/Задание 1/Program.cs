
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Group { get; set; }
    public double Grade { get; set; }

    public Student(string name, int age, string group, double grade)
    {
        Name = name;
        Age = age;
        Group = group;
        Grade = grade;
    }

    public override string ToString()
    {
        return $"Имя: {Name}, возраст: {Age}, группа: {Group}, средний балл: {Grade}";
    }
}

class Program
{
    static List<Student> students = new List<Student>();
    static string fileName = "students.txt";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nУправление студентами");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Удалить студента");
            Console.WriteLine("3. Редактировать студента");
            Console.WriteLine("4. Показать всех студентов");
            Console.WriteLine("5. Найти студента");
            Console.WriteLine("6. Сортировать студентов");
            Console.WriteLine("7. Сохранить список в файл");
            Console.WriteLine("8. Загрузить список из файла");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1": AddStudent(); break;
                case "2": DeleteStudent(); break;
                case "3": EditStudent(); break;
                case "4": ShowStudents(); break;
                case "5": SearchStudent(); break;
                case "6": SortStudents(); break;
                case "7": SaveStudents(); break;
                case "8": LoadStudents(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор."); break;
            }
        }
    }

    static void AddStudent()
    {
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();

        Console.Write("Введите возраст: ");
        if (!int.TryParse(Console.ReadLine(), out int age) || age < 1)
        {
            Console.WriteLine("Некорректный возраст.");
            return;
        }

        Console.Write("Введите группу: ");
        string group = Console.ReadLine();

        Console.Write("Введите средний балл (0–10): ");
        if (!double.TryParse(Console.ReadLine(), out double grade) ||
            grade < 0 || grade > 10)
        {
            Console.WriteLine("Некорректный балл.");
            return;
        }

        students.Add(new Student(name, age, group, grade));
        Console.WriteLine("Студент добавлен.");
    }

    static void DeleteStudent()
    {
        Console.Write("Введите имя студента для удаления: ");
        string name = Console.ReadLine();

        Student student = students.FirstOrDefault(s =>
            s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine("Студент удалён.");
        }
        else
            Console.WriteLine("Студент не найден.");
    }

    static void EditStudent()
    {
        Console.Write("Введите имя студента для редактирования: ");
        string name = Console.ReadLine();

        Student student = students.FirstOrDefault(s =>
            s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (student == null)
        {
            Console.WriteLine("Студент не найден.");
            return;
        }

        Console.Write("Новое имя: ");
        student.Name = Console.ReadLine();

        Console.Write("Новый возраст: ");
        if (!int.TryParse(Console.ReadLine(), out int age) || age < 1)
        {
            Console.WriteLine("Некорректный возраст.");
            return;
        }
        student.Age = age;

        Console.Write("Новая группа: ");
        student.Group = Console.ReadLine();

        Console.Write("Новый средний балл (0–10): ");
        if (!double.TryParse(Console.ReadLine(), out double grade) ||
            grade < 0 || grade > 10)
        {
            Console.WriteLine("Некорректный балл.");
            return;
        }
        student.Grade = grade;

        Console.WriteLine("Данные студента обновлены.");
    }

    static void ShowStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Список студентов пуст.");
            return;
        }

        foreach (Student student in students)
            Console.WriteLine(student);
    }

    static void SearchStudent()
    {
        Console.Write("Введите имя или его часть: ");
        string name = Console.ReadLine();

        var result = students.Where(s =>
            s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

        if (result.Count == 0)
            Console.WriteLine("Студенты не найдены.");
        else
            foreach (Student student in result)
                Console.WriteLine(student);
    }

    static void SortStudents()
    {
        Console.WriteLine("1. По имени");
        Console.WriteLine("2. По возрасту");
        Console.WriteLine("3. По среднему баллу");
        Console.Write("Выберите критерий: ");

        switch (Console.ReadLine())
        {
            case "1":
                students = students.OrderBy(s => s.Name).ToList();
                break;
            case "2":
                students = students.OrderBy(s => s.Age).ToList();
                break;
            case "3":
                students = students.OrderByDescending(s => s.Grade).ToList();
                break;
            default:
                Console.WriteLine("Неверный критерий.");
                return;
        }

        Console.WriteLine("Список отсортирован.");
        ShowStudents();
    }

    static void SaveStudents()
    {
        File.WriteAllLines(fileName, students.Select(s =>
            $"{s.Name}|{s.Age}|{s.Group}|{s.Grade}"));

        Console.WriteLine("Список сохранён в файл students.txt.");
    }

    static void LoadStudents()
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл students.txt не найден.");
            return;
        }

        students.Clear();

        foreach (string line in File.ReadAllLines(fileName))
        {
            string[] data = line.Split('|');

            if (data.Length == 4 &&
                int.TryParse(data[1], out int age) &&
                double.TryParse(data[3], out double grade))
            {
                students.Add(new Student(data[0], age, data[2], grade));
            }
        }

        Console.WriteLine("Список студентов загружен.");
    }
}