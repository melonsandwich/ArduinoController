using ArduinoController.Utilities;
using System.Diagnostics;
using System.Windows;

namespace ArduinoController.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void ButtonOpenConfigFolder_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", ProjectSaving.ConfigFolderPath);
        }
    }
}
