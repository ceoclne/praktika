
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace Задание_2
{
    public class Database
    {
        private static readonly string dbPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.db");

        private static readonly string connectionString =
            $"Data Source={dbPath}";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS [Задачи] (
                    [Код задачи] INTEGER PRIMARY KEY AUTOINCREMENT,
                    [Название] TEXT NOT NULL,
                    [Описание] TEXT,
                    [Статус] TEXT NOT NULL
                );";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static DataTable GetTasks()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(
                "SELECT * FROM [Задачи] ORDER BY [Код задачи]",
                connection);

            using var reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            return table;
        }

        public static void AddTask(
            string title, string description, string status)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"INSERT INTO [Задачи]
                ([Название], [Описание], [Статус])
                VALUES ($title, $description, $status)";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$title", title);
            command.Parameters.AddWithValue("$description", description);
            command.Parameters.AddWithValue("$status", status);
            command.ExecuteNonQuery();
        }

        public static void UpdateTask(
            int id, string title, string description, string status)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"UPDATE [Задачи]
                SET [Название]=$title,
                    [Описание]=$description,
                    [Статус]=$status
                WHERE [Код задачи]=$id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$title", title);
            command.Parameters.AddWithValue("$description", description);
            command.Parameters.AddWithValue("$status", status);
            command.ExecuteNonQuery();
        }

        public static void DeleteTask(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(
                "DELETE FROM [Задачи] WHERE [Код задачи]=$id",
                connection);

            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }
    }
}