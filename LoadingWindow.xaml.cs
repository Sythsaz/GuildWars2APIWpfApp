using System;
using System.Windows;

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
    }
}
