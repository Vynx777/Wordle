using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public MainWindow()
        {
            InitializeComponent();

            string tekst = "banan";

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    ContentControl contentControl = new ContentControl
                    {
                        Template = (ControlTemplate)FindResource("CharBlockTemplate"),
                        Content = tekst[x],
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Margin = new Thickness(15, 5, 15, 5)
                    };
                    Grid.SetRow(contentControl, y);
                    Grid.SetColumn(contentControl, x);
                    mainGrid.Children.Add(contentControl);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
