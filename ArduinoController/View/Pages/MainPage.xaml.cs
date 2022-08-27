using ArduinoController.Utilities;
using ArduinoController.Utilities.SettingsControls;
using ArduinoController.View.Windows;
using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ArduinoController.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ConnectionManager ConnectionManager { get; set; } = new ConnectionManager();

        private Window _owner;

        public MainPage(Window owner)
        {
            _owner = owner;

            InitializeComponent();
            LoadingWindow.Call();
            SetCharacter(0);

            // com-port label
            if (Settings.Current.SelectedCOMPort == string.Empty)
            {
                LabelSelectedCOMPort.Content = "COM-порт не выбран! Пожалуйста, откройте настройки для установки COM-порта!";
                LabelSelectedCOMPort.Foreground = Brushes.Red;
                LabelSelectedCOMPort.FontWeight = FontWeights.Bold;
            }
            else
            {
                LabelSelectedCOMPort.Content = $"Текущий COM-порт: {Settings.Current.SelectedCOMPort}";
                LabelSelectedCOMPort.Foreground = Brushes.Black;
                LabelSelectedCOMPort.FontWeight = FontWeights.Normal;
            }
        }

        private CharacterSelection.Character CurrentCharacter => CharacterSelection.Container[_currentCharacterIndex];
        private int _currentCharacterIndex = 0;

        private void ButtonLeftSwitch_Click(object sender, RoutedEventArgs e)
        {
            ButtonSwitch(true);
        }

        private void ButtonRightSwitch_Click(object sender, RoutedEventArgs e)
        {
            ButtonSwitch(false);
        }

        private void ButtonSwitch(bool isLeft)
        {
            int maxIndex = CharacterSelection.Container.Count - 1;
            int newIndex = _currentCharacterIndex;

            newIndex += isLeft ? 1 : -1;
            if (newIndex < 0)
                _currentCharacterIndex = maxIndex;
            else if (newIndex > maxIndex)
                _currentCharacterIndex = 0;
            else
                _currentCharacterIndex = newIndex;

            SetCharacter(_currentCharacterIndex);

            SoundPlayer player = new SoundPlayer(Resource1.siren1);
            if (CurrentCharacter.Name == "obunga")
            {
                player.Play();
                Background = Brushes.Red;
                OpenObungas(20);
            }
            else
            {
                player.Stop();
                Background = Brushes.White;
                CloseObungas();
            }
        }

        private ObungaWindow[]? _obungas;

        private void OpenObungas(int amount)
        {
            _obungas = new ObungaWindow[amount];
            Random random = new Random();

            for (int i = 0; i < amount; i++)
            {
                _obungas[i] = new ObungaWindow();
                _obungas[i].Show();
                _obungas[i].Left = random.Next(1, (int)SystemParameters.PrimaryScreenWidth - (int)_obungas[i].Width);
                _obungas[i].Top = random.Next(1, (int)SystemParameters.PrimaryScreenHeight - (int)_obungas[i].Height);
            }
        }

        private void CloseObungas()
        {
            if (_obungas == null) return;

            foreach (ObungaWindow obunga in _obungas)
            {
                obunga.Close();
            }
            _obungas = null;
        }

        private void SetCharacter(int index)
        {
            _currentCharacterIndex = index;
            ImageCharacter.Source = new BitmapImage(new Uri(CurrentCharacter.ImagePath, UriKind.RelativeOrAbsolute));
            LabelCharacterName.Content = CurrentCharacter.Name;
        }

        private void ButtonCreateConfig_Click(object sender, RoutedEventArgs e)
        {
            ConfigCreationWindow window = new ConfigCreationWindow(_owner);
            window.Show();
        }

        private void ButtonLoadConfig_Click(object sender, RoutedEventArgs e)
        {
            new ConfigLoadWindow((MainWindow)_owner).Show();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.Show();
        }

        private void ButtonTutorial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ButtonCopyProtocolCode_Click(object sender, RoutedEventArgs e)
        {
            new ProtocolCodeCopyWindow().Show();
        }
    }
}
