using ArduinoController.Utilities;
using ArduinoController.Utilities.SettingsControls;
using System;
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
            TextBoxBaudRate.Text = Convert.ToString(Settings.Current.BaudRate);
            TextBoxSliderValueFrequencyUpdate.Text = Convert.ToString(Settings.Current.SliderValueUpdateFrequency);

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
            try
            {
                Settings.Current.BaudRate = Convert.ToInt32(TextBoxBaudRate.Text);
                if (Settings.Current.BaudRate < 0 || Settings.Current.BaudRate > 115200)
                {
                    MessageBox.Show("Неприемлимое значение Baud-частоты!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неприемлимое значение Baud-частоты!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Settings.Current.SliderValueUpdateFrequency = Convert.ToInt32(TextBoxSliderValueFrequencyUpdate.Text);
                if (Settings.Current.SliderValueUpdateFrequency < 200 || Settings.Current.SliderValueUpdateFrequency > 5000)
                {
                    MessageBox.Show("Некорректное значение!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректное значение!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
    }
}
