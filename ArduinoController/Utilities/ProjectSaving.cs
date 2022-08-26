using ArduinoController.Utilities.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ArduinoController.Utilities
{
    public static class ProjectSaving
    {
        public static List<Config> LoadedConfigs { get; set; } = new List<Config>();

        private static string _localData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static readonly string ConfigFolderPath = _localData + @"\ArduinoController\Configs";

        public static void CreateConfigFolderIfExists()
        { 
            if (!IsConfigFolderPresent())
            {
                DirectoryInfo di = Directory.CreateDirectory(ConfigFolderPath);
            }
        }

        public static bool IsConfigFolderPresent() => Directory.Exists(ConfigFolderPath);
 
        public static Config CreateConfig(string name, int analogPortCount, int digitalPortCount)
        {
            Config config = new Config(name, analogPortCount, digitalPortCount);
            LoadedConfigs.Add(config);

            using (StreamWriter file = File.CreateText(ConfigFolderPath + @$"\{name}.json"))
            {
                string json = JsonConvert.SerializeObject(config);
                file.Write(json);
            }

            return config;
        }

        public static void ScanConfigs()
        {
            foreach (var file in Directory.EnumerateFiles(ConfigFolderPath, "*.json"))
            {
                string contents = File.ReadAllText(file);

                Config config = JsonConvert.DeserializeObject<Config>(contents)!;
                if (config == null || !config.IsValid) return;

                LoadedConfigs.Add(config);
            }
        }

        public static void DeleteConfig()
        {
            // TODO
        }
    }
}
