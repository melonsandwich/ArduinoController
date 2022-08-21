using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }


        public static LoadingWindow Get()
        {
            return new LoadingWindow();
        }

        public static LoadingWindow Call()
        {
            LoadingWindow window = Get();
            window.ShowDialog();
            return window;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                ProgressBarLoading.Value++;
                Thread.Sleep(10);
            }
            Close();
        }
    }
}
