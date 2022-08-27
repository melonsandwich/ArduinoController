using ArduinoController.Utilities.SettingsControls;
using System.Collections.Generic;

namespace ArduinoController.Utilities
{
    public class CharacterSelection
    {
        public static List<Character> Container { get; private set; } = new List<Character>();

        //private const string Path = "pack://application:,,,/ArduinoController;component/Resources/Media/";
        private const string Path = "pack://application:,,,/Resources/Media/";
        static CharacterSelection()
        {
            Add("Misato Katsuragi", "misato.png");
            Add("Ye (Kanye West)", "kanye west.jpg");
            if (Settings.Current.JumpscareEnabled) Add("obunga", "obunga.png");
            Add("Valentin Strykalo", "strykalo.png");
            Add("Rei Ayanami", "rei.png");
            Add("Asuka Sohryu Langley", "asuka.png");
        }

        private static void Add(string name, string fileName)
        {
            Container.Add(new Character(name, fileName));
        }

        public struct Character
        {
            public string Name { get; set; }
            public string FileName { get; set; }

            public string ImagePath => Path + FileName;

            public Character(string name, string filename)
            {
                Name = name;
                FileName = filename;
            }
        }
    }
}
