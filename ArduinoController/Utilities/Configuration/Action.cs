using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoController.Utilities.Configuration
{
    public class ActionObject
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("analog_ports")]
        public Dictionary<int, int>? AnalogPorts { get; set; }

        [JsonProperty("digitalPorts")]
        public Dictionary<int, int>? DigitalPorts { get; set; }
    }
}
