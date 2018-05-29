using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Configuration;

namespace ServerConnection
{
    /// <summary>
    /// class of the server
    /// </summary>
    public class Server
    {
        /// <summary>
        /// The server controller
        /// </summary>
        private Controller serverController;
        /// <summary>
        /// The listener
        /// </summary>
        private TcpListener listener;
        /// <summary>
        /// The ch
        /// </summary>
        private IClientHandler ch;
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="ch">The ch.</param>
        public Server(IClientHandler ch)
        {
            this.ch = ch;
            serverController = new Controller();
        }
        /// <summary>
        /// Starts the server to listen to clients
        /// </summary>
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings[0]),
                int.Parse(ConfigurationManager.AppSettings[1]));
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client, serverController);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }
        /// <summary>
        /// Stops the server.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }

    }
}
