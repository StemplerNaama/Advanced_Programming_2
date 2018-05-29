using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClientConnection;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace GuiGame
{
    /// <summary>
    /// multiplayer model
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class MultiModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The stop
        /// </summary>
        volatile Boolean stop;

        /// <summary>
        /// My direction
        /// </summary>
        volatile string myDirection;

        /// <summary>
        /// The other direction
        /// </summary>
        volatile string otherDirection;

        /// <summary>
        /// The games
        /// </summary>
        private ObservableCollection<string> games;

        /// <summary>
        /// The close sig
        /// </summary>
        private int closeSig;

        /// <summary>
        /// The writer
        /// </summary>
        BinaryWriter writer;

        /// <summary>
        /// The other position
        /// </summary>
        private Position otherPos;

        /// <summary>
        /// The client
        /// </summary>
        private Client client;

        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;

        /// <summary>
        /// The name
        /// </summary>
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiModel"/> class.
        /// </summary>
        public MultiModel()
        {
            games = new ObservableCollection<string>();
            closeSig = 0;
            myDirection = null;
            otherDirection = null;
            stop = false;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [initialize position property changed].
        /// </summary>
        public event PropertyChangedEventHandler InitPosPropertyChanged;

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
            NetworkStream stream = client.TcpCl.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
            //send command to server
            writer.Write(generateCommand);
            writer.Flush();
            // Get result from server
            string result = reader.ReadString();
            //if start command
            if (commandID == 0)
            {
                Console.WriteLine("start");
                maze = Maze.FromJSON(result);
                MazeName = maze.Name;
                OtherCurrentPos = maze.InitialPos;
            }
            //if join command
            else if (commandID == 1)
            {
                Console.WriteLine("join");
                maze = Maze.FromJSON(result);
                MazeName = maze.Name;
                OtherCurrentPos = maze.InitialPos;
            }
            //if list command
            else if (commandID == 2)
            {
                //from json
                client.ClientDisconnect();
                if (result == "there is no games to join")
                    GamesList = null;
                else
                {
                    //making a list of games names to join
                    List<string> tempList = new List<string>();
                    JArray jsonGames = JArray.Parse(result);
                    foreach (string game in jsonGames)
                    {
                        if (!tempList.Contains(game))
                            tempList.Add(game);
                    }
                    GamesList = new ObservableCollection<string>(tempList);
                }
                return;
            }

            //creating thread for reading
            Task tReader = new Task(() =>
            {
                while (true)
                {
                    //getting the result from the server
                    string tempCommand = reader.ReadString();
                    Console.WriteLine("in reader: " + tempCommand);

                    //message from the server to the client who sent close command
                    if (tempCommand.Equals("closeMyself"))
                    {
                        client.ClientDisconnect();
                        Console.WriteLine("my client gets closed");
                        break;
                    }
                    //message from the server to the client who gets close from partner
                    if (tempCommand.Equals("close"))
                    {
                        Console.WriteLine("other client forced to be closed");
                        CloseSignal = 1;
                        client.ClientDisconnect();
                        break;
                    }
                    //if it's move command
                    if (!tempCommand.Equals("play"))
                    { //ignore if it sent to the client who asked send the move
                        JObject jsonDirection = JObject.Parse(tempCommand);
                        OtherDirectionCommand = (string)jsonDirection["Direction"];
                        Console.WriteLine("command: " + OtherDirectionCommand);
                        UpdateOtherPos(OtherDirectionCommand);
                    }
                }
            });
            tReader.Start();
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
        /// Initializes the notify property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void InitNotifyPropertyChanged(string propName)
        {
            if (this.InitPosPropertyChanged != null)
                this.InitPosPropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Gets or sets the games list.
        /// </summary>
        /// <value>
        /// The games list.
        /// </value>
        public ObservableCollection<string> GamesList
        {
            get { return games; }
            set
            {
                games = value;
                NotifyPropertyChanged("GamesList");
            }
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
                InitNotifyPropertyChanged("OtherCurrentPos");
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
        /// Gets or sets the close signal.
        /// </summary>
        /// <value>
        /// The close signal.
        /// </value>
        public int CloseSignal
        {
            get { return closeSig; }
            set
            {
                closeSig = value;
                Console.WriteLine("notify closing in model");
                NotifyPropertyChanged("CloseSignal");
            }
        }

        /// <summary>
        /// Gets or sets my direction command.
        /// </summary>
        /// <value>
        /// My direction command.
        /// </value>
        public string MyDirectionCommand
        {
            get { return myDirection; }
            set
            {
                myDirection = value;
                if (MyDirectionCommand != null)
                {
                    writer.Write(MyDirectionCommand);
                    writer.Flush();
                }
            }
        }

        /// <summary>
        /// Gets or sets the other direction command.
        /// </summary>
        /// <value>
        /// The other direction command.
        /// </value>
        public string OtherDirectionCommand
        {
            get { return otherDirection; }
            set
            {
                otherDirection = value;
            }
        }

        /// <summary>
        /// Gets or sets the other current position.
        /// </summary>
        /// <value>
        /// The other current position.
        /// </value>
        public Position OtherCurrentPos
        {
            get { return otherPos; }
            set
            {
                otherPos = value;
                NotifyPropertyChanged("OtherCurrentPos");
            }
        }

        /// <summary>
        /// Updates the other position.
        /// </summary>
        /// <param name="otherDirection">The other direction.</param>
        public void UpdateOtherPos(string otherDirection)
        {
            switch (otherDirection)
            {
                case "left":
                    otherPos.Col -= 1;
                    OtherCurrentPos = otherPos;
                    break;
                case "right":
                    otherPos.Col += 1;
                    OtherCurrentPos = otherPos;
                    break;
                case "up":
                    otherPos.Row -= 1;
                    OtherCurrentPos = otherPos;
                    break;
                case "down":
                    otherPos.Row += 1;
                    OtherCurrentPos = otherPos;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Closes the command.
        /// </summary>
        public void CloseCommand()
        {
            writer.Write("close");
            writer.Flush();
        }
    }
}
