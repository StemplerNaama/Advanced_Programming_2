using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the play command class 
    /// </summary>
    /// <seealso cref="ServerConnection.ICommand" />
    public class PlayGameCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// CTOR- Initializes a new instance of the <see cref="PlayGameCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public PlayGameCommand(IModel model)
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
            string directionCommand = args[0];
            //getting the game that the client moved there
            Game game = model.play(directionCommand, client);
            //we writing "writer.write" to the opponent
            TcpClient opponent;
            if (game.ClientA.Equals(client))
                opponent = game.ClientB;
            else
                opponent = game.ClientA;
            NetworkStream stream = opponent.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            //sending move description to the opponent
            writer.Write(ToJSON(game.Maze.Name, directionCommand));
            writer.Flush();
            return "play";
          }
        /// <summary>
        /// convert the move desription to json.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>a string representing the move</returns>
        public string ToJSON(string name, string direction)
        {
            JObject solveObj = new JObject();
            solveObj["Name"] = name;
            solveObj["Direction"] = direction;
            return solveObj.ToString();
        }
    }
}

