using System;
using System.Collections.Generic;

// Класс автора
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

// Класс книги
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
        Console.WriteLine($"Год издания: {Year}");
    }
}

// Класс библиотеки
class Library
{
    private List<Book> books = new List<Book>();

    // Добавление книги
    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"Книга \"{book.Title}\" добавлена.");
    }

    // Удаление книги
    public void RemoveBook(Book book)
    {
        books.Remove(book);
        Console.WriteLine($"Книга \"{book.Title}\" удалена.");
    }

    // Поиск книг по автору
    public void FindByAuthor(string authorName)
    {
        Console.WriteLine($"\nКниги автора {authorName}:");

        foreach (Book book in books)
        {
            if (book.Author.GetFullName() == authorName)
            {
                book.ShowInfo();
                Console.WriteLine();
            }
        }
    }

    // Поиск книг по году издания
    public void FindByYear(int year)
    {
        Console.WriteLine($"\nКниги {year} года:");

        foreach (Book book in books)
        {
            if (book.Year == year)
            {
                book.ShowInfo();
                Console.WriteLine();
            }
        }
    }

    // Вывод всех книг
    public void ShowAllBooks()
    {
        Console.WriteLine("\nВсе книги библиотеки:");

        foreach (Book book in books)
        {
            book.ShowInfo();
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        // Создаем авторов
        Author author1 = new Author("Лев", "Толстой", 1828);
        Author author2 = new Author("Фёдор", "Достоевский", 1821);

        // Создаем книги
        Book book1 = new Book("Война и мир", author1, 1869);
        Book book2 = new Book("Анна Каренина", author1, 1877);
        Book book3 = new Book("Преступление и наказание", author2, 1866);

        // Создаем библиотеку
        Library library = new Library();

        // Добавляем книги
        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);

        // Выводим все книги
        library.ShowAllBooks();

        // Поиск по автору
        library.FindByAuthor("Лев Толстой");

        // Поиск по году издания
        library.FindByYear(1866);

        // Удаляем книгу
        library.RemoveBook(book2);

        // Показываем оставшиеся книги
        library.ShowAllBooks();

        Console.ReadKey();
    }
}