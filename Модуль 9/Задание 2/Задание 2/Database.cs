using Microsoft.Data.Sqlite;

namespace Задание_2
{
    public static class Database
    {
        public static string ConnectionString = "Data Source=project.db";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string sql = @"
                PRAGMA foreign_keys = ON;

                CREATE TABLE IF NOT EXISTS Projects (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT
                );

                CREATE TABLE IF NOT EXISTS Employees (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT NOT NULL,
                    Position TEXT
                );

                CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    ProjectId INTEGER,
                    EmployeeId INTEGER,
                    Status TEXT DEFAULT 'Новая',
                    Progress INTEGER DEFAULT 0,
                    FOREIGN KEY (ProjectId) REFERENCES Projects(Id)
                        ON DELETE SET NULL,
                    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
                        ON DELETE SET NULL
                );";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
