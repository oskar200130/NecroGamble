using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static NecroGamble.MenuPreparacion;


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

        //Options variables
        private BrightnessOverride bo = null;

        public object WindowState { get; private set; }
        public object WindowStyle { get; private set; }

        OptionsStr optionsStr;

        public GameMenu()
        {
            bo = BrightnessOverride.GetForCurrentView();
            this.InitializeComponent();
            Turn1.Background = new SolidColorBrush(Colors.Red);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pjValues = ((OptionsStr)e.Parameter).dices;
            Initialize((OptionsStr)e.Parameter);
            DiceAcction();

            //Initialize();

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

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            if (!OptionsPopUp.IsOpen) { OptionsPopUp.IsOpen = true; }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (OptionsPopUp.IsOpen) { OptionsPopUp.IsOpen = false; }
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

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Visible;
            ButtonPause.Visibility = Visibility.Collapsed;
            //Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatallaBlur.png"));
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;
            ButtonPause.Visibility = Visibility.Visible;
            //Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatalla.png"));

        }

        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            //PauseMenu.Visibility = Visibility.Collapsed;
            //ButtonPause.Visibility = Visibility.Visible;


            //Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatalla.png"));

        }

        //private void Options_Click(object sender, RoutedEventArgs e)
        //{
        //    PauseMenu.Visibility = Visibility.Collapsed;
        //    ButtonPause.Visibility = Visibility.Visible;

        //    //Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatalla.png"));

        //}

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //PauseMenu.Visibility = Visibility.Collapsed;
            //ButtonPause.Visibility = Visibility.Collapsed;

            //Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\BlurOptiones.png"));

            DisplayConfirmationDialog();
        }

        private async void DisplayConfirmationDialog()
        {

            TextBlock text = new TextBlock
            {
                Text = "Are you sure?",
                TextAlignment = TextAlignment.Center,
                FontSize = 45,
                TextWrapping = TextWrapping.Wrap
            };
            StackPanel panel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,

            };

            panel.Children.Insert(0, text);

            ContentDialog confirmDialog = new ContentDialog
            {
                PrimaryButtonText = "Yes",
                CloseButtonText = "No",
                Content = panel
            };

            ContentDialogResult result = await confirmDialog.ShowAsync();

            // Delete the file if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                //Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatalla.png"));
                //ExitMenu.Visibility = Visibility.Collapsed;
                //ButtonPause.Visibility = Visibility.Visible;
                this.Frame.Navigate(typeof(MenuPrincipal), optionsStr);
            }
            else
            {
                //Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatalla.png"));
                //ExitMenu.Visibility = Visibility.Collapsed;
                //PauseMenu.Visibility = Visibility.Visible;
            }
        }

        //OPTIONS
        private void FullScreen(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            view.TryEnterFullScreenMode();
            optionsStr.check = true;
        }

        private void Resolution(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            view.ExitFullScreenMode();
            optionsStr.check = false;
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
                optionsStr.s = s;
            }
            optionsStr.s = s;
            optionsStr.resolution = ResolutionText.Text;
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
            optionsStr.s = s;
            optionsStr.resolution = ResolutionText.Text;
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
            optionsStr.lang = LanguajeText.Text;
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
            optionsStr.lang = LanguajeText.Text;
        }

        private void BrightSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            double br = BrightSlider.Value / 100;
            bo.SetBrightnessLevel(br, DisplayBrightnessOverrideOptions.None);
            //bo.StartOverride();
            bo.StartOverride();
            optionsStr.brightValue = (int)BrightSlider.Value;
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

            optionsStr.s = s;
            optionsStr.brightValue = 50;
            optionsStr.check = true;
            optionsStr.lang = LanguajeText.Text;
            optionsStr.resolution = "1920 x 1080";
            optionsStr.soundValue = 50;
            optionsStr.volValue = 50;
        }

        private void Init()
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

            optionsStr.s = s;
            optionsStr.brightValue = 50;
            optionsStr.check = true;
            optionsStr.lang = LanguajeText.Text;
            optionsStr.resolution = "1920 x 1080";
            optionsStr.soundValue = 50;
            optionsStr.volValue = 50;
        }

        private void Initialize(OptionsStr ost)
        {
            var view = ApplicationView.GetForCurrentView();
            Size s;
            ResolutionText.Text = ost.resolution;
            s.Height = ost.s.Height;
            s.Width = ost.s.Width;
            view.TryResizeView(s);

            LanguajeText.Text = ost.lang;

            BrightSlider.Value = ost.brightValue;
            double br = BrightSlider.Value / 100;
            bo.SetBrightnessLevel(br, DisplayBrightnessOverrideOptions.None);
            //bo.StartOverride();
            bo.StartOverride();

            CheckB.IsChecked = true;
            view.TryEnterFullScreenMode();

            OtherSlider.Value = ost.soundValue;
            VolumeSlider.Value = ost.volValue;

            optionsStr.s = ost.s;
            optionsStr.brightValue = ost.brightValue;
            optionsStr.check = ost.check;
            optionsStr.lang = ost.lang;
            optionsStr.resolution = ost.resolution;
            optionsStr.soundValue = ost.soundValue;
            optionsStr.volValue = ost.volValue;
        }
    }
}
