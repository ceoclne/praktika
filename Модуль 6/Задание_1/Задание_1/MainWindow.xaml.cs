
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Задание_1;

namespace Задание_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database.Initialize();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            EmployeesGrid.ItemsSource = Database.GetEmployees().DefaultView;
        }

        private bool GetFormData(
            out string fullName,
            out string position,
            out string department,
            out double salary)
        {
            fullName = txtFullName.Text.Trim();
            position = txtPosition.Text.Trim();
            department = txtDepartment.Text.Trim();

            salary = 0;

            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(position))
            {
                MessageBox.Show("Введите ФИО и должность сотрудника.");
                return false;
            }

            bool validSalary = double.TryParse(txtSalary.Text, out salary);

            if (validSalary || double.IsNaN(salary) ||
                double.IsInfinity(salary) || salary < 0)
            {
                MessageBox.Show("Введите корректную зарплату.");
                return false;
            }
            return true;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (GetFormData(out string name, out string position,
                out string department, out double salary))
                return;

            Database.AddEmployee(name, position, department, salary);
            LoadEmployees();
            ClearFields();
            txtStatus.Text = "Сотрудник добавлен.";
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is not
                System.Data.DataRowView row)
            {
                MessageBox.Show("Выберите сотрудника в таблице.");
                return;
            }

            if (GetFormData(out string name, out string position,
                out string department, out double salary))
                return;

            int id = Convert.ToInt32(row["Код сотрудника"]);

            Database.UpdateEmployee(id, name, position,
                department, salary);

            LoadEmployees();
            ClearFields();
            txtStatus.Text = "Данные сотрудника изменены.";
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is not System.Data.DataRowView row)
            {
                MessageBox.Show("Выберите сотрудника в таблице.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Удалить выбранного сотрудника?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            int id = Convert.ToInt32(row["Код сотрудника"]);

            Database.DeleteEmployee(id);

            LoadEmployees();
            ClearFields();
            txtStatus.Text = "Сотрудник удалён.";
        }

        private void EmployeesGrid_SelectionChanged(
    object sender, SelectionChangedEventArgs e)
{
    if (EmployeesGrid.SelectedItem is System.Data.DataRowView row)
    {
        txtFullName.Text = row["ФИО"].ToString();
        txtPosition.Text = row["Должность"].ToString();

        txtDepartment.Text = row["Отдел"] == DBNull.Value
            ? ""
            : row["Отдел"].ToString();

        txtSalary.Text = row["Зарплата"].ToString();
    }
}

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtFullName.Clear();
            txtPosition.Clear();
            txtDepartment.Clear();
            txtSalary.Clear();

            EmployeesGrid.SelectedItem = null;
            txtStatus.Text = "Поля очищены.";
        }
    }
}