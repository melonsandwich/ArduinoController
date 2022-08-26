using ArduinoController.Utilities;
using ArduinoController.Utilities.Configuration;
using ArduinoController.View.Pages;
using System;
using System.Windows;
using System.Windows.Input;

namespace ArduinoController.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ConfigCreationWindow.xaml
    /// </summary>
    public partial class ConfigCreationWindow : Window
    {
        private Window _owner;

        public ConfigCreationWindow(Window owner)
        {
            InitializeComponent();
            _owner = owner;
        }

        private void ButtonCreateConfig_Click(object sender, RoutedEventArgs e)
        {
            Config config = ProjectSaving.CreateConfig(TextBoxConfigName.Text, Convert.ToInt32(TextBoxAnalogPorts.Text), Convert.ToInt32(TextBoxDigitalPorts.Text));
            if (_owner is MainWindow window)
            {
                window.MainFrame.Content = new ConfigPage(config);
                Close();
            }    
        }

        private void TextBoxDigitalPorts_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
