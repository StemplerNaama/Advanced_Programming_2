using System.IO;
using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the close command class
    /// </summary>
    /// <seealso cref="ServerConnection.ICommand" />
    public class CloseGameCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseGameCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseGameCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// execute the command that the client sent
        /// </summary>
        /// <param name="args">The arguments of the command</param>
        /// <param name="client">The client.</param>
        /// <returns>"close" to the client that asks for closing</returns>
        public string Execute(string[] args, TcpClient client)
        {
            //finding the opponent of the client
            TcpClient opponent = model.close(client);
            NetworkStream stream = opponent.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            //sending close to opponent
            writer.Write("close");
            writer.Flush();
            return "closeMyself";
        }
    }
}
