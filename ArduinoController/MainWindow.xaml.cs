using System.Windows;
using ArduinoController.Utilities;
using ArduinoController.View.Pages;

namespace ArduinoController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ProjectSaving.CreateConfigFolderIfExists();
            
            MainFrame.Content = new MainPage(this);
        }
    }
}
