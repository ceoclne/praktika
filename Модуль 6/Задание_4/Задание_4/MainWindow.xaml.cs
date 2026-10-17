
using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Задание_4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Database.Initialize();
            dpDate.SelectedDate = DateTime.Today;

            LoadRecords();
            UpdateTotals();
        }

        private void LoadRecords()
        {
            FinanceGrid.ItemsSource = Database.GetRecords().DefaultView;
        }

        private string GetTypeValue()
        {
            return (cmbType.SelectedItem as ComboBoxItem)?.Content?.ToString()
                   ?? "Доход";
        }

        private bool CheckData(
            out string type, out string category,
            out double amount, out string date, out string description)
        {
            type = GetTypeValue();
            category = txtCategory.Text.Trim();
            description = txtDescription.Text.Trim();
            date = "";
            amount = 0;

            if (string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Введите категорию операции.");
                return false;
            }

            bool validAmount = double.TryParse(txtAmount.Text, out amount);

            if (!validAmount || double.IsNaN(amount) ||
                double.IsInfinity(amount) || amount <= 0)
            {
                MessageBox.Show("Введите корректную сумму больше нуля.");
                return false;
            }

            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату операции.");
                return false;
            }

            date = dpDate.SelectedDate.Value.ToString("yyyy-MM-dd");

            return true;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckData(out string type, out string category,
                out double amount, out string date, out string description))
                return;

            Database.AddRecord(type, category, amount, date, description);

            LoadRecords();
            UpdateTotals();
            ClearFields();

            txtStatus.Text = "Запись добавлена.";
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (FinanceGrid.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите запись в таблице.");
                return;
            }

            if (CheckData(out string type, out string category,
                out double amount, out string date, out string description))
                return;

            int id = Convert.ToInt32(row["Код записи"]);

            Database.UpdateRecord(id, type, category,
                amount, date, description);

            LoadRecords();
            UpdateTotals();
            ClearFields();

            txtStatus.Text = "Запись обновлена.";
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (FinanceGrid.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите запись в таблице.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Удалить выбранную запись?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            int id = Convert.ToInt32(row["Код записи"]);

            Database.DeleteRecord(id);

            LoadRecords();
            UpdateTotals();
            ClearFields();

            txtStatus.Text = "Запись удалена.";
        }

        private void FinanceGrid_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (FinanceGrid.SelectedItem is DataRowView row)
            {
                txtCategory.Text = row["Категория"].ToString();
                txtAmount.Text = Convert.ToDouble(row["Сумма"]).ToString();
                string type = row["Тип"].ToString();

                foreach (ComboBoxItem item in cmbType.Items)
                {
                    if (item.Content.ToString() == type)
                    {
                        cmbType.SelectedItem = item;
                        break;
                    }
                }

                if (DateTime.TryParse(row["Дата"].ToString(), out DateTime date))
                {
                    dpDate.SelectedDate = date;
                }
            }
        }

        private void UpdateTotals()
        {
            var totals = Database.GetTotals();

            txtIncome.Text = totals.Income.ToString("N2");
            txtExpense.Text = totals.Expense.ToString("N2");
            txtBalance.Text = (totals.Income - totals.Expense).ToString("N2");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtCategory.Clear();
            txtAmount.Clear();
            txtDescription.Clear();

            cmbType.SelectedIndex = 0;
            dpDate.SelectedDate = DateTime.Today;

            FinanceGrid.SelectedItem = null;
            txtStatus.Text = "Поля очищены.";
        }
    }
}