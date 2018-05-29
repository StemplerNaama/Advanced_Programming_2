using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClientConnection;
using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GuiGame
{
    /// <summary>
    /// single model
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SingleModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The sol
        /// </summary>
        private String sol;
        
        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;
        
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        
        /// <summary>
        /// The client
        /// </summary>
        private Client client;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleModel"/> class.
        /// </summary>
        public SingleModel() {}

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sends the command.
        /// </summary>
        /// <param name="generateCommand">The generate command.</param>
        /// <param name="commandID">The command identifier.</param>
        public void SendCommand(string generateCommand, int commandID)
        {
            client = new Client(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            client.ClientConnect();
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.TcpCl.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                //send command to server
                writer.Write(generateCommand);
                writer.Flush();
                // Get result from server
                string result = reader.ReadString();
                //if generate command
                if (commandID == 0)
                {
                    maze = Maze.FromJSON(result);
                    MazeName = maze.Name;
                    MazeInitialPos = maze.InitialPos;
                }
                //if solve command
                else if (commandID == 1)
                {
                    JObject solJson = JObject.Parse(result);
                    MazeSol = (string)solJson["Solution"];
                }
            }
            //finishing the connection
            Console.WriteLine("client gets closed");
            client.ClientDisconnect();
        }

        // public string MazeName { get; set; }
        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("MazeName");
            }
        }
        
        /// <summary>
        /// Gets the maze game.
        /// </summary>
        /// <value>
        /// The maze game.
        /// </value>
        public Maze MazeGame
        {
            get { return maze; }
        }
        
        /// <summary>
        /// Gets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        public int MazeRows
        {
            get { return maze.Rows; }
        }
        
        /// <summary>
        /// Gets the maze cols.
        /// </summary>
        /// <value>
        /// The maze cols.
        /// </value>
        public int MazeCols
        {
            get { return maze.Cols; }
        }
        
        /// <summary>
        /// Gets the maze string.
        /// </summary>
        /// <value>
        /// The maze string.
        /// </value>
        public string MazeString
        {
            get { return maze.ToString(); }
        }
        
        /// <summary>
        /// Gets the maze goal position.
        /// </summary>
        /// <value>
        /// The maze goal position.
        /// </value>
        public Position MazeGoalPos
        {
           get { return maze.GoalPos; }
        }

        /// <summary>
        /// Gets or sets the maze initial position.
        /// </summary>
        /// <value>
        /// The maze initial position.
        /// </value>
        public Position MazeInitialPos
        {
            get { return maze.InitialPos; }
            set
            {
                NotifyPropertyChanged("MazeInitialPos");
            }
        }
        
        /// <summary>
        /// Gets or sets the maze sol.
        /// </summary>
        /// <value>
        /// The maze sol.
        /// </value>
        public string MazeSol
        {
            get { return sol; }
            set
            {
                sol = value;
                NotifyPropertyChanged("MazeSol");
            }
        }
    }
}
