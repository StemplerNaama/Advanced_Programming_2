using MazeLib;
using SearchAlgorithmsLib;
using System.Collections.Generic;
using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the interface of the model
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Generates a maze with a name that is given, size according the parameters
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="rows">num of rows.</param>
        /// <param name="cols">num of cols.</param>
        /// <returns>a new maze</returns>
        Maze generate(string name, int rows, int cols);
        /// <summary>
        /// Solves the game according the type of algorithm
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="algorithm">The algorithm we want to solve by.</param>
        /// <returns>a string of the solution</returns>
        string solve(string name, ISearcher<Position> algorithm);
        /// <summary>
        /// waiting for a partner to join
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="rows">>num of rows.</param>
        /// <param name="cols">>num of cols.</param>
        /// <param name="client">The client.</param>
        /// <returns>the game that was created</returns>
        Game start(string name, int rows, int cols, TcpClient client);
        /// <summary>
        /// List of the available games to join
        /// </summary>
        /// <returns>the list</returns>
        List<string> list();
        /// <summary>
        /// Joins the specified game name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        /// <returns>the game that has been joined</returns>
        Game join(string name, TcpClient client);
        /// <summary>
        /// Play command- finding the opponent 
        /// </summary>
        /// <param name="direct">The direct.</param>
        /// <param name="client">The client.</param>
        /// <returns>the game to join</returns>
        Game play(string direct, TcpClient client);
        /// <summary>
        /// finding the game to close
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>the game to close</returns>
        TcpClient close(TcpClient client);


    }
}
