using System;
using System.Windows;
using System.Windows.Data;

namespace GuildWars2APIWpfApp
{
    public partial class LoadingWindow : Window
    {
        // Define an event to signal when the OK button is pressed
        public event EventHandler? OkButtonClicked;

        public string ApiKey => apiKeyTextBox.Text;

        public LoadingWindow()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            // Raise the event when the OK button is clicked
            OkButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        #region Title Bar
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Window Visibility Converter
        /// <summary>
        /// Converter to convert window size to visibility state
        /// </summary>
        public class WindowSizeToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                double windowWidth = (double)value;
                bool inverse = false;

                if (parameter != null && parameter.ToString() == "Inverse")
                {
                    inverse = true;
                }

                if (windowWidth < 600)
                {
                    return inverse ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    return inverse ? Visibility.Collapsed : Visibility.Visible;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
