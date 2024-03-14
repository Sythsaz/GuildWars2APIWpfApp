using System;
using System.Windows;

namespace GuildWars2APIWpfApp
{
    public partial class LoadingWindow : Window
    {
        private readonly string url = "https://api.guildwars2.com/v2/tokeninfo";
        private readonly HttpClient httpClient = new HttpClient();
        private bool apiKeyValidated = false;

        public LoadingWindow()
        {
            InitializeComponent();
        }

        private async void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            string apiKey = apiKeyTextBox.Text;

            // Perform initialization tasks asynchronously (e.g., fetching data)
            apiKeyValidated = await TestAPIKey(apiKey);

            if (apiKeyValidated)
            {
                // Close the loading screen and transition to the main window
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid API key. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> TestAPIKey(string apiKey)
        {
            try
            {
                // Add API key to request headers
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                // Send GET request to the /v2/tokeninfo endpoint
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // If the API key is valid, return true
                    return true;
                }
                else
                {
                    // If the API key is invalid, return false
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Close the application if the API key validation was not successful
            if (!apiKeyValidated)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
