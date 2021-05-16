using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class MenuPreparacion : Page
    {
        bool ready = true;
        bool dicePriority = true;
        int prioRerollCount = 3;
        int actRerollCount = 3;
        int prioNum1 = -1, prioNum2 = -1, prioNum3 = -1,
            actNum1 = -1, actNum2 = -1, actNum3 = -1;
        Random rnd = new Random();
        public MenuPreparacion()
        {
            this.InitializeComponent();
        }
        private void Ready_Click(object sender, RoutedEventArgs e)
        {
            if (ready)
                this.Frame.Navigate(typeof(MainPage));
        }
        private void Options_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Options));
        }
        private void Throw_Click(object sender, RoutedEventArgs e)
        {
            if (dicePriority)
            {
                if (prioRerollCount > 0)
                {
                    prioRerollCount--;
                    rerollCount.Text = "" + prioRerollCount;
                    prioNum1 = rnd.Next(1, 7);
                    prioNum2 = rnd.Next(1, 7);
                    prioNum3 = rnd.Next(1, 7);

                    dice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + prioNum1 + "dice.png"));
                    dice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + prioNum2 + "dice.png"));
                    dice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + prioNum3 + "dice.png"));
                }
            }
            else if (actRerollCount > 0)
            {
                actRerollCount--;
                rerollCount.Text = "" + actRerollCount;

                actNum1 = rnd.Next(7, 13);
                do
                {
                    actNum2 = rnd.Next(7, 13);
                } while (actNum1 == actNum2);
                do
                {
                    actNum3 = rnd.Next(7, 13);
                } while (actNum3 == actNum2 || actNum3 == actNum1);


                dice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + actNum1 + "dice.png"));
                dice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + actNum2 + "dice.png"));
                dice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + actNum3 + "dice.png"));
            }

        }
        private void Priority_Click(object sender, RoutedEventArgs e)
        {
            if (!dicePriority)
            {
                dicePriority = true;
                priorityBut.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/priority_a.png"));
                actionBut.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/action_b.png"));
                rerollCount.Text = "" + prioRerollCount;

                if (prioNum1 != -1)
                    dice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + prioNum1 + "dice.png"));
                else
                    dice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                if (prioNum2 != -1)
                    dice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + prioNum2 + "dice.png"));
                else
                    dice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                if (prioNum3 != -1)
                    dice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + prioNum3 + "dice.png"));
                else
                    dice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
            }

        }
        private void Action_Click(object sender, RoutedEventArgs e)
        {
            if (dicePriority)
            {
                dicePriority = false;
                priorityBut.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/priority_b.png"));
                actionBut.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/action_a.png"));
                rerollCount.Text = "" + actRerollCount;

                if (actNum1 != -1)
                    dice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + actNum1 + "dice.png"));
                else
                    dice1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                if (actNum2 != -1)
                    dice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + actNum2 + "dice.png"));
                else
                    dice2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                if (actNum3 != -1)
                    dice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/" + actNum3 + "dice.png"));
                else
                    dice3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
            }
        }
    }
}
