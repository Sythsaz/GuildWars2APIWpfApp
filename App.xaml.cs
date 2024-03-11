using System.Threading.Tasks; // Add using directive for Task
using System.Windows;

namespace GuildWars2APIWpfApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Show splash screen
            var splashScreen = new SplashScreenWindow();
            splashScreen.Show();

            // Perform initialization tasks asynchronously
            Task.Run(async () =>
            {
                try
                {
                    await InitializeApp();
                }
                catch (Exception ex)
                {
                    // Handle initialization errors
                    MessageBox.Show($"An error occurred during initialization: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Close splash screen and show main window
                splashScreen.Dispatcher.Invoke(() =>
                {
                    splashScreen.Close();
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                });
            });
        }

        private async Task InitializeApp()
        {
            // Perform initialization tasks here
            await Task.Delay(2000); // Simulate initialization delay

        }
    }
}
