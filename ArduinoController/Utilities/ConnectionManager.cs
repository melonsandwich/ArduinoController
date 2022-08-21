﻿using Solid.Arduino;
using Solid.Arduino.Firmata;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ArduinoController.Utilities
{
    public class ConnectionManager
    {
        public ISerialConnection SerialConnection { get; set; }

        public ConnectionManager()
        {
            SerialConnection = GetConnection();

            if (SerialConnection != null)
            {
                using (var session = new ArduinoSession(SerialConnection))
                {
                    PerformBasicTest(session, out string output);
                    MessageBox.Show(output);
                }
            }
        }

        private static ISerialConnection GetConnection()
        {
            ISerialConnection connection = EnhancedSerialConnection.Find();

            if (connection == null)
                MessageBox.Show("No connection found");
            else
                MessageBox.Show($"Connected to port {connection.PortName} at {connection.BaudRate} baud");

            return connection;
        }

        public static void PerformBasicTest(IFirmataProtocol session, out string output)
        {
            output = string.Empty;

            var firmware = session.GetFirmware();
            output += $"Firmware: {firmware.Name}, version {firmware.MajorVersion}.{firmware.MinorVersion}";
            var protocolVersion = session.GetProtocolVersion();
            output += $"Firmata protocol version {protocolVersion.Major}.{protocolVersion.Minor}";

            session.SetDigitalPinMode(2, PinMode.DigitalOutput);
            session.SetDigitalPin(2, true);
            Thread.Sleep(1000);
            session.SetDigitalPin(2, false);
        }
    }
}
