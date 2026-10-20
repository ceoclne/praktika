using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Note
{
    public partial class MainWindow : Window
    {
        string file = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Editor.Document.Blocks.Clear();
            Editor.Document.Blocks.Add(new Paragraph());
            file = "";
            StatusText.Text = "Новый документ";
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Все документы|*.txt;*.rtf|Текст|*.txt|RTF|*.rtf";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    TextRange range = new TextRange(
                        Editor.Document.ContentStart,
                        Editor.Document.ContentEnd);

                    using (FileStream stream = File.OpenRead(dialog.FileName))
                    {
                        range.Load(stream,
                            Path.GetExtension(dialog.FileName).ToLower() == ".rtf"
                            ? DataFormats.Rtf : DataFormats.Text);
                    }

                    file = dialog.FileName;
                    StatusText.Text = "Файл открыт";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка открытия: " + ex.Message);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (file == "")
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "RTF документ|*.rtf|Текстовый файл|*.txt";
                dialog.DefaultExt = ".rtf";

                if (dialog.ShowDialog() != true)
                    return;

                file = dialog.FileName;
            }

            try
            {
                TextRange range = new TextRange(
                    Editor.Document.ContentStart,
                    Editor.Document.ContentEnd);

                using (FileStream stream = File.Create(file))
                {
                    range.Save(stream,
                        Path.GetExtension(file).ToLower() == ".rtf"
                        ? DataFormats.Rtf : DataFormats.Text);
                }

                StatusText.Text = "Документ сохранён";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleBold.Execute(null, Editor);
            Editor.Focus();
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleItalic.Execute(null, Editor);
            Editor.Focus();
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleUnderline.Execute(null, Editor);
            Editor.Focus();
        }
        private void FontFamily_Changed(object sender,
            SelectionChangedEventArgs e)
        {
            if (Editor == null ||
                FontFamilyBox.SelectedItem is not ComboBoxItem item)
                return;

            Editor.Selection.ApplyPropertyValue(
                TextElement.FontFamilyProperty,
                new System.Windows.Media.FontFamily(item.Content.ToString()));

            Editor.Focus();
        }
        private void FontSize_Changed(object sender,
            SelectionChangedEventArgs e)
        {
            if (Editor == null ||
                FontSizeBox.SelectedItem is not ComboBoxItem item)
                return;

            if (double.TryParse(item.Content.ToString(), out double size))
            {
                Editor.Selection.ApplyPropertyValue(
                    TextElement.FontSizeProperty, size);
                Editor.Focus();
            }
        }
    }
}