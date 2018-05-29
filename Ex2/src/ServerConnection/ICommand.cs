using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the interface of the commands
    /// </summary>
    interface ICommand
    {
        /// <summary>
        /// Executes the commands that the client sent
        /// </summary>
        /// <param name="args">The arguments of the commands.</param>
        /// <param name="client">The client that sent the command.</param>
        /// <returns>a string of the result to the client</returns>
        string Execute(string[] args, TcpClient client = null);
    }
}
