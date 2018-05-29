using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiGame
{
    /// <summary>
    /// setting vm
    /// </summary>
    /// <seealso cref="GuiGame.ViewModel" />
    public class SettingsViewModel : ViewModel
    {
        /// <summary>
        /// The model
        /// </summary>
        private ISettingsModel model;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }
        
        /// <summary>
        /// Gets or sets the vm server ip.
        /// </summary>
        /// <value>
        /// The vm server ip.
        /// </value>
        public string VM_ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }
        
        /// <summary>
        /// Gets or sets the vm server port.
        /// </summary>
        /// <value>
        /// The vm server port.
        /// </value>
        public int VM_ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }
        
        /// <summary>
        /// Gets or sets the vm maze rows.
        /// </summary>
        /// <value>
        /// The vm maze rows.
        /// </value>
        public int VM_MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }
        
        /// <summary>
        /// Gets or sets the vm maze cols.
        /// </summary>
        /// <value>
        /// The vm maze cols.
        /// </value>
        public int VM_MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }
        
        /// <summary>
        /// Gets or sets the vm search algorithm.
        /// </summary>
        /// <value>
        /// The vm search algorithm.
        /// </value>
        public int VM_SearchAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set
            {
                model.SearchAlgorithm = value;
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }
        
        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
