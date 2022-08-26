using ArduinoController.Utilities;
using ArduinoController.Utilities.SettingsControls;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows;

namespace ArduinoController.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private string[] _portNames = SerialPort.GetPortNames();

        private DebugWindow? _debugWindow;

        public SettingsWindow()
        {
            InitializeComponent();
            ComboBoxSelectedCOMPort.ItemsSource = _portNames;

            for (int i = 0; i < _portNames.Length; i++)
            {
                if (_portNames[i] == Settings.Current.SelectedCOMPort)
                {
                    ComboBoxSelectedCOMPort.SelectedIndex = i;
                }
            }
        }

        private void ButtonOpenConfigFolder_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", ProjectSaving.ConfigFolderPath);
        }

        private void ButtonApplySettings_Click(object sender, RoutedEventArgs e)
        {
            Settings.Current.SelectedCOMPort = _portNames[ComboBoxSelectedCOMPort.SelectedIndex];
            Settings.SaveSettings();
            Close();
        }

        private void ButtonRefreshCOMPorts_Click(object sender, RoutedEventArgs e)
        {
            _portNames = SerialPort.GetPortNames();
        }

        private void ButtonOpenDebugWindow_Click(object sender, RoutedEventArgs e)
        {
            if (_debugWindow == null)
                _debugWindow = new DebugWindow();
            _debugWindow.Show();
        }

        //private
    }
}
