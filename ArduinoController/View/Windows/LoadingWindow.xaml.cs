using System;
using System.Threading;
using System.Windows;

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
