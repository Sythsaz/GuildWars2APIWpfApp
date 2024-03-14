using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace GuildWars2APIWpfApp
{
    public partial class App : Application
    {
        private LoadingWindow? loadingScreen;
        private bool isMainWindowOpen = false;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create and show the loading screen
            loadingScreen = new LoadingWindow();
            loadingScreen.OkButtonClicked += LoadingScreen_OkButtonClicked; // Subscribe to the event
            loadingScreen.Show();
        }

        private async void LoadingScreen_OkButtonClicked(object? sender, EventArgs e)
        {
            if (loadingScreen != null)
            {
                // Obtain the API key from the loading screen
                string apiKey = loadingScreen.ApiKey;

                // Perform initialization tasks asynchronously
                bool apiKeyValidated = await InitializeApp(apiKey);

                if (apiKeyValidated)
                {
                    // Open the main window
                    MainWindow mainWindow = new();
                    mainWindow.Closed += MainWindow_Closed; // Handle the Closed event

                    // Show the main window asynchronously
                    mainWindow.Show();

                    // Keep the loading screen open until the main window is fully loaded
                    mainWindow.ContentRendered += (s, _) =>
                    {
                        // Close the loading screen when the main window is fully loaded
                        loadingScreen?.Close();
                    };
                }
                else
                {
                    // Exit the application if the API key is not validated
                    Shutdown();
                }
            }
        }


        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            // Set the flag to indicate that the main window is closed
            isMainWindowOpen = false;
        }


        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            // Shutdown the application
            Application.Current.Shutdown();
        }



        private static async Task<bool> InitializeApp(string apiKey)
        {
            try
            {
                string url = "https://api.guildwars2.com/v2/tokeninfo";
                HttpClient httpClient = new();
                bool apiKeyValidated = false;
                // Add API key to request headers
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                // Send GET request to the /v2/tokeninfo endpoint
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("API Verify Success");
                    // If the API key is valid, return true
                    return true;
                }
                else
                {
                    Debug.WriteLine("API Verify Failure");
                    // If the API key is invalid, return false
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show($"An error occurred during initialization: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
    }
}
