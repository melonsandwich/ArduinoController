using ArduinoController.Utilities;
using ArduinoController.Utilities.SettingsControls;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public static bool JumpscareEnabled { get; set; } = Settings.Current.JumpscareEnabled;

        public DebugWindow()
        {
            InitializeComponent();
            SetJumpscareButtonTextOnValue(Settings.Current.JumpscareEnabled);
        }

        private void ButtonDeleteSettingsJson_Click(object sender, RoutedEventArgs e)
        {
            File.Delete(ProjectSaving.ConfigFolderPath + @"\settings.json");
        }

        private void ButtonEnableJumpscare_Click(object sender, RoutedEventArgs e)
        {
            Settings.Current.JumpscareEnabled = !Settings.Current.JumpscareEnabled;
            SetJumpscareButtonTextOnValue(Settings.Current.JumpscareEnabled);
            Settings.SaveSettings();
        }

        private void SetJumpscareButtonTextOnValue(bool value)
        {
            SolidColorBrush defaultBrush = (SolidColorBrush)ButtonEnableJumpscare.Background;
            if (value)
            {
                ButtonEnableJumpscare.Content = "disable jumpscare (yep do it) requires reload";
                ButtonEnableJumpscare.Foreground = Brushes.White;
                ButtonEnableJumpscare.Background = Brushes.Red;
            }
            else
            {
                ButtonEnableJumpscare.Content = "enable jumpscare (NO AT ANY COST!!!) requires reload";
                ButtonEnableJumpscare.Foreground = Brushes.White;
                ButtonEnableJumpscare.Background = defaultBrush;
            }
        }

        private void ButtonProgramReboot_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
