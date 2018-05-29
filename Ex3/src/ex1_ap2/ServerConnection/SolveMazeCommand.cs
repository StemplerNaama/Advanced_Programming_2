using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace ServerConnection
{
    /// <summary>
    /// the solve command class 
    /// </summary>
    /// <seealso cref="ServerConnection.ICommand" />
    public class SolveMazeCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="SolveMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the commands that the client sent
        /// </summary>
        /// <param name="args">The arguments of the commands.</param>
        /// <param name="client">The client that sent the command.</param>
        /// <returns>a string of the result to the client</returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int algorithmType = int.Parse(args[1]);
            ISearcher<Position> algorithm;
            //find what algorithm it is
            if (algorithmType == 0)
                //BFS way
                algorithm = new Bfs<Position>();
            else
                //DFS way
                algorithm = new Dfs<Position>();
            string sol = model.solve(name, algorithm);
            int nodesEvaluated = algorithm.EvaluatedNodes;
            return ToJSON(name, sol, nodesEvaluated);
        }
        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="sol">The solution.</param>
        /// <param name="nodesEvaluated">The nodes evaluated.</param>
        /// <returns>a string of the solution command</returns>
        public string ToJSON(string name, string sol, int nodesEvaluated)
        {
            JObject solveObj = new JObject();
            solveObj["Name"] = name;
            solveObj["Solution"] = sol;
            solveObj["NodesEvaluated"] = nodesEvaluated;
            return solveObj.ToString();
        }
    }
}
