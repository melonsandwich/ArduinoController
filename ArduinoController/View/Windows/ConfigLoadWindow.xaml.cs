using ArduinoController.Utilities;
using ArduinoController.Utilities.Configuration;
using ArduinoController.View.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ConfigLoadWindow.xaml
    /// </summary>
    public partial class ConfigLoadWindow : Window
    {
        private MainWindow _owner;

        private List<string> _configNames = new List<string>();

        public ConfigLoadWindow(MainWindow owner)
        {
            InitializeComponent();

            _owner = owner;
            ProjectSaving.ScanConfigs();

            /*for (int i = 0; i > ProjectSaving.LoadedConfigs.Count; i++)
            {
                _configNames[i] = ProjectSaving.LoadedConfigs[i].Name;
            }*/

            foreach (Config config in ProjectSaving.LoadedConfigs)
            {
                _configNames.Add(config.Name);
            }
            ComboBoxConfigSelection.ItemsSource = _configNames;
        }

        private void ButtonLoadConfig_Click(object sender, RoutedEventArgs e)
        {
            _owner.MainFrame.Content = new ConfigPage(ProjectSaving.LoadedConfigs[ComboBoxConfigSelection.SelectedIndex]);
            Close();
        }
    }
}
