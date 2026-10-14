using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Задание_1
{
    public partial class MainWindow : Window
    {
        private BitmapImage currentImage;
        private double zoom = 1.0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter =
                "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (dialog.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(dialog.FileName);
                image.EndInit();
                image.Freeze();

                currentImage = image;

                ImageViewer.Source = currentImage;
                zoom = 1.0;
                UpdateZoom();
            }
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage != null && zoom < 3.0)
            {
                zoom = Math.Min(3.0, zoom + 0.1);
                UpdateZoom();
            }
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage != null && zoom > 0.2)
            {
                zoom = Math.Max(0.2, zoom - 0.1);
                UpdateZoom();
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage != null)
            {
                zoom = 1.0;
                UpdateZoom();
            }
        }

        private void UpdateZoom()
        {
            lblZoom.Text = "Масштаб: " +
                (int)Math.Round(zoom * 100) + "%";

            if (currentImage == null)
                return;

            ImageViewer.Width = currentImage.PixelWidth * zoom;
            ImageViewer.Height = currentImage.PixelHeight * zoom;
        }
    }
}
