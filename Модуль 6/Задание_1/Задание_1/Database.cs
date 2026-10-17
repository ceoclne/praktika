
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace Задание_1
{
    public class Database
    {
        private static readonly string dbPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employees.db");

        private static readonly string connectionString =
            $"Data Source={dbPath}";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS [Сотрудники] (
                    [Код сотрудника] INTEGER PRIMARY KEY AUTOINCREMENT,
                    [ФИО] TEXT NOT NULL,
                    [Должность] TEXT NOT NULL,
                    [Отдел] TEXT,
                    [Зарплата] REAL NOT NULL
                );";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static DataTable GetEmployees()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(
                "SELECT * FROM [Сотрудники] ORDER BY [Код сотрудника]",
                connection);

            using var reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            return table;
        }

        public static void AddEmployee(
            string fullName, string position, string department, double salary)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"INSERT INTO [Сотрудники]
                ([ФИО], [Должность], [Отдел], [Зарплата])
                VALUES ($name, $position, $department, $salary)";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$name", fullName);
            command.Parameters.AddWithValue("$position", position);
            command.Parameters.AddWithValue("$department", department);
            command.Parameters.AddWithValue("$salary", salary);
            command.ExecuteNonQuery();
        }

        public static void UpdateEmployee(
            int id, string fullName, string position,
            string department, double salary)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"UPDATE [Сотрудники]
                SET [ФИО]=$name,
                    [Должность]=$position,
                    [Отдел]=$department,
                    [Зарплата]=$salary
                WHERE [Код сотрудника]=$id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$name", fullName);
            command.Parameters.AddWithValue("$position", position);
            command.Parameters.AddWithValue("$department", department);
            command.Parameters.AddWithValue("$salary", salary);
            command.ExecuteNonQuery();
        }

        public static void DeleteEmployee(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(
                "DELETE FROM [Сотрудники] WHERE [Код сотрудника]=$id",
                connection);

            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }
    }
}