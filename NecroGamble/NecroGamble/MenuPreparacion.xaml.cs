using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
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

        //Options variables
        private BrightnessOverride bo = null;


        public object WindowState { get; private set; }
        public object WindowStyle { get; private set; }

        public MenuPreparacion()
        {
            bo = BrightnessOverride.GetForCurrentView();
            this.InitializeComponent();
        }
        private void Ready_Click(object sender, RoutedEventArgs e)
        {
            if (ready)
                this.Frame.Navigate(typeof(MainPage));
        }
        private void Options_Click(object sender, RoutedEventArgs e)
        {
            if (!OptionsPopUp.IsOpen) { OptionsPopUp.IsOpen = true; }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (OptionsPopUp.IsOpen) { OptionsPopUp.IsOpen = false; }
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


        //OPTIONS
        private void FullScreen(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            view.TryEnterFullScreenMode();
        }

        private void Resolution(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            view.ExitFullScreenMode();
        }

        private void LessResolution(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            Size s;
            if (ResolutionText.Text == "1920 x 1080")
            {
                ResolutionText.Text = "1280 x 720";
                s.Height = 720;
                s.Width = 1280;
                view.TryResizeView(s);
            }
            else if (ResolutionText.Text == "1280 x 720")
            {
                ResolutionText.Text = "720 x 576";
                s.Height = 576;
                s.Width = 720;
                view.TryResizeView(s);

            }
            else if (ResolutionText.Text == "720 x 576")
            {
                ResolutionText.Text = "720 x 480";
                s.Height = 480;
                s.Width = 720;
                view.TryResizeView(s);
            }
            else if (ResolutionText.Text == "720 x 480")
            {
                ResolutionText.Text = "1920 x 1080";
                s.Height = 1080;
                s.Width = 1920;
                view.TryResizeView(s);
            }
        }

        private void MoreResolution(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            Size s;
            if (ResolutionText.Text == "1920 x 1080")
            {
                ResolutionText.Text = "720 x 480";
                s.Height = 480;
                s.Width = 720;
                view.TryResizeView(s);
            }
            else if (ResolutionText.Text == "1280 x 720")
            {
                ResolutionText.Text = "1920 x 1080";
                s.Height = 1080;
                s.Width = 1920;
                view.TryResizeView(s);
            }
            else if (ResolutionText.Text == "720 x 576")
            {
                ResolutionText.Text = "1280 x 720";
                s.Height = 720;
                s.Width = 1280;
                view.TryResizeView(s);
            }
            else if (ResolutionText.Text == "720 x 480")
            {
                ResolutionText.Text = "720 x 576";
                s.Height = 576;
                s.Width = 720;
                view.TryResizeView(s);
            }
        }

        private void LanguajeRight(object sender, RoutedEventArgs e)
        {
            if (LanguajeText.Text == "English")
            {
                LanguajeText.Text = "Español";

            }
            else if (LanguajeText.Text == "Español")
            {
                LanguajeText.Text = "French";

            }
            else if (LanguajeText.Text == "French")
            {
                LanguajeText.Text = "Português";

            }
            else if (LanguajeText.Text == "Português")
            {
                LanguajeText.Text = "Italiano";

            }
            else if (LanguajeText.Text == "Italiano")
            {
                LanguajeText.Text = "English";

            }
        }

        private void LanguajeLeft(object sender, RoutedEventArgs e)
        {
            if (LanguajeText.Text == "English")
            {
                LanguajeText.Text = "Italiano";

            }
            else if (LanguajeText.Text == "Español")
            {
                LanguajeText.Text = "English";

            }
            else if (LanguajeText.Text == "French")
            {
                LanguajeText.Text = "Español";

            }
            else if (LanguajeText.Text == "Português")
            {
                LanguajeText.Text = "French";

            }
            else if (LanguajeText.Text == "Italiano")
            {
                LanguajeText.Text = "Português";

            }
        }

        private void BrightSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            double br = BrightSlider.Value / 100;
            bo.SetBrightnessLevel(br, DisplayBrightnessOverrideOptions.None);
            //bo.StartOverride();
            bo.StartOverride();
        }

        private void RevertChanges(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            Size s;
            ResolutionText.Text = "1920 x 1080";
            s.Height = 1080;
            s.Width = 1920;
            view.TryResizeView(s);

            LanguajeText.Text = "English";

            BrightSlider.Value = 50;
            double br = BrightSlider.Value / 100;
            bo.SetBrightnessLevel(br, DisplayBrightnessOverrideOptions.None);
            //bo.StartOverride();
            bo.StartOverride();

            CheckB.IsChecked = true;
            view.TryEnterFullScreenMode();

            OtherSlider.Value = 50;
            VolumeSlider.Value = 50;
        }
    }
}
