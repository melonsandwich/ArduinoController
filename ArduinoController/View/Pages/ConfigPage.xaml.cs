﻿using ArduinoController.Utilities;
using ArduinoController.Utilities.Configuration;
using ArduinoController.Utilities.SettingsControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ArduinoController.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConfigPage.xaml
    /// </summary>
    public partial class ConfigPage : Page
    {
        public Config Config { get; set; }

        private SerialPort _serialPort;

        public ObservableCollection<Pin> AnalogPins { get; set; } = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> DigitalPins { get; set; } = new ObservableCollection<Pin>();

        private Dictionary<Pin, Pin> _pins = new Dictionary<Pin, Pin>();

        private FunctionExecutor _functionExecutor;

        private bool _isClockWorking { get; set; } = false;

        public ConfigPage(Config config)
        {
            InitializeComponent();
            Config = config;

            for (int i = 1; i < config.AnalogPortCount; i++)
            {
                AnalogPins.Add(new Pin(i, PinType.Analog));
            }

            for (int i = 1; i < config.DigitalPortCount; i++)
            {
                DigitalPins.Add(new Pin(i, PinType.Digital));
            }

            //ListViewAnalogPins.DataContext = new Pin(0);
            //ListViewAnalogPins.DataContext = new Pin(1);

            //ListViewAnalogPins.ItemsSource = AnalogPins;
            //ListViewDigitalPins.ItemsSource = DigitalPins;

            // =====



            List<string> pins = new List<string>();
            for (int i = 0; i < Config.AnalogPortCount; i++)
            {
                pins.Add($"{i}");
            }

            //ComboBoxPinType.SelectedIndex = 0;

            //ComboBoxPin.ItemsSource = 

            // =====

            WriteLine($"Config \"{config.Name}\" loaded successfully");

            _serialPort = new SerialPort();
            _serialPort.PortName = Settings.Current.SelectedCOMPort;
            _serialPort.BaudRate = Settings.Current.BaudRate;

            _serialPort.DataReceived += (object sender, SerialDataReceivedEventArgs args) =>
            {
                Write(_serialPort.ReadExisting());
                //MessageBox.Show(_serialPort.ReadExisting());
            };

            _serialPort.Open();
            WriteLine($"Opened a serial port connection on \"{_serialPort.PortName}\" at {_serialPort.BaudRate} baud-rate");

            _functionExecutor = new FunctionExecutor(_serialPort, this);
            //PortClock();

            _isClockWorking = (bool)CheckBoxAutoChangeValues.IsChecked!;
            _ = Clock();
        }

        public void Write(string text)
        {
            Dispatcher.Invoke(() => TextBlockConsolePrompt.Text += text);
        }

        public void WriteLine(string text)
        {
            Write(text + "\n");
        }

        private async Task Clock()
        {
            while (true)
            {   
                if (_isClockWorking)
                {
                    Dispatcher.Invoke(() =>
                    {
                        _functionExecutor.Write((int)Math.Round(SliderServoAngle.Value, 0));
                    });
                }
                
                await Task.Delay(Settings.Current.SliderValueUpdateFrequency);
            }
        }

        public class Pin
        {
            public int Number { get; private set; }

            public int PinMode { get; set; }

            public int Value
            {
                get => _value;
                set
                {
                    _value = value;
                }
            }
            private int _value;

            public PinType PinType { get => (PinType)PinMode; set => PinMode = (int)value; }


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

        private void ButtonSendToArduino_Click(object sender, RoutedEventArgs e)
        {
            // TODO: optimization checking changed pins

            /*foreach (Pin analogPin in AnalogPins)
            {
                string request = string.Empty;

                // setpinmode
                request = $"0,{analogPin.Number},0,{analogPin.PinMode}";

                _serialPort.Write(request);

                Thread.Sleep(200);

                // setvalue
                request = $"1,{analogPin.Number},0,{analogPin.Value}";

                _serialPort.Write(request);

                Thread.Sleep(200);
            }

            _serialPort.Write("0,1,0,0".ToCharArray(), 0, 7);*/

            //string request = string.Empty;

            // setpinmode
            //request = $"0,{TextBoxPin.Text},{ComboBoxPinType.SelectedIndex},{ComboBoxPinMode.SelectedIndex}";

            //_serialPort.Write(request);
            //Thread.Sleep(200);

            // setvalue
            //request = $"1,{TextBoxPin.Text},{ComboBoxPinType.SelectedIndex},{TextBlockValue.Text}";
           // _serialPort.Write(request);
        }

        private void ButtonSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            ProjectSaving.SaveConfig(Config);
        }
        

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LabelServoAngle.Content = SliderServoAngle.Value;
        }

        private void ButtonDetach_Click(object sender, RoutedEventArgs e)
        {
            _functionExecutor.Detach();
        }

        private void ButtonServoAttach_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _functionExecutor.Attach(Convert.ToInt32(TextBoxServoAttach.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректный ввод!");
            }
        }

        private void ButtonWriteAngle_Click(object sender, RoutedEventArgs e)
        {
            _functionExecutor.Write((int)Math.Round(SliderServoAngle.Value, 0));
        }

        private void ButtonWriteMicroseconds_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _functionExecutor.WriteMicroseconds(Convert.ToInt32(TextBoxWriteMicroseconds.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректный ввод!");
            }
        }

        private void ButtonRead_Click(object sender, RoutedEventArgs e)
        {
            _functionExecutor.Read();
        }

        private void ButtonAttached_Click(object sender, RoutedEventArgs e)
        {
            _functionExecutor.Attached();
        }

        private async void CheckBoxAutoChangeValues_Checked(object sender, RoutedEventArgs e)
        {
            _isClockWorking = true;
            //await Clock();
        }

        private void CheckBoxAutoChangeValues_Unchecked(object sender, RoutedEventArgs e)
        {
            _isClockWorking = false;
        }
    }
}
