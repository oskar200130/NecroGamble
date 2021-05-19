using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Composition;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace NecroGamble
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    /// 

   
    public sealed partial class MainPage : Page
    {
        //Options variables
        private BrightnessOverride bo = null;


        public object WindowState { get; private set; }
        public object WindowStyle { get; private set; }

        public MainPage()
        {
            bo = BrightnessOverride.GetForCurrentView();
            this.InitializeComponent();
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
        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Visible;
            ButtonPause.Visibility = Visibility.Collapsed;
            Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatallaBlur.png"));
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;
            ButtonPause.Visibility = Visibility.Visible;
            Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatalla.png"));

        }

        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;
            ButtonPause.Visibility = Visibility.Visible;

            Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatalla.png"));

        }

        //private void Options_Click(object sender, RoutedEventArgs e)
        //{
        //    PauseMenu.Visibility = Visibility.Collapsed;
        //    ButtonPause.Visibility = Visibility.Visible;

        //    Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatalla.png"));

        //}

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;
            ButtonPause.Visibility = Visibility.Collapsed;

            Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\BlurOptiones.png"));

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
                Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatalla.png"));
                //ExitMenu.Visibility = Visibility.Collapsed;
                ButtonPause.Visibility = Visibility.Visible;
            }
            else
            {
                Background.Source = new BitmapImage(new Uri(this.BaseUri, @"\Assets\MenuPausa\fondoBatalla.png"));
                //ExitMenu.Visibility = Visibility.Collapsed;
                PauseMenu.Visibility = Visibility.Visible;
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

        private void Initialize()
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
