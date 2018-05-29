using MazeLib;
using System.Net.Sockets;

namespace WebApp
{
    /// <summary>
    /// the Game class that contains the clients and the maze that they are playing
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The maze
        /// </summary>
        private Maze gameMaze;
        /// <summary>
        /// clientA the started the game
        /// </summary>
        private string clientA;
        /// <summary>
        /// clientB that joined the game
        /// </summary>
        private string clientB;
        /// <summary>
        /// bool member to indicate if there are 2 players in the game
        /// </summary>
        private bool hasTwoPlayers;
        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="gameMaze">The game maze.</param>
        /// <param name="clientA">first client</param>
        public Game(Maze gameMaze, string clientA)
        {
            this.gameMaze = gameMaze;
            this.clientA = clientA;
            this.clientB = null;
            this.hasTwoPlayers = false;
        }
        /// <summary>
        /// Gets or sets the client a.
        /// </summary>
        /// <value>
        /// The client a.
        /// </value>
        public string ClientA
        {
            get { return clientA; }
            set { clientA = value; }
        }
        /// <summary>
        /// Gets or sets the client b.
        /// </summary>
        /// <value>
        /// The client b.
        /// </value>
        public string ClientB
        {
            get { return clientB; }
            /*when initializing clientB (who joines the game)-
            update the bool member to mean that there are 2 players*/
            set
            {
                clientB = value;
                hasTwoPlayers = true;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has two players.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has two players; otherwise, <c>false</c>.
        /// </value>
        public bool HasTwoPlayers
        {
            get { return hasTwoPlayers; }
            set { hasTwoPlayers = value; }
        }
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze
        {
            get { return gameMaze; }
        }
    }
}