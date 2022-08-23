using Newtonsoft.Json;

namespace ArduinoController.Utilities.Configuration
{
    public enum PinMode
    {
        [JsonProperty("input")]
        Input = 0,

        [JsonProperty("output")]
        Output = 1,

        [JsonProperty("input_pullup")]
        InputPullup = 2
    }
}
