using System;
using System.Threading.Tasks;
using System.Windows;

namespace GuildWars2APIWpfApp
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create and show the splash screen
            var splashScreen = new SplashScreenWindow();
            splashScreen.Show();

            // Perform initialization tasks asynchronously
            await InitializeApp();

            // Close the splash screen and open the main window
            splashScreen.Close();
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private async Task InitializeApp()
        {
            // Perform initialization tasks here (e.g., loading resources, setting up services)
            await Task.Delay(2000); // Simulate initialization delay
        }
    }
}
