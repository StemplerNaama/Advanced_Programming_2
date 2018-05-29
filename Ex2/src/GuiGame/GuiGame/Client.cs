using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnection
{
    /// <summary>
    /// 
    /// </summary>
    public class Client
    {
        /// <summary>
        /// The ep
        /// </summary>
        private IPEndPoint ep;

        /// <summary>
        /// The client
        /// </summary>
        private TcpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public Client(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
        }

        /// <summary>
        /// Clients the connect.
        /// </summary>
        public void ClientConnect()
        {
            client.Connect(ep);
        }

        /// <summary>
        /// Clients the disconnect.
        /// </summary>
        public void ClientDisconnect()
        {
            this.client.Close();
        }

        /// <summary>
        /// Gets the TCP cl.
        /// </summary>
        /// <value>
        /// The TCP cl.
        /// </value>
        public TcpClient TcpCl
        {
            get { return client; }
        }
    }
}