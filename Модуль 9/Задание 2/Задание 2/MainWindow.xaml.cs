using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Задание_2;

namespace Задание_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                Database.Initialize();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка запуска:\n" + ex.Message);
            }
        }

        private SqliteConnection Connect()
        {
            var con = new SqliteConnection(Database.ConnectionString);
            con.Open();

            using var cmd = new SqliteCommand("PRAGMA foreign_keys=ON;", con);
            cmd.ExecuteNonQuery();

            return con;
        }
        private void Run(string sql, params (string, object)[] values)
        {
            using var con = Connect();
            using var cmd = new SqliteCommand(sql, con);

            foreach (var v in values)
                cmd.Parameters.AddWithValue(v.Item1, v.Item2 ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }
        private DataTable GetData(string sql)
        {
            using var con = Connect();
            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            var table = new DataTable();
            table.Load(reader);
            return table;
        }
        private void LoadData()
        {
            try
            {
                ProjectsGrid.ItemsSource =
                    GetData("SELECT * FROM Projects").DefaultView;
                EmployeesGrid.ItemsSource =
                    GetData("SELECT * FROM Employees").DefaultView;
                TasksGrid.ItemsSource =
                    GetData("SELECT * FROM Tasks").DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить данные:\n" + ex.Message);
            }
        }

        private void SafeRun(string sql, params (string, object)[] values)
        {
            try
            {
                Run(sql, values);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось выполнить операцию:\n" + ex.Message);
            }
        }

        private int Id(DataGrid grid)
        {
            if (grid.SelectedItem is DataRowView row)
                return Convert.ToInt32(row["Id"]);

            MessageBox.Show("Сначала выбери запись в таблице.");
            return 0;
        }
        private void AddProject(object s, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProjectName.Text))
            {
                MessageBox.Show("Введи название проекта.");
                return;
            }

            SafeRun("INSERT INTO Projects (Name, Description) VALUES (@n, @d)",
                ("@n", ProjectName.Text),
                ("@d", ProjectDescription.Text));
        }

        private void EditProject(object s, RoutedEventArgs e)
        {
            int id = Id(ProjectsGrid);
            if (id == 0) return;

            SafeRun("UPDATE Projects SET Name=@n, Description=@d WHERE Id=@id",
                ("@n", ProjectName.Text),
                ("@d", ProjectDescription.Text), ("@id", id));
        }

        private void DeleteProject(object s, RoutedEventArgs e)
        {
            int id = Id(ProjectsGrid);
            if (id == 0) return;

            SafeRun("DELETE FROM Projects WHERE Id=@id", ("@id", id));
        }

        private void ProjectsGrid_SelectionChanged(object s, SelectionChangedEventArgs e)
        {
            if (ProjectsGrid.SelectedItem is DataRowView r)
            {
                ProjectName.Text = r["Name"].ToString();
                ProjectDescription.Text = r["Description"].ToString();
            }
        }

        private void AddEmployee(object s, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmployeeName.Text))
            {
                MessageBox.Show("Введи ФИО сотрудника.");
                return;
            }

            SafeRun("INSERT INTO Employees (FullName, Position) VALUES (@n, @p)",
                ("@n", EmployeeName.Text), ("@p", EmployeePosition.Text));
        }

        private void EditEmployee(object s, RoutedEventArgs e)
        {
            int id = Id(EmployeesGrid);
            if (id == 0) return;

            SafeRun("UPDATE Employees SET FullName=@n, Position=@p WHERE Id=@id",
                ("@n", EmployeeName.Text), ("@p", EmployeePosition.Text),
                ("@id", id));
        }

        private void DeleteEmployee(object s, RoutedEventArgs e)
        {
            int id = Id(EmployeesGrid);
            if (id == 0) return;

            SafeRun("DELETE FROM Employees WHERE Id=@id", ("@id", id));
        }

        private void EmployeesGrid_SelectionChanged(object s, SelectionChangedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is DataRowView r)
            {
                EmployeeName.Text = r["FullName"].ToString();
                EmployeePosition.Text = r["Position"].ToString();
            }
        }

        private void AddTask(object s, RoutedEventArgs e) => SaveTask(false);
        private void EditTask(object s, RoutedEventArgs e) => SaveTask(true);

        private void SaveTask(bool edit)
        {
            if (string.IsNullOrWhiteSpace(TaskName.Text))
            {
                MessageBox.Show("Введи название задачи.");
                return;
            }

            if (!int.TryParse(TaskProgress.Text, out int progress) ||
                progress < 0 || progress > 100)
            {
                MessageBox.Show("Прогресс должен быть от 0 до 100.");
                return;
            }

            object project = DBNull.Value;
            object employee = DBNull.Value;

            if (TaskProjectId.Text.Trim() != "")
            {
                if (!int.TryParse(TaskProjectId.Text, out int p) || p <= 0)
                {
                    MessageBox.Show("Введи правильный ID проекта.");
                    return;
                }
                project = p;
            }

            if (TaskEmployeeId.Text.Trim() != "")
            {
                if (!int.TryParse(TaskEmployeeId.Text, out int emp) || emp <= 0)
                {
                    MessageBox.Show("Введи правильный ID сотрудника.");
                    return;
                }
                employee = emp;
            }

            if (edit && Id(TasksGrid) == 0) return;

            string status = (TaskStatus.SelectedItem as ComboBoxItem)
                ?.Content.ToString() ?? "Новая";

            if (status == "Завершена") progress = 100;

            string sql = edit
                ? "UPDATE Tasks SET Name=@n, ProjectId=@p, EmployeeId=@e, Status=@s, Progress=@pr WHERE Id=@id"
                : "INSERT INTO Tasks (Name, ProjectId, EmployeeId, Status, Progress) VALUES (@n, @p, @e, @s, @pr)";

            if (edit)
                SafeRun(sql, ("@n", TaskName.Text), ("@p", project),
                    ("@e", employee), ("@s", status), ("@pr", progress),
                    ("@id", Id(TasksGrid)));
            else
                SafeRun(sql, ("@n", TaskName.Text), ("@p", project),
                    ("@e", employee), ("@s", status), ("@pr", progress));
        }

        private void DeleteTask(object s, RoutedEventArgs e)
        {
            int id = Id(TasksGrid);
            if (id == 0) return;

            SafeRun("DELETE FROM Tasks WHERE Id=@id", ("@id", id));
        }

        private void TasksGrid_SelectionChanged(object s, SelectionChangedEventArgs e)
        {
            if (TasksGrid.SelectedItem is DataRowView r)
            {
                TaskName.Text = r["Name"].ToString();
                TaskProjectId.Text = r["ProjectId"] == DBNull.Value
                    ? "" : r["ProjectId"].ToString();
                TaskEmployeeId.Text = r["EmployeeId"] == DBNull.Value
                    ? "" : r["EmployeeId"].ToString();
                TaskProgress.Text = r["Progress"].ToString();

                foreach (ComboBoxItem item in TaskStatus.Items)
                    if (item.Content.ToString() == r["Status"].ToString())
                        TaskStatus.SelectedItem = item;
            }
        }
        private void CreateReport(object s, RoutedEventArgs e)
        {
            try
            {
                var report = new StringBuilder("ОТЧЁТ О ПРОЕКТАХ\n\n");

                using var con = Connect();
                string sql = @"SELECT p.Name, COUNT(t.Id) AS Total,
                    SUM(CASE WHEN t.Status='Завершена' THEN 1 ELSE 0 END) AS Done,
                    COALESCE(AVG(t.Progress), 0) AS Progress
                    FROM Projects p LEFT JOIN Tasks t ON p.Id=t.ProjectId
                    GROUP BY p.Id, p.Name";

                using var cmd = new SqliteCommand(sql, con);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    report.AppendLine("Проект: " + reader["Name"]);
                    report.AppendLine("Всего задач: " + reader["Total"]);
                    report.AppendLine("Завершено: " + reader["Done"]);
                    report.AppendLine("Выполнение: " +
                        Math.Round(Convert.ToDouble(reader["Progress"])) + "%");
                    report.AppendLine();
                }

                ReportText.Text = report.ToString();

                var dialog = new SaveFileDialog
                {
                    Filter = "Текстовый файл (*.txt)|*.txt",
                    FileName = "Отчёт.txt"
                };

                if (dialog.ShowDialog() == true)
                    File.WriteAllText(dialog.FileName, report.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка создания отчёта:\n" + ex.Message);
            }
        }
    }
}