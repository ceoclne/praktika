using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace курсач
{
    public partial class Hashtable : Form
    {
        private HashTable table = new HashTable();
        bool stepMode = false;
        int step = 0;
        string currentKey;
        string currentValue;
        public Hashtable()
        {
            InitializeComponent();
            this.Load += Form2_Load;
            this.Text = "Анализ хэш-таблиц с цепочками";
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private bool IsInputValid()
        {
            return !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrWhiteSpace(textBox2.Text);
        }
        private void UpdateStatistics()
        {
            label1.Text = "Элементов: " + table.CountElements();
            label2.Text = "Корзин: " + table.CountBuckets();
            label3.Text = "Коллизий: " + table.Collisions;
            label4.Text = "Макс. цепочка: " + table.MaxChain();
            label5.Text = "Средняя цепочка: " + table.AverageChain().ToString("0.00");
            label6.Text = "Коэффициент загрузки: " + table.LoadFactor().ToString("0.00");
            label7.Text = "Операций: " + table.Operations;
        }

        private void DrawTable()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var bucket in table.GetBuckets())
            {
                GroupBox group = new GroupBox();
                group.Text = "Корзина " + bucket.Key;
                group.Width = 180;
                group.Height = 170;
                ListBox list = new ListBox();
                list.Dock = DockStyle.Fill;
                foreach (HashNode node in bucket.Value)
                {
                    list.Items.Add(node.Key + " → " + node.Value);
                }
                group.Controls.Add(list);
                flowLayoutPanel1.Controls.Add(group);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsInputValid())
            {
                MessageBox.Show("Введите корректные ключ и значение.");
                return;
            }
            if (stepMode)
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Введите ключ и значение.");
                    return;
                }

                currentKey = textBox1.Text;
                currentValue = textBox2.Text;
                step = 0;
                richTextBox1.Clear();
                richTextBox1.AppendText("Обучение началось.\n\n");
                richTextBox1.AppendText("Нажмите кнопку \"Следующий шаг\".\n");
                return;
            }
            if (textBox1.Text == "" || textBox2.Text == "")
                return;
            table.Add(textBox1.Text, textBox2.Text);
            richTextBox1.Clear();
            richTextBox1.AppendText("Шаг 1: ключ = " + textBox1.Text + "\n");
            richTextBox1.AppendText("Шаг 2: hash = " + textBox1.Text.GetHashCode() + "\n");
            richTextBox1.AppendText("Шаг 3: корзина определена\n");
            richTextBox1.AppendText("Шаг 4: добавлено\n");
            listBox1.Items.Add("Добавлен " + textBox1.Text);
            DrawTable();
            UpdateStatistics();
            textBox1.Clear();
            textBox2.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите ключ для поиска.");
                return;
            }
            var node = table.Find(textBox1.Text);
            richTextBox1.Clear();
            if (node == null)
                richTextBox1.Text = "Не найдено";
            else
                richTextBox1.Text = $"Найдено:\n{node.Key} -> {node.Value}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите ключ для удаления.");
                return;
            }
            if (table.Remove(textBox1.Text))
            {
                listBox1.Items.Add("Удалён " + textBox1.Text);
                DrawTable();
                UpdateStatistics();
                richTextBox1.Text = "Удалено";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!IsInputValid())
            {
                MessageBox.Show("Введите ключ и новое значение.");
                return;
            }
            if (table.Update(textBox1.Text, textBox2.Text))
            {
                listBox1.Items.Add("Изменён " + textBox1.Text);
                DrawTable();
                richTextBox1.Text = "Обновлено";
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            table.Clear();
            flowLayoutPanel1.Controls.Clear();
            listBox1.Items.Clear();
            richTextBox1.Clear();
            UpdateStatistics();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!stepMode)
            {
                MessageBox.Show("Сначала выберите обучающий режим.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите ключ и значение.");
                return;
            }
            currentKey = textBox1.Text;
            currentValue = textBox2.Text;
            step = 0;
            richTextBox1.Clear();
            richTextBox1.AppendText("Обучение начато.\n\n");
            richTextBox1.AppendText("Нажмите кнопку \"Следующий шаг\".\n");
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (!stepMode)
                return;
            int hash = currentKey.GetHashCode();
            int bucket = table.GetBucket(currentKey);
           switch (step)
            {
                case 0:
                    richTextBox1.AppendText(
                        "\nШаг 1.\n" +
                        "Получен ключ: \"" + currentKey + "\".\n" +
                        "Теперь необходимо вычислить его хэш-код.\n");
                    break;

                case 1:
                    richTextBox1.AppendText(
                        "\nШаг 2.\n" +
                        "Вычисляем хэш-код ключа.\n\n" +
                        "HashCode = " + hash + "\n");
                    break;

                case 2:
                    richTextBox1.AppendText(
                        "\nШаг 3.\n" +
                        "Определяем номер корзины по формуле:\n\n" +
                        "Номер корзины = |HashCode| % Количество корзин\n\n" +
                        "|" + hash + "| % " + table.CountBuckets() +
                        " = " + bucket + "\n\n" +
                        "Следовательно, элемент попадёт в корзину №" + bucket + ".\n");
                    break;

                case 3:
                    var buckets = table.GetBuckets();

                    if (buckets.ContainsKey(bucket))
                    {
                        richTextBox1.AppendText(
                            "\nШаг 4.\n" +
                            "Корзина №" + bucket + " уже содержит элементы.\n" +
                            "Возникает коллизия.\n" +
                            "Элемент будет добавлен в конец цепочки.\n");
                    }
                    else
                    {
                        richTextBox1.AppendText(
                            "\nШаг 4.\n" +
                            "Корзина №" + bucket + " пуста.\n" +
                            "Коллизии не возникает.\n");
                    }

                    table.Add(currentKey, currentValue);

                    DrawTable();
                    UpdateStatistics();

                    break;

                case 4:
                    richTextBox1.AppendText(
                        "\nШаг 5.\n" +
                        "Элемент успешно добавлен в хэш-таблицу.\n\n" +
                        "Обучение завершено.");

                    textBox1.Clear();
                    textBox2.Clear();

                    step = -1;       // чтобы после step++ стало 0
                    stepMode = false;
                    break;
            }

            step++;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "Добавление элемента в хэш-таблицу");
            toolTip1.SetToolTip(button2, "Поиск элемента по ключу");
            toolTip1.SetToolTip(button3, "Удаление элемента");
            toolTip1.SetToolTip(button4, "Обновление значения");
            toolTip1.SetToolTip(button5, "Очистка таблицы");
            toolTip1.SetToolTip(button6, "Следующий шаг обучения");
            toolTip1.SetToolTip(button11, "Запуск режима обучения");
            toolTip1.SetToolTip(button7, "Сохранить таблицу");
            toolTip1.SetToolTip(button8, "Загрузить таблицу");
        }
        private void button9_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt|*.txt";
            save.FileName = "report.txt";

            if (save.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter w = new StreamWriter(save.FileName))
                {
                    w.WriteLine("ОТЧЕТ");

                    foreach (var b in table.GetBuckets())
                    {
                        w.WriteLine("Корзина " + b.Key);
                        foreach (HashNode n in b.Value)
                            w.WriteLine(n.Key + " -> " + n.Value);
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "txt|*.txt";

            if (open.ShowDialog() == DialogResult.OK)
            {
                table.Clear();

                foreach (var line in File.ReadAllLines(open.FileName))
                {
                    var p = line.Split(';');
                    if (p.Length == 2)
                        table.Add(p[0], p[1]);
                }

                DrawTable();
                UpdateStatistics();
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt|*.txt";

            if (save.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(save.FileName))
                {
                    foreach (var bucket in table.GetBuckets())
                    {
                        foreach (HashNode node in bucket.Value)
                        {
                            writer.WriteLine(node.Key + ";" + node.Value);
                        }
                    }
                }
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton2.Checked)
                return;
            stepMode = false;
            richTextBox1.Clear();
            richTextBox1.AppendText(
                "БЫСТРЫЙ РЕЖИМ\n\n" +
                "Все операции выполняются сразу без пошаговых объяснений.\n" +
                "Введите данные и используйте кнопки управления.");
        }
        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked)
                return;
            stepMode = true;
            step = 0;
            textBox1.Clear();
            textBox2.Clear();

            richTextBox1.AppendText(
                "ОБУЧАЮЩИЙ РЕЖИМ\n\n" +
                "В этом режиме программа показывает, как работает добавление элемента в хэш-таблицу.\n\n" +
                "1. Введите ключ.\n" +
                "2. Введите значение.\n" +
                "3. Нажмите кнопку \"Начать обучение\".\n" +
                "4. Затем нажимайте кнопку \"Следующий шаг\".\n");
        }
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button10_Click_1(object sender, EventArgs e)
        {
            Random rnd = new Random();

            for (int i = 0; i < 20; i++)
            {
                string key = rnd.Next(1000, 9999).ToString();
                string value = "Значение_" + rnd.Next(100, 999);
                table.Add(key, value);
                listBox1.Items.Add("Добавлен: " + key + " → " + value);
            }
            DrawTable();
            UpdateStatistics();
            richTextBox1.Text = "Сгенерировано 20 случайных элементов.";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Hashtable_Load(object sender, EventArgs e)
        {

        }
    }
}