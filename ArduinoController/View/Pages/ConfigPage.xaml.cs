using ArduinoController.Utilities.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ArduinoController.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConfigPage.xaml
    /// </summary>
    public partial class ConfigPage : Page
    {
        public Config Config { get; set; }

        public ObservableCollection<Pin> AnalogPins { get; set; } = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> DigitalPins { get; set; } = new ObservableCollection<Pin>();

        public ConfigPage(Config config)
        {
            InitializeComponent();
            Config = config;

            for (int i = 0; i < config.AnalogPortCount; i++)
            {
                AnalogPins.Add(new Pin(i, PinType.Analog));
            }

            for (int i = 0; i < config.DigitalPortCount; i++)
            {
                DigitalPins.Add(new Pin(i, PinType.Digital));
            }

            ListViewAnalogPins.ItemsSource = AnalogPins;
            ListViewDigitalPins.ItemsSource = DigitalPins;

            WriteLine($"Config \"{config.Name}\" loaded successfully");
        }

        public void Write(string text)
        {
            TextBlockConsolePrompt.Text += text;
        }

        public void WriteLine(string text)
        {
            Write("\n" + text);
        }

        public class Pin
        {
            public int Number { get; private set; }

            public int Value
            {
                get => _value;
                set
                {

                }
            }
            private int _value;

            public PinType PinType { get; set; }


            public Pin(int number, int value, PinType pinType = PinType.Analog)
            {
                Number = number;
                Value = value;
                pinType = PinType;
            }

            public Pin(int number, PinType pinType = PinType.Analog) : this(number, 0, pinType) { }
        }

        public enum PinType
        {
            Analog = 0,
            Digital
        }

        private void ButtonSendToArduino_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
