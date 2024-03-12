using System;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using Timer = System.Timers.Timer;


namespace GuildWars2APIWpfApp
{
    public partial class App : Application
    {
        private SplashScreenWindow? splashScreen;
        private Timer? timer;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create and show the splash screen
            splashScreen = new SplashScreenWindow();
            splashScreen.Show();

            // Start a timer to close the splash screen after a certain duration
            timer = new Timer();
            timer.Interval = 3000; // 3 seconds (adjust as needed)
            timer.Elapsed += TimerElapsed;
            timer.AutoReset = false; // Only fire once
            timer.Start();

            // Perform initialization tasks asynchronously
            await InitializeApp();

            // Open the main window
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private async Task InitializeApp()
        {
            // Perform initialization tasks here (e.g., loading resources, setting up services)
            await Task.Delay(1000); // Simulate initialization delay
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            // Close the splash screen when the timer elapses
            splashScreen?.Dispatcher.Invoke(() =>
            {
                splashScreen.Close();
            });
        }

    }
}
