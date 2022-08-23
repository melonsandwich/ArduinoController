using ArduinoController.Utilities;
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
            ProjectSaving.CreateConfig(TextBoxConfigName.Text, Convert.ToInt32(TextBoxAnalogPorts.Text), Convert.ToInt32(TextBoxDigitalPorts.Text));
        }

        private void TextBoxDigitalPorts_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
