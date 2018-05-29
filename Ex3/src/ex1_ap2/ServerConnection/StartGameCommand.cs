using System.Net.Sockets;
using System.Threading;

namespace ServerConnection
{
    /// <summary>
    /// the start command class 
    /// </summary>
    /// <seealso cref="ServerConnection.ICommand" />
    public class StartGameCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="StartGameCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public StartGameCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the commands that the client sent
        /// </summary>
        /// <param name="args">The arguments of the commands.</param>
        /// <param name="client">The client that sent the command.</param>
        /// <returns>a string of the result to the client</returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            //getting the game that was created
            Game game = model.start(name, rows, cols, client);
            //looping until we find out that a player connected
            while (!game.HasTwoPlayers)
            {
                Thread.Sleep(1000);
            }
            return game.Maze.ToJSON();
        }
    }
}