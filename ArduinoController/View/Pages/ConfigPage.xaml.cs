using ArduinoController.Utilities.Configuration;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ArduinoController.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConfigPage.xaml
    /// </summary>
    public partial class ConfigPage : Page
    {
        public Config Config { get; set; }

        public List<Pin> AnalogPins { get; set; }
        public List<Pin> DigitalPins { get; set; }

        public ConfigPage(Config config)
        {
            InitializeComponent();
            Config = config;
        }

        public class Pin
        {
            

            public string Number { get; set; }

            public int Value { get; set; }
        }
    }
}
