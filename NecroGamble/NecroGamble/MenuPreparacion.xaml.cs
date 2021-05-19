using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Input;
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
        public DataPackageView DataView { get; }
        int[] pjValues = new int[6] { -1, -1, -1, -1, -1, -1 };

        bool ready = false;
        bool dicePriority = true;
        bool blockPrio = false;
        bool blockAct = false;

        int prioRerollCount = 3;
        int actRerollCount = 3;
        int prioNum1 = -1, prioNum2 = -1, prioNum3 = -1,
            actNum1 = -1, actNum2 = -1, actNum3 = -1;
        Random rnd = new Random();

        ImageSource draggedImage;
        Image draggedDice;


        //Options variables
        private BrightnessOverride bo = null;


        public object WindowState { get; private set; }
        public object WindowStyle { get; private set; }

        public MenuPreparacion()
        {
            bo = BrightnessOverride.GetForCurrentView();
            this.InitializeComponent();
        }

        private void checkReady()
        {
            int i = 0;
            while (i < pjValues.Length && pjValues[i] != -1) i++;

            if (i == pjValues.Length)
            {
                ready = true;
                readyImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/ready_b.png"));
            }
        }

        private void Ready_Click(object sender, RoutedEventArgs e)
        {
            if (ready)
                this.Frame.Navigate(typeof(GameMenu), pjValues);
        }
        private void Options_Click(object sender, RoutedEventArgs e)
        {
            if (!OptionsPopUp.IsOpen) { OptionsPopUp.IsOpen = true; }
        }

        private void Image_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private void Image1_Drop(object sender, DragEventArgs e)
        {

            if (draggedImage != null && dicePriority)
            {
                p1Prio.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                if (!blockPrio)
                    blockPrio = true;
                p1Prio.AllowDrop = false;

                setPrioPjValues(0);
                if (!ready)
                    checkReady();
            }

        }
        private void Image2_Drop(object sender, DragEventArgs e)
        {

            if (draggedImage != null && dicePriority)
            {
                p2Prio.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                if (!blockPrio)
                    blockPrio = true;
                p2Prio.AllowDrop = false;

                setPrioPjValues(1);
                if (!ready)
                    checkReady();
            }

        }
        private void Image3_Drop(object sender, DragEventArgs e)
        {
            if (draggedImage != null && dicePriority)
            {
                p3Prio.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                if (!blockPrio)
                    blockPrio = true;
                p3Prio.AllowDrop = false;

                setPrioPjValues(2);
                if (!ready)
                    checkReady();
            }
        }
        private void Image4_Drop(object sender, DragEventArgs e)
        {
            if (draggedImage != null && !dicePriority)
            {
                p1Die.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                if (!blockAct)
                    blockAct = true;
                p1Die.AllowDrop = false;

                setActPjValues(3);

                if (!ready)
                    checkReady();
            }
        }
        private void Image5_Drop(object sender, DragEventArgs e)
        {
            if (draggedImage != null && !dicePriority)
            {
                p2Die.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                if (!blockAct)
                    blockAct = true;
                p2Die.AllowDrop = false;

                setActPjValues(4);
                if (!ready)
                    checkReady();
            }
        }
        private void Image6_Drop(object sender, DragEventArgs e)
        {
            if (draggedImage != null && !dicePriority)
            {
                p3Die.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                if (!blockAct)
                    blockAct = true;
                p3Die.AllowDrop = false;

                setActPjValues(5);

                if (!ready)
                    checkReady();
            }
        }

        private void setActPjValues(int index)
        {
            if (draggedDice == dice1)
            {
                pjValues[index] = actNum1;
                actNum1 = -1;
            }
            else if (draggedDice == dice2)
            {
                pjValues[index] = actNum2;
                actNum2 = -1;
            }
            else if (draggedDice == dice3)
            {
                pjValues[index] = actNum3;
                actNum3 = -1;
            }
        }
        private void setPrioPjValues(int index)
        {
            if (draggedDice == dice1)
            {
                pjValues[index] = prioNum1;
                prioNum1 = -1;
            }
            else if (draggedDice == dice2)
            {
                pjValues[index] = prioNum2;
                prioNum2 = -1;
            }
            else if (draggedDice == dice3)
            {
                pjValues[index] = prioNum3;
                prioNum3 = -1;
            }
        }

        private void Viewbox_DragLeave(object sender, DragEventArgs e)
        {
            draggedImage = null;
            draggedDice = null;
        }

        private void Viewbox1_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            draggedImage = (dice1.Source);
            draggedDice = dice1;
        }

        private void Viewbox2_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            draggedImage = (dice2.Source);
            draggedDice = dice2;
        }

        private void Viewbox3_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            draggedImage = (dice3.Source);
            draggedDice = dice3;
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
                if (prioRerollCount > 0 && !blockPrio)
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
            else if (actRerollCount > 0 && !blockAct)
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

        private void dice_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            prioSquare1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/white_square.png"));
            prioSquare2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/white_square.png"));
            prioSquare3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/white_square.png"));

            actSquare1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/white_square.png"));
            actSquare2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/white_square.png"));
            actSquare3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/white_square.png"));
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
        private void Dice1_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;
            showText(prioNum1, actNum1, dice1);
        }
        private void Dice2_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;
            showText(prioNum2, actNum2, dice2);
        }
        private void Dice3_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;
            showText(prioNum3, actNum3, dice3);

        }
        private void showText(int dicePrio, int diceAct, Image dice)
        {
            infoIcon.Source = dice.Source;
            infoText.FontSize = 90;
            infoText.Foreground = new SolidColorBrush(Colors.Black);
            infoText.FontFamily = new FontFamily("Aclonica");

            if (dicePriority)
            {
                if (dicePrio > 0 && prioNum3 < 7)
                {
                    infoText.Text = "\nThe character equipped with this die will have a priority of: " + dicePrio;
                    prioSquare1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    prioSquare2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    prioSquare3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                }
            }
            else
            {
                if (diceAct > 6 && diceAct < 13)
                {
                    actSquare1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    actSquare2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    actSquare3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                }
                switch (diceAct)
                {
                    case 7:
                        infoText.Text = "\nThe Trapper's Dice has: \n\n" +
                            "- 4 faces which place 1 trap on the enemy's next square (1 damage point) \n\n" +
                            "- 1 face which place 1 strong trap on the enemy's next square (3 damage points) \n\n" +
                            "- 1 blank face with no effect";
                        break;
                    case 8:
                        infoText.Text = "\nThe Toxic Die has: \n\n" +
                            "- 4 faces which deal 1 damage points to the enemy everytime it moves for 3 turns \n\n" +
                            "- 1 face which deals 2 damage points to the enemy everytime it moves for 2 turns \n\n" +
                            "- 1 blank face with no effect";
                        break;
                    case 9:
                        infoText.Text = "\nThe Fast Attack Die has: \n\n" +
                            "- 3 faces which deal 1 damage points to the enemy \n\n" +
                            "- 2 faces which deal 2 damage points to the enemy \n\n" +
                            "- 1 face which deals 3 damage points to the enemy";
                        break;
                    case 10:
                        infoText.Text = "\nThe Slow Motion Die has: \n\n" +
                            "- 3 faces which substract 1 square to the next enemy movement \n\n" +
                            "- 1 face which substract 2 squares to the next enemy movement \n\n" +   
                            "- 1 blank face with no effect";
                        break;
                    case 11:
                        infoText.Text = "\nThe Ranged Attack Die has: \n\n" +
                            "- 2 faces which deal 1 damage points to the enemy \n\n" +
                            "- 3 faces which deal 2 damage points to the enemy \n\n" +
                            "- 1 blank face with no effect";
                        break;
                    case 12:
                        infoText.Text = "\nThe Gambler's Die has: \n\n" +
                            "- 1 face which deals 8 damage points to the enemy \n\n" +
                            "- 5 blank faces with no effect";
                        break;
                    default:
                        break;
                }
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
