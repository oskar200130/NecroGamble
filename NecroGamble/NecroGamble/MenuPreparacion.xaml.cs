using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        Viewbox draggedDiceBox;


        public MenuPreparacion()
        {
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
                this.Frame.Navigate(typeof(MainPage));
        }
        private void Options_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Options));
        }

        private void Image_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void Image1_Drop(object sender, DragEventArgs e)
        {

            if (draggedImage != null && dicePriority)
            {
                p1Prio.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                //draggedDiceBox.CanDrag = false;
                if (!blockPrio)
                    blockPrio = true;
                p1Prio.AllowDrop = false;

                if (draggedDice == dice1)
                {
                    pjValues[0] = prioNum1;
                    prioNum1 = -1;
                }
                else if (draggedDice == dice2)
                {
                    pjValues[0] = prioNum2;
                    prioNum2 = -1;
                }
                else if (draggedDice == dice3)
                {
                    pjValues[0] = prioNum3;
                    prioNum3 = -1;
                }
                if (!ready)
                    checkReady();
            }

        }
        private async void Image2_Drop(object sender, DragEventArgs e)
        {

            if (draggedImage != null && dicePriority)
            {
                p2Prio.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                //draggedDiceBox.CanDrag = false;
                if (!blockPrio)
                    blockPrio = true;
                p2Prio.AllowDrop = false;

                if (draggedDice == dice1)
                {
                    pjValues[1] = prioNum1;
                    prioNum1 = -1;
                }
                else if (draggedDice == dice2)
                {
                    pjValues[1] = prioNum2;
                    prioNum2 = -1;
                }
                else if (draggedDice == dice3)
                {
                    pjValues[1] = prioNum3;
                    prioNum3 = -1;
                }
                if (!ready)
                    checkReady();
            }

        }
        private async void Image3_Drop(object sender, DragEventArgs e)
        {
            if (draggedImage != null && dicePriority)
            {
                p3Prio.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                //draggedDiceBox.CanDrag = false;
                if (!blockPrio)
                    blockPrio = true;
                p3Prio.AllowDrop = false;

                if (draggedDice == dice1)
                {
                    pjValues[2] = prioNum1;
                    prioNum1 = -1;
                }
                else if (draggedDice == dice2)
                {
                    pjValues[2] = prioNum2;
                    prioNum2 = -1;
                }
                else if (draggedDice == dice3)
                {
                    pjValues[2] = prioNum3;
                    prioNum3 = -1;
                }
                if (!ready)
                    checkReady();
            }
        }
        private async void Image4_Drop(object sender, DragEventArgs e)
        {
            if (draggedImage != null && !dicePriority)
            {
                p1Die.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                //draggedDiceBox.CanDrag = false;
                if (!blockAct)
                    blockAct = true;
                p1Die.AllowDrop = false;

                if (draggedDice == dice1)
                {
                    pjValues[3] = actNum1;
                    actNum1 = -1;
                }
                else if (draggedDice == dice2)
                {
                    pjValues[3] = actNum2;
                    actNum2 = -1;
                }
                else if (draggedDice == dice3)
                {
                    pjValues[3] = actNum3;
                    actNum3 = -1;
                }
                if (!ready)
                    checkReady();
            }
        }
        private async void Image5_Drop(object sender, DragEventArgs e)
        {
            if (draggedImage != null && !dicePriority)
            {
                p2Die.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                //draggedDiceBox.CanDrag = false;
                if (!blockAct)
                    blockAct = true;
                p2Die.AllowDrop = false;

                if (draggedDice == dice1)
                {
                    pjValues[4] = actNum1;
                    actNum1 = -1;
                }
                else if (draggedDice == dice2)
                {
                    pjValues[4] = actNum2;
                    actNum2 = -1;
                }
                else if (draggedDice == dice3)
                {
                    pjValues[4] = actNum3;
                    actNum3 = -1;
                }
                if (!ready)
                    checkReady();
            }
        }
        private async void Image6_Drop(object sender, DragEventArgs e)
        {
            if (draggedImage != null && !dicePriority)
            {
                p3Die.Source = draggedImage;
                draggedDice.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/empty_dice.png"));
                //draggedDiceBox.CanDrag = false;
                if (!blockAct)
                    blockAct = true;
                p3Die.AllowDrop = false;

                if (draggedDice == dice1)
                {
                    pjValues[5] = actNum1;
                    actNum1 = -1;
                }
                else if (draggedDice == dice2)
                {
                    pjValues[5] = actNum2;
                    actNum2 = -1;
                }
                else if (draggedDice == dice3)
                {
                    pjValues[5] = actNum3;
                    actNum3 = -1;
                }
                if (!ready)
                    checkReady();
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
            draggedDiceBox = dice1Box;
        }

        private void Viewbox2_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            draggedImage = (dice2.Source);
            draggedDiceBox = dice2Box;
            draggedDice = dice2;
        }

        private void Viewbox3_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            draggedImage = (dice3.Source);
            draggedDice = dice3;
            draggedDiceBox = dice3Box;
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

            infoIcon.Source = dice1.Source;
            infoText.FontSize = 90;
            infoText.Foreground = new SolidColorBrush(Colors.Black);
            infoText.FontFamily = new FontFamily("Aclonica");

            if (dicePriority)
            {
                if (prioNum1 > 0 && prioNum1 < 7)
                {
                    infoText.Text = "\nEl personaje equipado con este dado poseerá una prioridad de " + prioNum1;
                    prioSquare1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    prioSquare2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    prioSquare3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                }
            }
            else
            {
                if (actNum1 > 6 && actNum1 < 13)
                {
                    actSquare1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    actSquare2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    actSquare3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                }
                switch (actNum1)
                {
                    case 7:
                        infoText.Text = "\nEl Dado de Trampero tiene: \n\n" +
                            "- 4 caras que colocan 1 trampa en la próxima casilla del enemigo (1 de daño) \n\n" +
                            "- 1 cara que coloca 1 trampa potente en la próxima casilla del enemigo (3 de daño) \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 8:
                        infoText.Text = "\nEl  Dado Tóxico  tiene: \n\n" +
                            "- 4 caras que provocan 1 de daño al enemigo cada vez que se mueva durante 3 turnos \n\n" +
                            "- 1 cara que provocan 2 de daño al enemigo cada vez que se mueva durante 2 turnos \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 9:
                        infoText.Text = "\nEl  Dado de Ataque Rápido  tiene: \n\n" +
                            "- 3 caras que provocan 1 punto de daño al enemigo \n\n" +
                            "- 2 caras que provocan 2 puntos de daño al enemigo \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 10:
                        infoText.Text = "\nEl  Dado de Ralentí  tiene: \n\n" +
                            "- 3 caras que restan 1 casilla al próximo movimiento del enemigo \n\n" +
                            "- 2 caras que provocan 2 puntos de daño al enemigo \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 11:
                        infoText.Text = "\nEl  Dado de Ataque a Distancia  tiene: \n\n" +
                            "- 4 caras que provocan 1 punto de daño al enemigo \n\n" +
                            "- 1 cara que restan 2 casillas al próximo movimiento del enemigo \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 12:
                        infoText.Text = "\nEl  Dado de Azaroso  tiene: \n\n" +
                            "- 1 cara que hace 8 de daño al enemigo \n\n" +
                            "- 5 caras vacías sin efecto";
                        break;
                    default:
                        break;
                }
            }
        }
        private void Dice2_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;

            infoIcon.Source = dice2.Source;
            infoText.FontSize = 90;
            infoText.Foreground = new SolidColorBrush(Colors.Black);
            infoText.FontFamily = new FontFamily("Aclonica");

            if (dicePriority)
            {
                if (prioNum2 > 0 && prioNum2 < 7)
                {
                    infoText.Text = "\nEl personaje equipado con este dado poseerá una prioridad de " + prioNum2;
                    prioSquare1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    prioSquare2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    prioSquare3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                }
            }
            else
            {
                if (actNum2 > 6 && actNum2 < 13)
                {
                    actSquare1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    actSquare2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    actSquare3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                }
                switch (actNum2)
                {
                    case 7:
                        infoText.Text = "\nEl  Dado de Trampero \b  tiene: \n\n" +
                            "- 4 caras que colocan 1 trampa en la próxima casilla del enemigo (1 de daño) \n\n" +
                            "- 1 cara que coloca 1 trampa potente en la próxima casilla del enemigo (3 de daño) \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 8:
                        infoText.Text = "\nEl  Dado Tóxico  tiene: \n\n" +
                            "- 4 caras que provocan 1 de daño al enemigo cada vez que se mueva durante 3 turnos \n\n" +
                            "- 1 cara que provocan 2 de daño al enemigo cada vez que se mueva durante 2 turnos \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 9:
                        infoText.Text = "\nEl  Dado de Ataque Rápido  tiene: \n\n" +
                            "- 3 caras que provocan 1 punto de daño al enemigo \n\n" +
                            "- 2 caras que provocan 2 puntos de daño al enemigo \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 10:
                        infoText.Text = "\nEl  Dado de Ralentí  tiene: \n\n" +
                            "- 3 caras que restan 1 casilla al próximo movimiento del enemigo \n\n" +
                            "- 2 caras que provocan 2 puntos de daño al enemigo \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 11:
                        infoText.Text = "\nEl  Dado de Ataque a Distancia  tiene: \n\n" +
                            "- 4 caras que provocan 1 punto de daño al enemigo \n\n" +
                            "- 1 cara que restan 2 casillas al próximo movimiento del enemigo \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 12:
                        infoText.Text = "\nEl  Dado de Azaroso  tiene: \n\n" +
                            "- 1 cara que hace 8 de daño al enemigo \n\n" +
                            "- 5 caras vacías sin efecto";
                        break;
                    default:
                        break;
                }
            }
        }
        private void Dice3_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;

            infoIcon.Source = dice3.Source;
            infoText.FontSize = 90;
            infoText.Foreground = new SolidColorBrush(Colors.Black);
            infoText.FontFamily = new FontFamily("Aclonica");

            if (dicePriority)
            {
                if (prioNum3 > 0 && prioNum3 < 7)
                {
                    infoText.Text = "\nEl personaje equipado con este dado poseerá una prioridad de " + prioNum3;
                    prioSquare1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    prioSquare2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    prioSquare3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                }
            }
            else
            {
                if (actNum3 > 6 && actNum3 < 13)
                {
                    actSquare1.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    actSquare2.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                    actSquare3.Source = new BitmapImage(new Uri("ms-appx:///Assets/MenuPreparacion/blue_square.png"));
                }
                switch (actNum3)
                {
                    case 7:
                        infoText.Text = "\nEl  Dado de Trampero \b  tiene: \n\n" +
                            "- 4 caras que colocan 1 trampa en la próxima casilla del enemigo (1 de daño) \n\n" +
                            "- 1 cara que coloca 1 trampa potente en la próxima casilla del enemigo (3 de daño) \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 8:
                        infoText.Text = "\nEl  Dado Tóxico  tiene: \n\n" +
                            "- 4 caras que provocan 1 de daño al enemigo cada vez que se mueva durante 3 turnos \n\n" +
                            "- 1 cara que provocan 2 de daño al enemigo cada vez que se mueva durante 2 turnos \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 9:
                        infoText.Text = "\nEl  Dado de Ataque Rápido  tiene: \n\n" +
                            "- 3 caras que provocan 1 punto de daño al enemigo \n\n" +
                            "- 2 caras que provocan 2 puntos de daño al enemigo \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 10:
                        infoText.Text = "\nEl  Dado de Ralentí  tiene: \n\n" +
                            "- 3 caras que restan 1 casilla al próximo movimiento del enemigo \n\n" +
                            "- 2 caras que provocan 2 puntos de daño al enemigo \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 11:
                        infoText.Text = "\nEl  Dado de Ataque a Distancia  tiene: \n\n" +
                            "- 4 caras que provocan 1 punto de daño al enemigo \n\n" +
                            "- 1 cara que restan 2 casillas al próximo movimiento del enemigo \n\n" +
                            "- 1 cara vacía sin efecto";
                        break;
                    case 12:
                        infoText.Text = "\nEl  Dado de Azaroso  tiene: \n\n" +
                            "- 1 cara que hace 8 de daño al enemigo \n\n" +
                            "- 5 caras vacías sin efecto";
                        break;
                    default:
                        break;
                }
            }
        }
        private void showText(int dice)
        {

        }
    }
}
