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
using Windows.UI.Xaml.Navigation;
using static NecroGamble.MenuPreparacion;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace NecroGamble
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MenuPrincipal : Page
    {
        //Options variables
        private BrightnessOverride bo = null;

        public object WindowState { get; private set; }
        public object WindowStyle { get; private set; }

        OptionsStr optionsStr;

        public MenuPrincipal()
        {
            bo = BrightnessOverride.GetForCurrentView();
            this.InitializeComponent();
            Init();
        }

        private void Options_Button(object sender, RoutedEventArgs e)
        {
            if (!OptionsPopUp.IsOpen) { OptionsPopUp.IsOpen = true; }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (OptionsPopUp.IsOpen) { OptionsPopUp.IsOpen = false; }
        }
        private void NewGame_Button(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MenuPreparacion), optionsStr);
        }

        private void LoadGame_Button(object sender, RoutedEventArgs e)
        {
            optionsStr.dices = new int[]{2, 5, 4, 9, 12, 7};
            this.Frame.Navigate(typeof(GameMenu), optionsStr);
        }

        private void Exit (object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
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
