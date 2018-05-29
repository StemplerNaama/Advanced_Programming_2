using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the join command class 
    /// </summary>
    /// <seealso cref="ServerConnection.ICommand" />
    public class JoinGameCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="JoinGameCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public JoinGameCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the command that the client sent
        /// </summary>
        /// <param name="args">The arguments of the commands.</param>
        /// <param name="client">The client that sent the command.</param>
        /// <returns>
        /// a string of the result to the client
        /// </returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            Game joinedGame = model.join(name, client);
            if (null == joinedGame)
                return "could not join";
            return joinedGame.Maze.ToJSON();
        }
    }
}
