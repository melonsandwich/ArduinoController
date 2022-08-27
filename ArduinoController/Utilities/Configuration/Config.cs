using Newtonsoft.Json;
using System.Collections.Generic;

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
        public List<ActionObject> Actions { get; set; } = new List<ActionObject>();

        public const string ValidationFileType = "ac_config";

        [JsonIgnore]
        public bool IsValid => FileType == ValidationFileType;

        public Config(string name, int analogPortCount, int digitalPortCount)
        {
            Name = name;
            AnalogPortCount = analogPortCount;
            DigitalPortCount = digitalPortCount;

            Actions.Add(new ActionObject("Default"));
            
            for (int i = 0; i < AnalogPortCount; i++)
            {
                Actions[0].AnalogPorts!.Add(new Port(i + 1));
            }
            for (int i = 0; i < DigitalPortCount; i++)
            {
                Actions[0].DigitalPorts!.Add(new Port(i + 1));
            }

        }

        /*public Config(string name, int analogPortCount, int digitalPortCount, List<ActionObject> actions) : this(name, analogPortCount, digitalPortCount)
        {
            Actions = actions;
        }*/
    }
}
