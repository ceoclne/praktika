
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Задание_5
{
    public partial class MainWindow : Window
    {
        private double firstNumber = 0;
        private string operation = "";
        private bool newNumber = true;

        public MainWindow()
        {
            InitializeComponent();
            txtDisplay.Text = "0";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string value = button.Content.ToString().Trim();

            if (value == "C")
            {
                txtDisplay.Text = "0";
                firstNumber = 0;
                operation = "";
                newNumber = true;
                return;
            }

            if (value.Length == 1 && char.IsDigit(value[0]))
            {
                if (newNumber || txtDisplay.Text == "0")
                    txtDisplay.Text = value;
                else
                    txtDisplay.Text += value;

                newNumber = false;
            }
            else if (value == ",")
            {
                if (newNumber)
                {
                    txtDisplay.Text = "0,";
                    newNumber = false;
                }
                else if (!txtDisplay.Text.Contains(","))
                {
                    txtDisplay.Text += ",";
                }
            }
            else if (value == "+" || value == "−" ||
                     value == "-" || value == "×" ||
                     value == "*" || value == "÷" ||
                     value == "/")
            {
                if (!double.TryParse(
                    txtDisplay.Text.Replace(',', '.'),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out firstNumber))
                {
                    return;
                }

                operation = value;
                newNumber = true;
            }
            else if (value == "=")
            {
                if (operation == "")
                    return;

                if (!double.TryParse(
                    txtDisplay.Text.Replace(',', '.'),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double secondNumber))
                {
                    return;
                }

                double result;

                switch (operation)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;

                    case "−":
                    case "-":
                        result = firstNumber - secondNumber;
                        break;

                    case "×":
                    case "*":
                        result = firstNumber * secondNumber;
                        break;

                    case "÷":
                    case "/":
                        if (secondNumber == 0)
                        {
                            MessageBox.Show(
                                "На ноль делить нельзя!",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            return;
                        }

                        result = firstNumber / secondNumber;
                        break;

                    default:
                        return;
                }

                txtDisplay.Text = result.ToString(
                    "G", CultureInfo.GetCultureInfo("ru-RU"));

                operation = "";
                newNumber = true;
            }
        }
    }
}