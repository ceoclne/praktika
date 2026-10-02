using System;

class Author
{
    public string Name;
    public int BirthYear;

    public Author(string name, int birthYear)
    {
        Name = name;
        BirthYear = birthYear;
    }
}

class Book
{
    public string Title;
    public int Year;
    public Author Author;

    public Book(string title, int year, Author author)
    {
        Title = title;
        Year = year;
        Author = author;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Название: {Title}");
        Console.WriteLine($"Год выпуска: {Year}");
        Console.WriteLine($"Автор: {Author.Name}, {Author.BirthYear} года рождения.");
    }
}

class Program
{
    static void Main()
    {
        Author author1 = new Author("Александр Пушкин", 1799);
        Author author2 = new Author("Лев Толстой", 1828);

        Book book1 = new Book("Евгений Онегин", 1833, author1);
        Book book2 = new Book("Война и мир", 1869, author2);

        Console.WriteLine("Книга 1:");
        book1.ShowInfo();

        Console.WriteLine("\nКнига 2:");
        book2.ShowInfo();
    }
}