using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the interface of the 
    /// handler
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// the method that handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="controller">The controller.</param>
        void HandleClient(TcpClient client, Controller controller);
    }
}
