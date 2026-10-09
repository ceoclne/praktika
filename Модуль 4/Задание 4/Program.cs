using System;

interface IBook
{
    bool IsAvailable();
    void GiveBook();
}

class Novel : IBook
{
    public string Title { get; set; }
    private bool available = true;

    public Novel(string title)
    {
        Title = title;
    }

    public bool IsAvailable()
    {
        return available;
    }

    public void GiveBook()
    {
        if (available)
        {
            available = false;
            Console.WriteLine($"Книга \"{Title}\" успешно выдана.");
        }
        else
        {
            Console.WriteLine($"Книга \"{Title}\" уже выдана.");
        }
    }
}

class Textbook : IBook
{
    public string Title { get; set; }
    private bool available = true;

    public Textbook(string title)
    {
        Title = title;
    }

    public bool IsAvailable()
    {
        return available;
    }

    public void GiveBook()
    {
        if (available)
        {
            available = false;
            Console.WriteLine($"Учебник \"{Title}\" успешно выдан.");
        }
        else
        {
            Console.WriteLine($"Учебник \"{Title}\" уже выдан.");
        }
    }
}

class Encyclopedia : IBook
{
    public string Title { get; set; }
    private bool available = true;

    public Encyclopedia(string title)
    {
        Title = title;
    }

    public bool IsAvailable()
    {
        return available;
    }

    public void GiveBook()
    {
        if (available)
        {
            available = false;
            Console.WriteLine($"Энциклопедия \"{Title}\" успешно выдана.");
        }
        else
        {
            Console.WriteLine($"Энциклопедия \"{Title}\" уже выдана.");
        }
    }
}

class Program
{
    static void Main()
    {
        Novel novel = new Novel("Мастер и Маргарита");
        Textbook textbook = new Textbook("Программирование на C#");
        Encyclopedia encyclopedia = new Encyclopedia("Большая энциклопедия");

        while (true)
        {
            Console.Clear();

            Console.WriteLine("Библиотека");
            Console.WriteLine();
            Console.WriteLine("Список книг:");
            Console.WriteLine($"1. {novel.Title}");
            Console.WriteLine($"2. {textbook.Title}");
            Console.WriteLine($"3. {encyclopedia.Title}");

            Console.WriteLine();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Проверить доступность книги");
            Console.WriteLine("2 - Выдать книгу");
            Console.WriteLine("0 - Выход");

            Console.Write("\nВаш выбор: ");
            string choice = Console.ReadLine();

            if (choice == "0")
                break;

            if (choice != "1" && choice != "2")
            {
                Console.WriteLine("Неверный выбор.");
                Console.ReadKey();
                continue;
            }

            Console.WriteLine();
            Console.WriteLine("Выберите книгу:");
            Console.WriteLine($"1 - {novel.Title}");
            Console.WriteLine($"2 - {textbook.Title}");
            Console.WriteLine($"3 - {encyclopedia.Title}");

            Console.Write("\nВаш выбор: ");
            string bookChoice = Console.ReadLine();

            IBook selectedBook = null;
            string selectedTitle = "";

            if (bookChoice == "1")
            {
                selectedBook = novel;
                selectedTitle = novel.Title;
            }
            else if (bookChoice == "2")
            {
                selectedBook = textbook;
                selectedTitle = textbook.Title;
            }
            else if (bookChoice == "3")
            {
                selectedBook = encyclopedia;
                selectedTitle = encyclopedia.Title;
            }
            else
            {
                Console.WriteLine("Такой книги нет.");
                Console.ReadKey();
                continue;
            }

            Console.WriteLine();

            if (choice == "1")
            {
                if (selectedBook.IsAvailable())
                    Console.WriteLine($"Книга \"{selectedTitle}\" доступна.");
                else
                    Console.WriteLine($"Книга \"{selectedTitle}\" уже выдана.");
            }
            else if (choice == "2")
            {
                selectedBook.GiveBook();
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}