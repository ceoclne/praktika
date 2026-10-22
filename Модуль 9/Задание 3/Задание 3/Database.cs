using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using Задание_3;
namespace Задание_3
{
    public class Database
    {
        private static readonly string dbPath = @"D:\Documents\GitHub\praktika\Модуль 9\Задание 3\Tasks.db";           

        private static readonly string connectionString = $"Data Source={dbPath}";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT,
                    Priority TEXT,
                    DueDate TEXT,
                    Progress INTEGER
                )";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static List<TaskItem> GetTasks()
        {
            var tasks = new List<TaskItem>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = "SELECT * FROM Tasks";

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                tasks.Add(new TaskItem
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Priority = reader.IsDBNull(3) ? "Средний" : reader.GetString(3),
                    DueDate = DateTime.Parse(reader.GetString(4)),
                    Progress = reader.GetInt32(5)
                });
            }

            return tasks;
        }

        public static void AddTask(TaskItem task)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"INSERT INTO Tasks
                (Title, Description, Priority, DueDate, Progress)
                VALUES (@title, @description, @priority, @date, @progress)";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@title", task.Title);
            command.Parameters.AddWithValue("@description", task.Description);
            command.Parameters.AddWithValue("@priority", task.Priority);
            command.Parameters.AddWithValue("@date", task.DueDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@progress", task.Progress);

            command.ExecuteNonQuery();
        }

        public static void UpdateTask(TaskItem task)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"UPDATE Tasks SET
                Title=@title, Description=@description,
                Priority=@priority, DueDate=@date,
                Progress=@progress WHERE Id=@id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@title", task.Title);
            command.Parameters.AddWithValue("@description", task.Description);
            command.Parameters.AddWithValue("@priority", task.Priority);
            command.Parameters.AddWithValue("@date", task.DueDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@progress", task.Progress);
            command.Parameters.AddWithValue("@id", task.Id);

            command.ExecuteNonQuery();
        }

        public static void DeleteTask(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(
                "DELETE FROM Tasks WHERE Id=@id", connection);

            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
