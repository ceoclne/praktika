
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Задание_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database.Initialize();
            LoadTasks();
        }

        private void LoadTasks()
        {
            TasksGrid.ItemsSource = Database.GetTasks().DefaultView;
        }

        private string GetStatus()
        {
            return (cmbStatus.SelectedItem as ComboBoxItem)?.Content?.ToString()
                   ?? "Новая";
        }

        private bool CheckTitle()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название задачи.");
                return false;
            }

            return true;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckTitle())
                return;

            Database.AddTask(
                txtTitle.Text.Trim(),
                txtDescription.Text.Trim(),
                GetStatus());

            LoadTasks();
            ClearFields();
            txtStatus.Text = "Задача добавлена.";
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (TasksGrid.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите задачу в таблице.");
                return;
            }

            if (!CheckTitle())
                return;

            int id = Convert.ToInt32(row["Код задачи"]);

            Database.UpdateTask(
                id,
                txtTitle.Text.Trim(),
                txtDescription.Text.Trim(),
                GetStatus());

            LoadTasks();
            ClearFields();
            txtStatus.Text = "Задача обновлена.";
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (TasksGrid.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите задачу в таблице.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Удалить выбранную задачу?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            int id = Convert.ToInt32(row["Код задачи"]);

            Database.DeleteTask(id);
            LoadTasks();
            ClearFields();

            txtStatus.Text = "Задача удалена.";
        }

        private void TasksGrid_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (TasksGrid.SelectedItem is DataRowView row)
            {
                txtTitle.Text = row["Название"].ToString();
                txtDescription.Text = row["Описание"].ToString();

                string status = row["Статус"].ToString();

                foreach (ComboBoxItem item in cmbStatus.Items)
                {
                    if (item.Content.ToString() == status)
                    {
                        cmbStatus.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            cmbStatus.SelectedIndex = 0;
            TasksGrid.SelectedItem = null;
            txtStatus.Text = "Поля очищены.";
        }
    }
}