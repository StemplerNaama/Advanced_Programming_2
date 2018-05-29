using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApp
{
    /// <summary>
    /// gamehub class
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    public class GameHub : Hub
    {
        /// <summary>
        /// The connected users
        /// </summary>
        private static ConcurrentDictionary<string, List<string>> connectedUsers =
            new ConcurrentDictionary<string, List<string>>();

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void StartGame(string mazeName)
        {
            //if maze already exists 
            if (!connectedUsers.ContainsKey(mazeName))
            {
                connectedUsers[mazeName] = new List<string> {Context.ConnectionId};
            }
        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void JoinGame(string mazeName)
        {
            //if maze already exists 
            if (connectedUsers.ContainsKey(mazeName))
            {
                connectedUsers[mazeName].Add(Context.ConnectionId);
            }
        }

        /// <summary>
        /// Sends the move.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="cellWidth">Width of the cell.</param>
        /// <param name="cellHeight">Height of the cell.</param>
        /// <param name="playerRowPos">The player row position.</param>
        /// <param name="playerColPos">The player col position.</param>
        /// <param name="exitRow">The exit row.</param>
        /// <param name="exitCol">The exit col.</param>
        public void SendMove(string direction,  string mazeName, int cellWidth, int cellHeight, 
            int playerRowPos, int playerColPos, int exitRow, int exitCol)
        {
            string partnerId;
            if (connectedUsers[mazeName][0] == Context.ConnectionId)
                partnerId = connectedUsers[mazeName][1] ;
            else
                partnerId = connectedUsers[mazeName][0];
            Clients.Client(partnerId).getOppStep(direction, cellWidth, cellHeight, playerRowPos,
                playerColPos, exitRow, exitCol);
        }
    }
}
