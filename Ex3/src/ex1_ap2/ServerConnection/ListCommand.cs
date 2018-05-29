using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the list command class 
    /// </summary>
    /// <seealso cref="ServerConnection.ICommand" />
    public class ListCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="ListCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ListCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the commands that the 
        /// sent
        /// </summary>
        /// <param name="args">The arguments of the commands.</param>
        /// <param name="client">The client that sent the command.</param>
        /// <returns>
        /// a string of the available games to join to the client
        /// </returns>
        public string Execute(string[] args, TcpClient client)
        {
            List<string> availableGames = model.list();
            if (availableGames.Count == 0)
                return "there is no games to join";
            string json = JsonConvert.SerializeObject(availableGames);
            return json;
        }
    }
}
