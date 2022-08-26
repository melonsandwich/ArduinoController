using Newtonsoft.Json;
using System;
using System.IO;

namespace ArduinoController.Utilities.SettingsControls
{
    public class Settings
    {
        public static Settings Current { get; set; } = new Settings();

        [JsonProperty("selected_com_port")]
        public string SelectedCOMPort { get; set; } = string.Empty;

        [JsonProperty("jumpscare_enabled")]
        public bool JumpscareEnabled { get; set; } = false;

        private static string _localData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static readonly string ConfigFolderPath = _localData + @"\ArduinoController\";

        public static bool IsSettingsFilePresent() => File.Exists(ConfigFolderPath + $@"\settings.json");

        public static Settings CreateSettingsFile()
        {
            Settings settings = new Settings();
            Current = settings;

            SaveSettings();

            return settings;
        }

        public static void SaveSettings()
        {
            using (StreamWriter file = File.CreateText(ConfigFolderPath + @$"\settings.json"))
            {
                string json = JsonConvert.SerializeObject(Current);
                file.Write(json);
            }
        }

        public static Settings? LoadSettingsFile()
        {
            if (!IsSettingsFilePresent()) CreateSettingsFile();

            string contents = File.ReadAllText(ConfigFolderPath + $@"\settings.json");

            Settings settings = JsonConvert.DeserializeObject<Settings>(contents)!;
            if (settings == null) return null;

            Current = settings;

            return settings;
        }
    }
}
