using Newtonsoft.Json;

namespace ArduinoController.Utilities.Configuration
{
    public class Port
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("pinmode")]
        public PinMode PinMode { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        public Port(int id)
        {
            ID = id;
        }
    }
}
