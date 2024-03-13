using System;
using System.Threading.Tasks;
using System.Windows;

namespace GuildWars2APIWpfApp
{
    public partial class App : Application
    {
        #region Startup
        /// <summary>
        /// Show spash screen, load req deps, close spash screen, open main window
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create and show the splash screen
            var splashScreen = new SplashScreenWindow();
            splashScreen.Show();

            // Perform initialization tasks asynchronously
            await InitializeApp();

            // Open the main window
            var mainWindow = new MainWindow();

            // Handle the Loaded event of the main window
            mainWindow.Loaded += (sender, args) =>
            {
                // Close the splash screen when the main window is loaded
                splashScreen.Close();
            };

            // Show the main window
            mainWindow.Show();
        }

        #region

        #region Load Req Deps
        /// <summary>
        /// Load required info to work with the api
        /// </summary>
        /// <returns></returns>
        private async Task InitializeApp()
        {
            // Perform initialization tasks here (e.g., loading resources, setting up services)
            await Task.Delay(10000); // Simulate initialization delay
        }

        #endregion
    }
}