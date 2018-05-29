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
using System.Windows.Threading;

namespace GuiGame
{
    /// <summary>
    /// Interaction logic for MultiMenu.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiMenu : Window
    {
        /// <summary>
        /// The waiting win
        /// </summary>
        private WaitingForPartner waitingWin;
        
        /// <summary>
        /// The vm
        /// </summary>
        private StartMultiGameViewModel vm;
        
        /// <summary>
        /// The setvm
        /// </summary>
        private SettingsViewModel setvm;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiMenu"/> class.
        /// </summary>
        public MultiMenu()
        {
            InitializeComponent();
            setvm = new SettingsViewModel(new SettingsModel());
            vm = new StartMultiGameViewModel(new MultiModel());
            this.DataContext = setvm;
            this.waitingWin = new WaitingForPartner();
            gamesList.DataContext = vm;
        }

        /// <summary>
        /// Handles the Click event of the StartGameBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            //view a message that we are waiting for a player
            waitingWin.Show();
            
            //getting the game details
            string name = gameDetails.mazeNameTxtBox.Text;
            string rows = gameDetails.mazeRowsTxtBox.Text;
            string cols = gameDetails.mazeColsTxtBox.Text;
            
            //sending start command to model through the vm
            vm.StartMultiGame(name, rows, cols);
            MultiGame win = new MultiGame(vm);
            win.Show();
            waitingWin.Close();
            this.Close();
            Application.Current.MainWindow.Hide();
        }

        /// <summary>
        /// Handles the Click event of the joinGameBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void JoinGameBtn_Click(object sender, RoutedEventArgs e)
        {
            int gameIndex = gamesList.SelectedIndex;
            string name = gamesList.Items[gameIndex].ToString();

            //sending join command to the model through the vm
            vm.JoinMultiGame(name);
            MultiGame win = new MultiGame(vm);
            win.Show();
            this.Close();
            Application.Current.MainWindow.Hide();
        }

        /// <summary>
        /// Handles the DropDownOpened event of the gamesList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GamesList_DropDownOpened(object sender, EventArgs e)
        {
            vm.ListCommand();
        }
    }
}
