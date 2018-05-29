using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace GuiGame
{
    /// <summary>
    /// Interaction logic for MultiGame.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiGame : Window
    {
        /// <summary>
        /// The menu vm
        /// </summary>
        private StartMultiGameViewModel menuVm;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiGame"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public MultiGame(StartMultiGameViewModel vm)
        {
            InitializeComponent();
            menuVm = vm;
            DataContext = menuVm;
            menuVm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Contains("VM_CloseSignal"))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        string message;
                        Console.WriteLine(menuVm.VM_OtherCurrentPos);
                        Console.WriteLine(menuVm.VM_GoalPos);
                        if (menuVm.VM_OtherCurrentPos != menuVm.VM_GoalPos)
                        {
                            message = "Your partner refused a selfie! \n so he left the game.. ";
                        }
                        else
                        {
                            message = "you are a loser!!";
                        }
                        MessageBox.Show(message, "notice", MessageBoxButton.OK);
                        this.Close();
                        Application.Current.MainWindow.Show();
                    });
                }
            };
        }

        /// <summary>
        /// Handles the KeyDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            string direction = "other";
            switch (e.Key)
            {
                case Key.Up:
                    {
                        direction = "up";
                        break;
                    }
                case Key.Down:
                    {
                        direction = "down";
                        break;
                    }
                case Key.Left:
                    {
                        direction = "left";
                        break;
                    }
                case Key.Right:
                    {
                        direction = "right";
                        break;
                    }
                default:
                    break;
            }
            menuVm.MoveMyPlayer(direction);
            if (myMazeBoard.CurrentPos == myMazeBoard.GoalPos)
            {
                EndGame win = new EndGame();
                win.ShowDialog();
                this.Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the BackToMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            menuVm.CloseMultiGame();
            this.Dispatcher.Invoke(() =>
            {
                this.Close();
                Application.Current.MainWindow.Show();
            });
        }

        /// <summary>
        /// Handles the Closing event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            menuVm.CloseMultiGame();
            this.Dispatcher.Invoke(() =>
            {
                Application.Current.MainWindow.Show();
            });
        }
    }
}
