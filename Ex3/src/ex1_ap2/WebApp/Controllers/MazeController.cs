using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using ServerConnection;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// maze controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class MazeController : ApiController
    {
        /// <summary>
        /// My model
        /// </summary>
        private static IMazeModel myModel = new MazeModel();

        // GET: api/Maze
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        public JObject GetMaze(string name, int rows, int cols)
        {
            Maze maze = myModel.generate(name, rows, cols);
            JObject obj = JObject.Parse(maze.ToJSON());
            return obj;
        }

        // GET: api/Maze/GetSolve
        /// <summary>
        /// Gets the solve.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="alg">The alg.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Maze/{name}/{alg}")]
        public JObject GetSolve(string name, int alg)
        {
            ISearcher<Position> algorithm;
            if (alg == 0)
                //BFS way
                algorithm = new Bfs<Position>();
            else
                //DFS way
                algorithm = new Dfs<Position>();
            String mazeSolve = myModel.solve(name, algorithm);
            JObject solveObj = new JObject();
            solveObj["Solution"] = mazeSolve;
            return solveObj;
        }


        // GET: api/Maze
        /// <summary>
        /// Starts the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public JObject StartMaze(string name, int rows, int cols, string id)
        {
            //getting the game that was created
            Game game = myModel.start(name, rows, cols, id);
            //looping until we find out that a player connected
            while (!game.HasTwoPlayers)
            {
                Thread.Sleep(1000);
            }
            JObject obj = JObject.Parse(game.Maze.ToJSON());
            return obj;
        }

        // GET: api/Maze/5
        /// <summary>
        /// Joins the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Maze/JoinMaze/{name}/{id}")]
        public JObject JoinMaze(string name, string id)
        {
            //getting the game that was created
            Game joinedGame = myModel.join(name, id);
            JObject obj = JObject.Parse(joinedGame.Maze.ToJSON());
            return obj;
        }

        // GET: api/Maze
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JObject GetList()
        {
            List<string> availableGames = myModel.list();
            if (availableGames.Count == 0)
                return null;
            JObject json = new JObject();
            json["games"] = JToken.FromObject(availableGames);
            //return "there is no games to join";
            return json;
        }

        // POST: api/Maze
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Maze/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Maze/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }
    }
}
