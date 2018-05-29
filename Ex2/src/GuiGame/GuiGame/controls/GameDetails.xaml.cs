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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuiGame
{
    /// <summary>
    /// Interaction logic for GameDetails.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class GameDetails : UserControl
    {
        /// <summary>
        /// The setting vm
        /// </summary>
        private SettingsViewModel setvm;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDetails"/> class.
        /// </summary>
        public GameDetails()
        {
            InitializeComponent();
            setvm = new SettingsViewModel(new SettingsModel());
            this.DataContext = setvm;
        }

    }
}