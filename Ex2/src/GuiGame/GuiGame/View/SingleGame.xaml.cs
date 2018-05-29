using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for SingleGame.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SingleGame : Window
    {
        /// <summary>
        /// The menu vm
        /// </summary>
        private StartSingleGameViewModel menuVm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleGame"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public SingleGame(StartSingleGameViewModel vm)
        {
            InitializeComponent();
            menuVm = vm;
            DataContext = menuVm;
        }

        /// <summary>
        /// Handles the Click event of the mainMenuBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are You Sure?","confirmation",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                this.Close();
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
            menuVm.MovePlayer(direction);
            //if we reached to goal pos
            if(mazeBoard.CurrentPos == mazeBoard.GoalPos)
            {
                EndGame win = new EndGame();
                win.ShowDialog();
                this.Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the solveMazeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SolveMazeBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = menuVm.VM_MazeName;
            string algoType = Properties.Settings.Default.SearchAlgorithm.ToString();
            menuVm.SolveMaze(name, algoType);
            menuVm.MovePlayerInSol();
            EndGame win = new EndGame();
            win.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the RestartMazeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RestartMazeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                menuVm.DoRestart();
        }

        /// <summary>
        /// Handles the Closed event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
        }
    }
}
