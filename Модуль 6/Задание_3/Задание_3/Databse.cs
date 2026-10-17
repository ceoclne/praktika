
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace Задание_3
{
    public class Database
    {
        private static readonly string dbPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library.db");

        private static readonly string connectionString =
            $"Data Source={dbPath}";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS [Книги] (
                    [Код книги] INTEGER PRIMARY KEY AUTOINCREMENT,
                    [Название] TEXT NOT NULL,
                    [Автор] TEXT NOT NULL,
                    [Год издания] INTEGER,
                    [Статус] TEXT NOT NULL DEFAULT 'Доступна'
                );";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static DataTable GetBooks(string search = "")
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"SELECT * FROM [Книги]
                WHERE [Название] LIKE $search
                   OR [Автор] LIKE $search
                ORDER BY [Код книги]";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$search", "%" + search + "%");

            using var reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            return table;
        }

        public static void AddBook(string title, string author, int year)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"INSERT INTO [Книги]
                ([Название], [Автор], [Год издания], [Статус])
                VALUES ($title, $author, $year, 'Доступна')";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$title", title);
            command.Parameters.AddWithValue("$author", author);
            command.Parameters.AddWithValue("$year", year);
            command.ExecuteNonQuery();
        }

        public static void DeleteBook(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(
                "DELETE FROM [Книги] WHERE [Код книги]=$id", connection);

            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }

        public static bool BorrowBook(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"UPDATE [Книги]
                SET [Статус]='Выдана'
                WHERE [Код книги]=$id AND [Статус]='Доступна'";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public static bool ReturnBook(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"UPDATE [Книги]
                SET [Статус]='Доступна'
                WHERE [Код книги]=$id AND [Статус]='Выдана'";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$id", id);

            return command.ExecuteNonQuery() > 0;
        }
    }
}