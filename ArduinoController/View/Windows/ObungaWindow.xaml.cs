using System;
using System.Windows;
using System.Windows.Input;

namespace ArduinoController.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ObungaWindow.xaml
    /// </summary>
    public partial class ObungaWindow : Window
    {
        private Random random = new Random();

        private Point MousePosition => PointToScreen(Mouse.GetPosition(this));

        public ObungaWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            //while ((MousePosition.X <= Left && MousePosition.X <= Left + Width) && (MousePosition.Y <= Top && Mouse.Position <= Top + Height))
            //{
                Left = random.Next(1, (int)SystemParameters.PrimaryScreenWidth - (int)Width);
                Top = random.Next(1, (int)SystemParameters.PrimaryScreenHeight - (int)Height);
            //}
        }
    }
}
