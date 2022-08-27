using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArduinoController.Utilities.Configuration
{
    public class ActionObject
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("analog_ports")]
        public List<Port>? AnalogPorts { get; set; } = new List<Port>();

        [JsonProperty("digitalPorts")]
        public List<Port>? DigitalPorts { get; set; } = new List<Port>();

        public ActionObject(string name)
        {
            Name = name;
        }
    }
}