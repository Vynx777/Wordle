using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wordle
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int stage = 0;
        private const string answerText = "mleko";
        private ContentControl[,] charBlocks;
        public MainWindow()
        {
            InitializeComponent();

            charBlocks = new ContentControl[5,6];

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    ContentControl contentControl = new ContentControl
                    {
                        Template = (ControlTemplate)FindResource("CharBlockTemplate"),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Margin = new Thickness(15, 5, 15, 5)
                    };

                    charBlocks[x, y] = contentControl;

                    Grid.SetRow(contentControl, y);
                    Grid.SetColumn(contentControl, x);
                    mainGrid.Children.Add(contentControl);
                }
            }
        }

        private void Reset()
        {
            stage = 0;
            textInput.Text = "";
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    charBlocks[x, y].Content = "";
                    charBlocks[x, y].Background = Brushes.White;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tekst = textInput.Text;

            if (tekst.Length != 5 || Regex.IsMatch(tekst, @"\d"))
            {
                MessageBox.Show($"Błąd! {tekst} nie jest prawidłowym słowem");
                return;
            }

            if (stage < 0 || stage > 5)
            {
                MessageBox.Show("Koniec gry!");
                return;
            }

            bool[] green = new bool[5];
            int green_count = 0;
            for (int x = 0; x < 5; x++)
            {
                charBlocks[x, stage].Content = tekst[x].ToString();

                if (tekst[x] == answerText[x])
                {
                    green[x] = true;
                    green_count++;
                    charBlocks[x, stage].Background = Brushes.Green;
                }
            }

            int[] counts = new int[26];
            for (int i = 0; i < 5; i++)
            {
                if (!green[i])
                {
                    char c = char.ToLowerInvariant(answerText[i]);
                    if (c >= 'a' && c <= 'z')
                        counts[c - 'a']++;
                }
            }

            for (int x = 0; x < 5; x++)
            {
                if (green[x]) continue;

                char g = char.ToLowerInvariant(tekst[x]);
                if (g >= 'a' && g <= 'z' && counts[g - 'a'] > 0)
                {
                    charBlocks[x, stage].Background = Brushes.Yellow;
                    counts[g - 'a']--;
                }
                else
                {
                    charBlocks[x, stage].Background = Brushes.Gray;
                }
            }

            if (stage >= 5 || green_count == 5)
            {
                MessageBox.Show("Koniec gry!");
                Reset();
                return;
            }

            stage++;
        }
    }
}
