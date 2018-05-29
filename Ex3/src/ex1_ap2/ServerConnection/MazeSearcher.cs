using System.Collections.Generic;
using MazeLib;
using SearchAlgorithmsLib;

namespace ServerConnection
{
    /// <summary>
    /// adapter class for the maze problem
    /// </summary>
    /// <seealso cref="SearchAlgorithmsLib.ISearchable{MazeLib.Position}" />
    public class MazeSearcher : ISearchable<Position>
    {
        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="MazeSearcher"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public MazeSearcher(Maze maze)
        {
            this.maze = maze;
        }
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>the initial state</returns>
        public State<Position> getInitialState()
        {
            State<Position> init;
            init = State<Position>.StatePool.GetState(maze.InitialPos);
            return init;
        }
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns>return the goal state</returns>
        public State<Position> getGoalState()
        {
            State<Position> goal;
            goal = State<Position>.StatePool.GetState(maze.GoalPos);
            return goal;
        }
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The state to find its neighbors.</param>
        /// <returns>the list of the neighbors of the state</returns>
        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            List<State<Position>> possibleStates = new List<State<Position>>();
            State<Position> cameFrom = s.CameFrom; 
            Position currentPos = s.StateDescription;
            int col = currentPos.Col;
            int row = currentPos.Row;
            Position temp;
            State<Position> right = null, left = null, up = null, down = null;
            //the conditions for the neighbors
            if (col != 0 && row !=0 && col != maze.Cols-1 && row != maze.Rows-1)
            {
                right = State<Position>.StatePool.GetState(new Position(row, col + 1));
                left = State<Position>.StatePool.GetState(new Position(row, col - 1));
                up = State<Position>.StatePool.GetState(new Position(row + 1, col));
                down = State<Position>.StatePool.GetState(new Position(row - 1, col));
            }
            else if (col == 0)
            {
                temp = new Position(row, col+1);
                right = State<Position>.StatePool.GetState(temp);
                //down is possible
                if (row != 0)
                {
                    temp = new Position(row - 1, col);
                    down = State<Position>.StatePool.GetState(temp);
                }
                if (row != maze.Rows-1)
                {
                    temp = new Position(row + 1, col);
                    up = State<Position>.StatePool.GetState(temp);
                }
            }
            else if (row == 0)
            {
                temp = new Position(row+1, col);
                up = State<Position>.StatePool.GetState(temp);
                if(col != 0)
                {
                    temp = new Position(row, col - 1);
                    left = State<Position>.StatePool.GetState(temp);
                }
                if (col != maze.Cols-1)
                {
                    temp = new Position(row, col + 1);
                    right = State<Position>.StatePool.GetState(temp);
                }
            }
            else if (col == maze.Cols-1)
            {
                temp = new Position(row, col - 1);
                left = State<Position>.StatePool.GetState(temp);
                //down is possible
                if (row != 0)
                {
                    temp = new Position(row - 1, col);
                    down = State<Position>.StatePool.GetState(temp);
                }
                if (row != maze.Rows - 1)
                {
                    temp = new Position(row + 1, col);
                    up = State<Position>.StatePool.GetState(temp);
                }
            }
            else if (row == maze.Rows - 1)
            {
                temp = new Position(row - 1, col);
                down = State<Position>.StatePool.GetState(temp);
                if (col != 0)
                {
                    temp = new Position(row, col - 1);
                    left = State<Position>.StatePool.GetState(temp);
                }
                if (col != maze.Cols - 1)
                {
                    temp = new Position(row, col + 1);
                    right = State<Position>.StatePool.GetState(temp);
                }
            }
            //if the states are free
            if(right != null && maze[right.StateDescription.Row, right.StateDescription.Col] == CellType.Free)
                possibleStates.Add(right);
            if (left != null && maze[left.StateDescription.Row, left.StateDescription.Col] == CellType.Free)
                possibleStates.Add(left);
            if (up != null && maze[up.StateDescription.Row, up.StateDescription.Col] == CellType.Free)
                possibleStates.Add(up);
            if (down != null && maze[down.StateDescription.Row, down.StateDescription.Col] == CellType.Free)
                possibleStates.Add(down);
            //if we added the previous node- remove it
            if (possibleStates.Contains(cameFrom))
                possibleStates.Remove(cameFrom);
            return possibleStates;
        }

        /// <summary>
        /// Costs the of edge.
        /// </summary>
        /// <param name="s1">stateA.</param>
        /// <param name="s2">stateB.</param>
        /// <returns>the cost of the edge between the states</returns>
        public double costOfEdge(State<Position> s1, State<Position> s2)
        {
            if (s1.Equals(s2))
                return 0;
            else
                return 1;
        }
    }
}
