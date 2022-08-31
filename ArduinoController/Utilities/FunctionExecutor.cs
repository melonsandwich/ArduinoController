using ArduinoController.View.Pages;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArduinoController.Utilities
{
    public class FunctionExecutor
    {
        public SerialPort SerialPort { get; set; }

        // to be removed
        private ConfigPage _configPage;

        public FunctionExecutor(SerialPort port, ConfigPage configPage)
        {
            SerialPort = port;
            _configPage = configPage;
        }

        public enum FunctionID
        {
            Attach = 0,
            Write,
            WriteMicroseconds,
            Read,
            Attached,
            Detach
        }

        public int ID(FunctionID id) => (int)id;

        public void Attach(int pin)
        {
            SerialPort.Write(BuildRequest(ID(FunctionID.Attach), pin));
        }

        public void Write(int angle)
        {
            if (angle < 0 || angle > 180)
                throw new ArgumentOutOfRangeException(nameof(angle));

            SerialPort.Write(BuildRequest(ID(FunctionID.Write), angle));
        }

        public void WriteMicroseconds(int microseconds)
        {
            SerialPort.Write(BuildRequest(ID(FunctionID.WriteMicroseconds), microseconds));
        }

        public void Read()
        {
            SerialPort.Write(BuildRequest(ID(FunctionID.Read), 0));
        }

        public void Attached()
        {
            SerialPort.Write(BuildRequest(ID(FunctionID.Attached), 0));
        }

        public void Detach()
        {
            SerialPort.Write(BuildRequest(ID(FunctionID.Detach), 0));
        }

        private string BuildRequest(params object[] objs)
        {
            if (objs == null)
            {
                throw new ArgumentNullException(nameof(objs));
            }

            string request = string.Empty;

            for (int i = 0; i < objs.Length; i++)
            {
                request += $"{objs[i]}";
                if (i != objs.Length - 1)
                {
                    request += ",";
                }
            }

            _configPage.WriteLine(request);
            Thread.Sleep(200);
            return request;
        }
    }
}
