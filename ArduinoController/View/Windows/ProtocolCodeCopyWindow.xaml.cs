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
    /// Логика взаимодействия для ProtocolCodeCopyWindow.xaml
    /// </summary>
    public partial class ProtocolCodeCopyWindow : Window
    {
        private string _protocolCode = @"#define SETPINMODE 0
#define SETVALUE 1

#define P_INPUT 0
#define P_OUTPUT 1
#define P_INPUT_PULLUP 2

#define ANALOG 0
#define DIGITAL 1

void setup() 
{
  pinMode(2, OUTPUT);
  pinMode(3, OUTPUT);
  
  protocol_setup(9600, 5);
  // пишите ваш код ниже
}

void loop() 
{
  protocol_loop();
  // пишите ваш код ниже
}

void protocol_setup(int serialSpeed, int timeout)
{
  Serial.begin(serialSpeed);
  Serial.setTimeout(timeout);
}

void protocol_loop()
{
  if (Serial.available() > 2)
  {
    char str[30];
    int amount = Serial.readBytesUntil(';', str, 30);
    str[amount] = NULL;

    int data[10];
    int count = 0;
    char* offset = str;

    while (true)
    {
      data[count++] = atoi(offset);
      offset = strchr(offset, ',');
      if (offset)
        offset++;
      else
        break;
    }

    switch (data[0])
    {
      case SETPINMODE:
        pinMode(data[1], data[3]);
        break;
      case SETVALUE:
        switch (data[2])
        {
          case ANALOG:
            analogWrite(data[1], data[3]);
            break;
          case DIGITAL:
            digitalWrite(data[1], data[3]);
            break;
        }
        break;
    }
  }
}";

        public ProtocolCodeCopyWindow()
        {
            InitializeComponent();
            TextBoxProtocolCode.Text = _protocolCode;
        }

        private void ButtonCopyProtocolCode_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_protocolCode);
            MessageBox.Show("Скопировано!");
        }
    }
}
