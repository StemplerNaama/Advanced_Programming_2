using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// the bfs solution class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.PrioritySearcher{T}" />
    public class Bfs<T> : PrioritySearcher<T>
    {
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="Bfs{T}"/> class.
        /// </summary>
        public Bfs() { }
        /// <summary>
        /// Solve the problem by the algorithm
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>the solution of the problem</returns>
        public override Solution<T> search(ISearchable<T> searchable)
        {
            addToContainer(searchable.getInitialState()); // inherited from Searcher
            HashSet<State<T>> closed = new HashSet<State<T>>();
            State<T> goalState = searchable.getGoalState();
            State<T> n;
            while (OpenListSize > 0)
            {
                n = popContainer(); // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(goalState))
                {
                    State<T>.StatePool.clearDictionary();
                    return backTrace(n); // private method, back traces through the parents
                }
                // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s))
                    {
                        if (!openContaines(s))
                        {
                            s.CameFrom = n;
                            double tempCost = searchable.costOfEdge(n, s);
                            s.Cost = tempCost;
                            addToContainer(s);
                        }
                        else
                        {
                            double tempCost = searchable.costOfEdge(n, s) + n.Cost;
                            if (tempCost < s.Cost) //if this new path is better than previous one
                            {
                                s.Cost = tempCost;
                                s.CameFrom = n;
                                removeFromContainer(s);
                                addToContainer(s);
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}