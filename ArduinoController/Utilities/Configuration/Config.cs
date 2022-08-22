using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoController.Utilities.Configuration
{
    public class Config
    {
        [JsonProperty("file_type")]
        public string FileType { get; set; } = ValidationFileType;

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("analog_port_count")]
        public int AnalogPortCount { get; set; }

        [JsonProperty("digital_port_count")]
        public int DigitalPortCount { get; set; }

        [JsonProperty("actions")]
        public List<ActionObject> Actions { get; set; }

        public const string ValidationFileType = "ac_config";

        public bool IsValid => FileType == ValidationFileType;

        public Config(string name, int analogPortCount, int digitalPortCount)
        {
            Name = name;
            AnalogPortCount = analogPortCount;
            DigitalPortCount = digitalPortCount;
        }

        public Config(string name, int analogPortCount, int digitalPortCount, List<ActionObject> actions) : this(name, analogPortCount, digitalPortCount)
        {
            Actions = actions;
        }
    }
}
