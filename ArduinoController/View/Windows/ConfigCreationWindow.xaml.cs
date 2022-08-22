using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        }

        private void TextBoxDigitalPorts_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
