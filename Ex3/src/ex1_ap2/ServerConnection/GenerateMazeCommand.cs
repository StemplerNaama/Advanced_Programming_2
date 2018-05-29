using MazeLib;
using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the generate command class 
    /// </summary>
    /// <seealso cref="ServerConnection.ICommand" />
    public class GenerateMazeCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// execute the command that the 
        /// sent
        /// </summary>
        /// <param name="args">The arguments of the command</param>
        /// <param name="client">The client.</param>
        /// <returns>a string of the maze</returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.generate(name, rows, cols);
            return maze.ToJSON();
        }
    }
}
