using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Net.WebRequestMethods;
using System.Windows.Data;

namespace GuildWars2APIWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constructor
        /// <summary>
        /// Main Window Initializer
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        private readonly string? apiKey;

        public MainWindow(string apiKey)
        {
            InitializeComponent();
            this.apiKey = apiKey;
        }


        #region Perform Request to API

        private void ApiRequest(int type, int? item)
        {

        }

        #endregion

    }

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
}
