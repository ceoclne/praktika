using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Задание_3;

namespace Задание_3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database.Initialize();
            dpDueDate.SelectedDate = DateTime.Today;
            LoadTasks();
            CheckDeadlines();
        }
        private void LoadTasks()
        {
            dgTasks.ItemsSource = Database.GetTasks();
            int count = Database.GetTasks().Count;
            txtStatus.Text = "Всего задач: " + count;
        }
        private TaskItem GetTaskFromFields()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название задачи.");
                return null;
            }
            if (dpDueDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату выполнения.");
                return null;
            }
            string priority =
                (cmbPriority.SelectedItem as ComboBoxItem)?.Content.ToString()
                ?? "Средний";

            string progressText =
                (cmbProgress.SelectedItem as ComboBoxItem)?.Content.ToString()
                ?? "0%";

            return new TaskItem
            {
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                Priority = priority,
                DueDate = dpDueDate.SelectedDate.Value,
                Progress = int.Parse(progressText.Replace("%", ""))
            };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            TaskItem task = GetTaskFromFields();
            if (task == null)
                return;
            Database.AddTask(task);
            LoadTasks();
            ClearFields();
            txtStatus.Text = "Задача добавлена.";
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgTasks.SelectedItem is not TaskItem selectedTask)
            {
                MessageBox.Show("Выберите задачу для изменения.");
                return;
            }

            TaskItem task = GetTaskFromFields();
            if (task == null)
                return;
            task.Id = selectedTask.Id;
            Database.UpdateTask(task);
            LoadTasks();
            ClearFields();
            txtStatus.Text = "Изменения сохранены.";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgTasks.SelectedItem is not TaskItem task)
            {
                MessageBox.Show("Выберите задачу для удаления.");
                return;
            }
            MessageBoxResult result = MessageBox.Show(
                "Удалить выбранную задачу?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Database.DeleteTask(task.Id);
                LoadTasks();
                ClearFields();

                txtStatus.Text = "Задача удалена.";
            }
        }
        private void dgTasks_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (dgTasks.SelectedItem is not TaskItem task)
                return;
            txtTitle.Text = task.Title;
            txtDescription.Text = task.Description;
            dpDueDate.SelectedDate = task.DueDate;
            foreach (ComboBoxItem item in cmbPriority.Items)
            {
                if (item.Content.ToString() == task.Priority)
                {
                    cmbPriority.SelectedItem = item;
                    break;
                }
            }
            foreach (ComboBoxItem item in cmbProgress.Items)
            {
                if (item.Content.ToString() == task.Progress + "%")
                {
                    cmbProgress.SelectedItem = item;
                    break;
                }
            }
        }
        private void ClearFields()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            cmbPriority.SelectedIndex = 1;
            cmbProgress.SelectedIndex = 0;
            dpDueDate.SelectedDate = DateTime.Today;
            dgTasks.SelectedItem = null;
        }
        private void CheckDeadlines()
        {
            var tasks = Database.GetTasks();

            var overdue = tasks
                .Where(t => t.Progress < 100 &&
                            t.DueDate.Date < DateTime.Today).ToList();

            var upcoming = tasks
                .Where(t => t.Progress < 100 &&
                            t.DueDate.Date >= DateTime.Today &&
                            t.DueDate.Date <= DateTime.Today.AddDays(1)).ToList();
            string message = "";
            if (overdue.Count > 0)
            {
                message += "Просроченные задачи:\n";

                foreach (var task in overdue)
                    message += "• " + task.Title + "\n";
            }
            if (upcoming.Count > 0)
            {
                message += "\nСкоро срок выполнения:\n";

                foreach (var task in upcoming)
                    message += "• " + task.Title + " — " +
                               task.DueDate.ToString("dd.MM.yyyy") + "\n";
            }
            if (message != "")
            {
                MessageBox.Show(
                    message,
                    "Напоминание о задачах",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}
