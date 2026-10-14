using System.Windows;
namespace Задание_3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string task = txtTask.Text.Trim();

            if (task != "")
            {
                lstTasks.Items.Add("[ ] " + task);
                txtTask.Clear();
            }
            else
            {
                MessageBox.Show("Введите задачу!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedIndex != -1)
            {
                lstTasks.Items.RemoveAt(lstTasks.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления!");
            }
        }

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            int index = lstTasks.SelectedIndex;

            if (index != -1)
            {
                string task = lstTasks.Items[index].ToString();

                if (task.StartsWith("[ ] "))
                {
                    lstTasks.Items[index] = "[V] " + task.Substring(4);
                }
                else
                {
                    MessageBox.Show("Задача уже выполнена!");
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу!");
            }
        }
    }
}
