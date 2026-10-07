using System;
using System.Collections.Generic;

class Author
{
    public string FirstName;
    public string LastName;
    public int BirthYear;

    public Author(string firstName, string lastName, int birthYear)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthYear = birthYear;
    }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}

class Book
{
    public string Title;
    public Author Author;
    public int Year;

    public Book(string title, Author author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Название: {Title}");
        Console.WriteLine($"Автор: {Author.GetFullName()}");
        Console.WriteLine($"Год рождения автора: {Author.BirthYear}");
        Console.WriteLine($"Год издания: {Year}");
    }
}

class Library
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine("Книга добавлена.");
    }

    public void RemoveBook(int number)
    {
        if (number >= 0 && number < books.Count)
        {
            Console.WriteLine($"Книга \"{books[number].Title}\" удалена.");
            books.RemoveAt(number);
        }
        else
        {
            Console.WriteLine("Книга с таким номером не найдена.");
        }
    }

    public void FindByAuthor(string authorName)
    {
        bool found = false;

        foreach (Book book in books)
        {
            if (book.Author.GetFullName().Equals(
                authorName, StringComparison.OrdinalIgnoreCase))
            {
                book.ShowInfo();
                Console.WriteLine();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Книги этого автора не найдены.");
        }
    }

    public void FindByYear(int year)
    {
        bool found = false;

        foreach (Book book in books)
        {
            if (book.Year == year)
            {
                book.ShowInfo();
                Console.WriteLine();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Книги этого года не найдены.");
        }
    }

    public void ShowAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Библиотека пуста.");
            return;
        }

        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"\nКнига №{i + 1}");
            books[i].ShowInfo();
        }
    }

    public int GetBookCount()
    {
        return books.Count;
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();

        Author author1 = new Author("Александр", "Пушкин", 1799);
        Author author2 = new Author("Михаил", "Булгаков", 1891);

        Book book1 = new Book("Евгений Онегин", author1, 1833);
        Book book2 = new Book("Мастер и Маргарита", author2, 1967);

        library.AddBook(book1);
        library.AddBook(book2);

        while (true)
        {
            Console.WriteLine("\n===== БИБЛИОТЕКА =====");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Удалить книгу");
            Console.WriteLine("3. Найти книги по автору");
            Console.WriteLine("4. Найти книги по году");
            Console.WriteLine("5. Показать все книги");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            Console.Clear();

            if (choice == "1")
            {
                Console.Write("Введите название книги: ");
                string title = Console.ReadLine();

                Console.Write("Введите имя автора: ");
                string firstName = Console.ReadLine();

                Console.Write("Введите фамилию автора: ");
                string lastName = Console.ReadLine();

                Console.Write("Введите год рождения автора: ");
                int birthYear = int.Parse(Console.ReadLine());

                Console.Write("Введите год издания книги: ");
                int year = int.Parse(Console.ReadLine());

                Author author = new Author(firstName, lastName, birthYear);
                Book book = new Book(title, author, year);

                library.AddBook(book);
            }
            else if (choice == "2")
            {
                if (library.GetBookCount() == 0)
                {
                    Console.WriteLine("Библиотека пуста.");
                    continue;
                }

                library.ShowAllBooks();

                Console.Write("\nВведите номер книги для удаления: ");
                int number = int.Parse(Console.ReadLine());

                library.RemoveBook(number - 1);
            }
            else if (choice == "3")
            {
                Console.Write("Введите имя и фамилию автора: ");
                string authorName = Console.ReadLine();

                Console.WriteLine($"\nКниги автора {authorName}:");
                library.FindByAuthor(authorName);
            }
            else if (choice == "4")
            {
                Console.Write("Введите год издания: ");
                int year = int.Parse(Console.ReadLine());

                Console.WriteLine($"\nКниги {year} года:");
                library.FindByYear(year);
            }
            else if (choice == "5")
            {
                Console.WriteLine("Все книги библиотеки:");
                library.ShowAllBooks();
            }
            else if (choice == "6")
            {
                break;
            }
            else
            {
                Console.WriteLine("Неверный пункт меню.");
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
