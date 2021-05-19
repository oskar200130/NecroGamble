using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace NecroGamble
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class GameMenu : Page
    {
        Random rand = new Random();
        private bool dadoIzq = true;
        private bool dadoDcho = true;
        private int numDados = 0;
        private int turn = 0;

        public GameMenu()
        {
            this.InitializeComponent();
            Turn1.Background = new SolidColorBrush(Colors.Red);
        }

        private void Moneda_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonsTarget.Text == "")
            {
                dadoDcho = dadoIzq = true;
                int i = rand.Next(0, 2);
                if (i == 0)
                {
                    //if (ButtonsTarget.Text != "Tira solo uno")
                    //{
                    Moneda.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/monedaCara.png"));
                    ButtonsTarget.Text = "Tira solo uno";
                    numDados = 1;
                    //}
                }
                else
                {
                    //if (ButtonsTarget.Text != "Tira ambos")
                    //{
                    Moneda.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/monedaCruz.png"));
                    ButtonsTarget.Text = "Tira ambos";
                    numDados = 2;
                    //}
                }
            }
        }

        private void WalkDice_Click(object sender, RoutedEventArgs e)
        {
            if (dadoIzq)
            {
                numDados--;
                if (numDados == 0) DiceAcction();
                dadoIzq = false;
            }
        }

        private void AttackDice_Click(object sender, RoutedEventArgs e)
        {
            if (dadoDcho)
            {
                numDados--;
                if (numDados == 0) DiceAcction();
                dadoDcho = false;
            }
        }

        private void DiceAcction()
        {
            infoText.FontSize = 40;
            infoText.Foreground = new SolidColorBrush(Colors.Black);
            infoText.FontFamily = new FontFamily("Aclonica");

            ButtonsTarget.Text = "";
            turn++;
            if (turn > 3) turn = 0;
            switch (turn)
            {
                case 0:
                    Turn4.Background = new SolidColorBrush(Colors.Transparent);
                    Turn1.Background = new SolidColorBrush(Colors.Red);
                    TurnArrow4.Visibility = Visibility.Collapsed;
                    TurnArrow1.Visibility = Visibility.Visible;
                    infoImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/arquero.png"));
                    infoText.Text = "Hola";
                    
                    break;
                case 1:
                    Turn1.Background = new SolidColorBrush(Colors.Transparent);
                    Turn2.Background = new SolidColorBrush(Colors.Red);
                    TurnArrow1.Visibility = Visibility.Collapsed;
                    TurnArrow2.Visibility = Visibility.Visible;
                    infoImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/muerte.png"));
                    infoText.Text = "Tus";
                    break;
                case 2:
                    Turn2.Background = new SolidColorBrush(Colors.Transparent);
                    Turn3.Background = new SolidColorBrush(Colors.Red);
                    TurnArrow2.Visibility = Visibility.Collapsed;
                    TurnArrow3.Visibility = Visibility.Visible;
                    infoImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/caballero.png"));
                    infoText.Text = "Putos";
                    break;
                case 3:
                    Turn3.Background = new SolidColorBrush(Colors.Transparent);
                    Turn4.Background = new SolidColorBrush(Colors.Red);
                    TurnArrow3.Visibility = Visibility.Collapsed;
                    TurnArrow4.Visibility = Visibility.Visible;
                    infoImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/tercio.png"));
                    infoText.Text = "Muertos";
                    break;
            }
        }
    }
}
