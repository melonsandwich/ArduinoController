﻿using ArduinoController.Utilities.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoController.Utilities
{
    public class ProjectSaving
    {
        public static List<Config> LoadedConfigs { get; set; } = new List<Config>();

        private static string _localData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static readonly string ConfigFolderPath = _localData + @"\Configs";

        public void CreateConfigFolderIfExists()
        { 
            if (!IsConfigFolderPresent())
            {
                DirectoryInfo di = Directory.CreateDirectory(ConfigFolderPath);
            }
        }

        public bool IsConfigFolderPresent() => Directory.Exists(ConfigFolderPath);
 
        public void CreateConfig(string name, int analogPortCount, int digitalPortCount)
        {
            Config config = new Config(name, analogPortCount, digitalPortCount);
            LoadedConfigs.Add(config);

            using (StreamWriter file = File.CreateText(ConfigFolderPath + @$"\{name}.json"))
            {
                string json = JsonConvert.SerializeObject(config);
                file.Write(json);
            }
        }

        public void ScanConfigs()
        {
            DirectoryInfo info = new DirectoryInfo(ConfigFolderPath);
            foreach (var file in Directory.EnumerateFiles(ConfigFolderPath, "*.json"))
            {
                string contents = File.ReadAllText(file);

                Config config = JsonConvert.DeserializeObject<Config>(contents)!;
                if (config == null || !config.IsValid) return;

                LoadedConfigs.Add(config);
            }
        }

        public void DeleteConfig()
        {
            // TODO
        }
    }
}
