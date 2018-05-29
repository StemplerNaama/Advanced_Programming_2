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
    /// Interaction logic for EndGame.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class EndGame : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndGame"/> class.
        /// </summary>
        public EndGame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the continueBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
