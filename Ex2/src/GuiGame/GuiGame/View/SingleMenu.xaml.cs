using System;
using System.Collections.Generic;
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
    /// Interaction logic for SingleMenu.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SingleMenu : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private StartSingleGameViewModel vm;
        
        /// <summary>
        /// The setvm
        /// </summary>
        private SettingsViewModel setvm;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleMenu"/> class.
        /// </summary>
        public SingleMenu()
        {
            InitializeComponent();
            setvm = new SettingsViewModel(new SettingsModel());
            this.DataContext = setvm;
        }

        /// <summary>
        /// Handles the Click event of the StartGameBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            vm = new StartSingleGameViewModel(new SingleModel());
            //getting the game details
            string name = gameDetails.mazeNameTxtBox.Text;
            string rows = gameDetails.mazeRowsTxtBox.Text;
            string cols = gameDetails.mazeColsTxtBox.Text;

            //sending start command to the model through the vm
            vm.StartGame(name, rows, cols);
            SingleGame win = new SingleGame(vm);
            win.Show();
            this.Close();
            Application.Current.MainWindow.Hide();
        }
    }
}
