using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BloodDonationSystem.UI
{
    public partial class CustomMessageWindow : Window
    {
        public CustomMessageWindow(string title, string message, AlertType type)
        {
            InitializeComponent();
            TitleText.Text = title;
            MessageText.Text = message;

            // Gán icon, màu viền và màu tiêu đề theo loại thông báo
            switch (type)
            {
                case AlertType.Success:
                    IconText.Text = "✅";
                    TitleText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#388E3C")); // Green
                    MainBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C8E6C9"));
                    break;

                case AlertType.Error:
                    IconText.Text = "❌";
                    TitleText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D32F2F")); // Red
                    MainBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCDD2"));
                    break;

                case AlertType.Warning:
                    IconText.Text = "⚠️";
                    TitleText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F57C00")); // Orange
                    MainBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0B2"));
                    break;

                case AlertType.Info:
                    IconText.Text = "ℹ️";
                    TitleText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1976D2")); // Blue
                    MainBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BBDEFB"));
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            this.BeginAnimation(Window.OpacityProperty, fadeIn);

            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2.0)
            };

            timer.Tick += (s, args) =>
            {
                timer.Stop();
                this.Close();
            };

            timer.Start();
        }
    }
}
