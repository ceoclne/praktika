
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using System.Windows.Media.Animation;

namespace Задание_4
{
    public class Database
    {
        private static readonly string dbPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "finance.db");

        private static readonly string connectionString =
            $"Data Source={dbPath}";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS [Финансы] (
                    [Код записи] INTEGER PRIMARY KEY AUTOINCREMENT,
                    [Тип] TEXT NOT NULL,
                    [Категория] TEXT NOT NULL,
                    [Сумма] REAL NOT NULL,
                    [Дата] TEXT NOT NULL,
                    [Описание] TEXT
                );";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static DataTable GetRecords()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(
                "SELECT * FROM [Финансы] ORDER BY [Дата] DESC, [Код записи] DESC",
                connection);

            using var reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            return table;
        }

        public static void AddRecord(
            string type, string category, double amount,
            string date, string description)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"INSERT INTO [Финансы]
                ([Тип], [Категория], [Сумма], [Дата], [Описание])
                VALUES ($type, $category, $amount, $date, $description)";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$type", type);
            command.Parameters.AddWithValue("$category", category);
            command.Parameters.AddWithValue("$amount", amount);
            command.Parameters.AddWithValue("$date", date);
            command.Parameters.AddWithValue("$description", description);

            command.ExecuteNonQuery();
        }

        public static void UpdateRecord(
            int id, string type, string category, double amount,
            string date, string description)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"UPDATE [Финансы]
                SET [Тип]=$type,
                    [Категория]=$category,
                    [Сумма]=$amount,
                    [Дата]=$date,
                    [Описание]=$description
                WHERE [Код записи]=$id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$type", type);
            command.Parameters.AddWithValue("$category", category);
            command.Parameters.AddWithValue("$amount", amount);
            command.Parameters.AddWithValue("$date", date);
            command.Parameters.AddWithValue("$description", description);

            command.ExecuteNonQuery();
        }

        public static void DeleteRecord(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = new SqliteCommand(
                "DELETE FROM [Финансы] WHERE [Код записи]=$id",
                connection);

            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }
        
        public static (double Income, double Expense) GetTotals()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"
                SELECT
                    COALESCE(SUM(CASE WHEN [Тип]='Доход'
                        THEN [Сумма] ELSE 0 END), 0),
                    COALESCE(SUM(CASE WHEN [Тип]='Расход'
                        THEN [Сумма] ELSE 0 END), 0)
                FROM [Финансы]";

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            reader.Read();

            return (reader.GetDouble(0), reader.GetDouble(1));
        }
    }
}