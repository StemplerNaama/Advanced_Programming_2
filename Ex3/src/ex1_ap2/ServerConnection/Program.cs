using System;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using MazeLib;

namespace ServerConnection
{
    /// <summary>
    /// the main of the client
    /// </summary>
    class Program
    {
        /// <summary>
        /// method that creates a maze and solves it by dfs and bfs
        /// </summary>
        public static void CompareSolvers()
        {
            DFSMazeGenerator maze = new DFSMazeGenerator();
            Maze currentMaze = maze.Generate(100,100);
            Console.WriteLine(currentMaze.ToString());
            MazeSearcher newMaze = new MazeSearcher(currentMaze);
            ISearcher<Position> bfsSearch = new Bfs<Position>();
            ISearcher<Position> dfsSearch = new Dfs<Position>();
            Solution<Position> solBfs = bfsSearch.search(newMaze);
            Solution<Position> solDfs = dfsSearch.search(newMaze);
            //printing the num of evaluated nodes
            Console.WriteLine("bfs "+ bfsSearch.EvaluatedNodes);
            Console.WriteLine("dfs "+ dfsSearch.EvaluatedNodes);
            Console.ReadLine();
        }

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            //CompareSolvers();
            //creates a server to connect with clients
            IClientHandler ch = new ClientHandler();
            Server ser = new Server(ch);
            ser.Start();
            Console.ReadLine();
            ser.Stop();
        }
    }
}
