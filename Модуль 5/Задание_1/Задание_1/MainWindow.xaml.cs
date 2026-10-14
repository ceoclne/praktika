
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Задание_1
{
    public partial class MainWindow : Window
    {
        private Point startPoint;
        private Point lastPoint;
        private bool isDrawing = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private string GetSelectedTool()
        {
            ComboBoxItem item = comboBox1.SelectedItem as ComboBoxItem;
            return item?.Content?.ToString() ?? "Свободно";
        }

        private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                startPoint = e.GetPosition(DrawingCanvas);
                lastPoint = startPoint;
                isDrawing = true;
                DrawingCanvas.CaptureMouse();
            }
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing)
                return;

            if (GetSelectedTool() == "Свободно")
            {
                Point currentPoint = e.GetPosition(DrawingCanvas);

                Line line = new Line
                {
                    X1 = lastPoint.X,
                    Y1 = lastPoint.Y,
                    X2 = currentPoint.X,
                    Y2 = currentPoint.Y,
                    Stroke = Brushes.Blue,
                    StrokeThickness = 3,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };

                DrawingCanvas.Children.Add(line);
                lastPoint = currentPoint;
            }
        }

        private void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!isDrawing)
                return;

            Point endPoint = e.GetPosition(DrawingCanvas);

            double x = Math.Min(startPoint.X, endPoint.X);
            double y = Math.Min(startPoint.Y, endPoint.Y);
            double width = Math.Abs(endPoint.X - startPoint.X);
            double height = Math.Abs(endPoint.Y - startPoint.Y);

            string tool = GetSelectedTool();

            Shape shape = null;

            switch (tool)
            {
                case "Линия":
                    shape = new Line
                    {
                        X1 = startPoint.X,
                        Y1 = startPoint.Y,
                        X2 = endPoint.X,
                        Y2 = endPoint.Y
                    };
                    break;

                case "Круг":
                    shape = new Ellipse
                    {
                        Width = width,
                        Height = height
                    };

                    Canvas.SetLeft(shape, x);
                    Canvas.SetTop(shape, y);
                    break;

                case "Квадрат":
                    double size = Math.Min(width, height);

                    shape = new Rectangle
                    {
                        Width = size,
                        Height = size
                    };

                    Canvas.SetLeft(shape, x);
                    Canvas.SetTop(shape, y);
                    break;
            }

            if (shape != null)
            {
                shape.Stroke = Brushes.Blue;
                shape.StrokeThickness = 3;
                DrawingCanvas.Children.Add(shape);
            }

            isDrawing = false;
            DrawingCanvas.ReleaseMouseCapture();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
        }
    }
}