using Newtonsoft.Json;
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
        private int turn = 3;
        private int[] pjValues = new int[6] { -1, -1, -1, -1, -1, -1 };

        public GameMenu()
        {
            this.InitializeComponent();
            Turn1.Background = new SolidColorBrush(Colors.Red);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pjValues = (int[])e.Parameter;
            DiceAcction();

            base.OnNavigatedTo(e);
        }
        private void Moneda_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonsTarget.Text == "")
            {
                dadoDcho = dadoIzq = true;
                int i = rand.Next(0, 2);
                if (i == 0)
                {
                    Moneda.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/monedaCara.png"));
                    ButtonsTarget.Text = "Tira solo uno";
                    numDados = 1;
                }
                else
                {
                    Moneda.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/monedaCruz.png"));
                    ButtonsTarget.Text = "Tira ambos";
                    numDados = 2;
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
                    infoText.Text = "CABALLERO:";
                    getText(pjValues[3], infoText);
                    break;
                case 1:
                    Turn1.Background = new SolidColorBrush(Colors.Transparent);
                    Turn2.Background = new SolidColorBrush(Colors.Red);
                    TurnArrow1.Visibility = Visibility.Collapsed;
                    TurnArrow2.Visibility = Visibility.Visible;
                    infoImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/muerte.png"));
                    infoText.Text = "MUERTE";
                    break;
                case 2:
                    Turn2.Background = new SolidColorBrush(Colors.Transparent);
                    Turn3.Background = new SolidColorBrush(Colors.Red);
                    TurnArrow2.Visibility = Visibility.Collapsed;
                    TurnArrow3.Visibility = Visibility.Visible;
                    infoImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/caballero.png"));
                    infoText.Text = "CABALLERO:";
                    getText(pjValues[5], infoText);
                    break;
                case 3:
                    Turn3.Background = new SolidColorBrush(Colors.Transparent);
                    Turn4.Background = new SolidColorBrush(Colors.Red);
                    TurnArrow3.Visibility = Visibility.Collapsed;
                    TurnArrow4.Visibility = Visibility.Visible;
                    infoImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/PartidaMenu/tercio.png"));
                    infoText.Text = "CABALLERO:";
                    getText(pjValues[4], infoText);
                    break;
            }
        }

        private void getText(int i, TextBlock t)
        {
            switch (i)
            {
                case 7:
                    t.Text += "\nThe Trapper's Dice has: \n\n" +
                        "- 4 faces which place 1 trap on the enemy's next square (1 damage point) \n\n" +
                        "- 1 face which place 1 strong trap on the enemy's next square (3 damage points) \n\n" +
                        "- 1 blank face with no effect\n\n";
                    DiceBottonIm.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/7dice.png"));
                    break;
                case 8:
                    t.Text += "\nThe Toxic Die has: \n\n" +
                        "- 4 faces which deal 1 damage points to the enemy everytime it moves for 3 turns \n\n" +
                        "- 1 face which deals 2 damage points to the enemy everytime it moves for 2 turns \n\n" +
                        "- 1 blank face with no effect\n\n";
                    DiceBottonIm.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/8dice.png"));
                    break;
                case 9:
                    t.Text += "\nThe Fast Attack Die has: \n\n" +
                        "- 3 faces which deal 1 damage points to the enemy \n\n" +
                        "- 2 faces which deal 2 damage points to the enemy \n\n" +
                        "- 1 face which deals 3 damage points to the enemy\n\n";
                    DiceBottonIm.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/9dice.png"));
                    break;
                case 10:
                    t.Text += "\nThe Slow Motion Die has: \n\n" +
                        "- 3 faces which substract 1 square to the next enemy movement \n\n" +
                        "- 1 face which substract 2 squares to the next enemy movement \n\n" +
                        "- 1 blank face with no effect\n\n";
                    DiceBottonIm.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/10dice.png"));
                    break;
                case 11:
                    t.Text += "\nThe Ranged Attack Die has: \n\n" +
                        "- 2 faces which deal 1 damage points to the enemy \n\n" +
                        "- 3 faces which deal 2 damage points to the enemy \n\n" +
                        "- 1 blank face with no effect \n\n";
                    DiceBottonIm.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/11dice.png"));
                    break;
                case 12:
                    t.Text += "\nThe Gambler's Die has: \n\n" +
                        "- 1 face which deals 8 damage points to the enemy \n\n" +
                        "- 5 blank faces with no effect \n\n";
                    DiceBottonIm.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/12dice.png"));
                    break;
                default:
                    break;
            }
        }
    }
}
