
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Задание_3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database.Initialize();
            LoadBooks();
        }

        private void LoadBooks(string search = "")
        {
            BooksGrid.ItemsSource = Database.GetBooks(search).DefaultView;
        }

        private bool CheckBookData(out string title, out string author,
            out int year)
        {
            title = txtTitle.Text.Trim();
            author = txtAuthor.Text.Trim();
            year = 0;

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(author))
            {
                MessageBox.Show("Введите название книги и автора.");
                return false;
            }

            if (!int.TryParse(txtYear.Text, out year) ||
                year < 1 || year > DateTime.Now.Year)
            {
                MessageBox.Show("Введите корректный год издания.");
                return false;
            }

            return true;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBookData(out string title, out string author,
    out int year))
            {
                Database.AddBook(title, author, year);

                LoadBooks(txtSearch.Text.Trim());
                ClearFields();

                txtStatus.Text = "Книга добавлена.";
            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks(txtSearch.Text.Trim());
            txtStatus.Text = "Поиск выполнен.";
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            LoadBooks();

            txtStatus.Text = "Отображаются все книги.";
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (BooksGrid.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите книгу в таблице.");
                return;
            }

            int id = Convert.ToInt32(row["Код книги"]);

            if (MessageBox.Show("Удалить выбранную книгу?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            Database.DeleteBook(id);
            LoadBooks(txtSearch.Text.Trim());
            ClearFields();

            txtStatus.Text = "Книга удалена.";
        }

        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
            if (BooksGrid.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите книгу в таблице.");
                return;
            }

            int id = Convert.ToInt32(row["Код книги"]);

            if (Database.BorrowBook(id))
                txtStatus.Text = "Книга выдана.";
            else
                MessageBox.Show("Эта книга уже выдана.");

            LoadBooks(txtSearch.Text.Trim());
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            if (BooksGrid.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите книгу в таблице.");
                return;
            }

            int id = Convert.ToInt32(row["Код книги"]);

            if (Database.ReturnBook(id))
                txtStatus.Text = "Книга возвращена.";
            else
                MessageBox.Show("Эта книга уже доступна.");

            LoadBooks(txtSearch.Text.Trim());
        }

        private void BooksGrid_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (BooksGrid.SelectedItem is DataRowView row)
            {
                txtTitle.Text = row["Название"].ToString();
                txtAuthor.Text = row["Автор"].ToString();
                txtYear.Text = row["Год издания"].ToString();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtYear.Clear();

            BooksGrid.SelectedItem = null;
            txtStatus.Text = "Поля очищены.";
        }
    }
}