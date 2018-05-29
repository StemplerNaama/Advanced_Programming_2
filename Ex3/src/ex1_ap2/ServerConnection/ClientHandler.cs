using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServerConnection
{
    /// <summary>
    /// the class that handles the client connection
    /// </summary>
    /// <seealso cref="ServerConnection.IClientHandler" />
    public class ClientHandler : IClientHandler
    {
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="controller">The controller.</param>
        public void HandleClient(TcpClient client, Controller controller)
        {
            //creating a new thread to handle the client
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    //continue the connection, unless a single command came
                    while (true)
                    {
                        //gets string from client
                        string commandLine = reader.ReadString();
                        //gets the result from the controller
                        string result = controller.ExecuteCommand(commandLine, client);
                        //sending the client the answer
                        writer.Write(result);
                        writer.Flush();
                        //if the command was single or a close command- finish the connection
                        if (commandLine.Contains("generate") || commandLine.Contains("solve") || 
                        commandLine.Contains("list") || commandLine.Contains("close") ||
                        (commandLine.Contains("join") && !result.Contains("Name")))
                        {
                            break;
                        }
                    }
                }
            }).Start();
        }
    }
}