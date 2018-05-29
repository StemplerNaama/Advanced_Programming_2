using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the class of the model - the in charge of the computing and having the dictionaries
    /// </summary>
    /// <seealso cref="ServerConnection.IModel" />
    public class Model : IModel
    {
        /// <summary>
        /// The solutions dictionary- key:name of game, value: its solution
        /// </summary>
        private Dictionary<string, string> solutions;
        /// <summary>
        /// All mazes
        /// </summary>
        private Dictionary<string, Maze> allMazes;
        /// <summary>
        /// The games
        /// </summary>
        private Dictionary<string, Game> games;
        /// <summary>
        /// The available games
        /// </summary>
        private List<string> availableGames;
        /// <summary>
        /// The playing clients
        /// </summary>
        private Dictionary<TcpClient, string> playingClients;
        //private Dictionary<string, List<TcpClient>> twoPlayersGames;


        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        public Model() {
            solutions = new Dictionary<string, string>();
            allMazes = new Dictionary<string, Maze>();
            games = new Dictionary<string, Game>();
            playingClients = new Dictionary<TcpClient, string>();
            availableGames = new List<string>();
        }
        /// <summary>
        /// Generates a maze with a name that is given, size according the parameters
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="rows">num of rows.</param>
        /// <param name="cols">num of cols.</param>
        /// <returns>
        /// a new maze
        /// </returns>
        public Maze generate(string name, int rows, int cols)
        {
            DFSMazeGenerator maze = new DFSMazeGenerator();
            Maze currentMaze = maze.Generate(rows, cols);
            currentMaze.Name = name; //this is the way?
            allMazes.Add(name, currentMaze);
            return currentMaze;
        }
        /// <summary>
        /// Solves the game according the type of algorithm
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="algorithm">The algorithm we want to solve by.</param>
        /// <returns>
        /// a string of the solution
        /// </returns>
        public string solve(string name, ISearcher<Position> algorithm)
        {
            if (!allMazes.ContainsKey(name))
                return "the maze doesn't exist in the server";
            if (solutions.ContainsKey(name))
                return solutions[name];
            else
            {
                Maze currentMaze = allMazes[name];
                MazeSearcher newMaze = new MazeSearcher(currentMaze);
                Solution<Position> sol = algorithm.search(newMaze);
                //translate the solution to string
                string solString = solutionToString(sol);
                solutions.Add(name, solString);
                return solString;
            }

        }
        /// <summary>
        /// waiting for a partner to join
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="rows">num of rows.</param>
        /// <param name="cols">num of cols.</param>
        /// <param name="client">The client.</param>
        /// <returns>the game that was created</returns>
        public Game start(string name, int rows, int cols, TcpClient client)
        {
            //creating the maze, initialize a game and return it to the StartGameCommand
            DFSMazeGenerator maze = new DFSMazeGenerator();
            Maze currentMaze = maze.Generate(rows, cols);
            //setting the game name
            currentMaze.Name = name;
            //creating a new game with the maze
            Game currentGame = new Game(currentMaze, client);
            games.Add(name, currentGame);
            //add the games to the list of the vailable games
            availableGames.Add(name);
            playingClients.Add(client, name);
            return currentGame;
        }
        /// <summary>
        /// List of the available games to join
        /// </summary>
        /// <returns>the list of the avaialble games</returns>
        public List<string> list()
        {
            return availableGames;
        }
        /// <summary>
        /// Joins the specified game name.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="client">The client that join.</param>
        /// <returns>
        /// the game that has been joined
        /// </returns>
        public Game join(string name, TcpClient client)
        {
            if (!games.ContainsKey(name) || (null != games[name].ClientB))
                //game doesn't exist or is already assigned
                return null;
            games[name].ClientB = client;
            //remove the game from available games list
            availableGames.Remove(name);
            playingClients.Add(client, name);
            return games[name];
        }
        /// <summary>
        /// Play command- finding the opponent
        /// </summary>
        /// <param name="direct">The direct.</param>
        /// <param name="client">The client.</param>
        /// <returns>the game to join</returns>
        public Game play(string direct, TcpClient client)
        {
            //finding the specific game
            string name;
            Game currentGame;
            //if client exists
            if (playingClients.ContainsKey(client))
            {
                name = playingClients[client];
                currentGame = games[name];
                //identyfing the opponent
                return currentGame;
            }
            return null;
        }
        /// <summary>
        /// finding the game to close
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>the game to close </returns>
        public TcpClient close(TcpClient client)
        {
            TcpClient opponent = null;
            string gameName;
            Game currentGame;
            //searching what game to close
            if (playingClients.ContainsKey(client))
            {
                gameName = playingClients[client];
                currentGame = games[gameName];
                if (currentGame.ClientA.Equals(client))
                    opponent = currentGame.ClientB;
                else
                    opponent = currentGame.ClientA;
            }
            return opponent;
        }
        /// <summary>
        /// translate the solution to string
        /// </summary>
        /// <param name="sol">The solution.</param>
        /// <returns>a string describing the solution</returns>
        public string solutionToString(Solution<Position> sol)
        {
            string solString = null;
            int size = sol.solListSize();
            Position var1, var2;
            List<Position> solList = sol.SolList;
            for (int i=0; i< size -1; i++)
            {
                var1 = solList[i];
                var2 = solList[i+1];
                if (var1.Row == var2.Row)
                {
                    if (var1.Col > var2.Col)
                    {
                        //go left
                        solString += "0";
                    }
                    else
                    {
                        //go right
                        solString += "1";
                    }
                } else if (var1.Row > var2.Row)
                {
                    //go up
                    solString += "2";
                }
                else
                {
                    //go down
                    solString += "3";
                }
            }
            return solString;
        }
    }
}
