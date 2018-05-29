using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the class that in charge of connecting between model and viewer
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// The commands and their objects
        /// </summary>
        private Dictionary<string, ICommand> commands;
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
        {
            model = new Model();
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveMazeCommand(model));
            commands.Add("start", new StartGameCommand(model));
            commands.Add("join", new JoinGameCommand(model));
            commands.Add("list", new ListCommand(model));
            commands.Add("play", new PlayGameCommand(model));
            commands.Add("close", new CloseGameCommand(model));
        }
        /// <summary>
        /// Executes the command that has been sent from the user
        /// </summary>
        /// <param name="commandLine">The command line from a 
        /// .</param>
        /// <param name="client">The client.</param>
        /// <returns>a result to the client to print</returns>
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
    }
}
