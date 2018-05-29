using System.Collections.Generic;
using System.Linq;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// the dfs solution class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public class Dfs<T> : Searcher<T>
    {
        /// <summary>
        /// The states stack
        /// </summary>
        private Stack<State<T>> statesStack;
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="Dfs{T}"/> class.
        /// </summary>
        public Dfs()
        {
            statesStack = new Stack<State<T>>();
        }
        /// <summary>
        /// Pops the container.
        /// </summary>
        /// <returns>the last element in the stack</returns>
        public override State<T> popContainer()
        {
            evaluatedNodes++;
            return statesStack.Pop();
        }
        /// <summary>
        /// Adds to container.
        /// </summary>
        /// <param name="s">The s.</param>
        public override void addToContainer(State<T> s)
        {
            statesStack.Push(s);
        }
        /// <summary>
        /// Solve the problem by the algorithm
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>the solution of the problem</returns>
        public override Solution<T> search(ISearchable<T> searchable)
        {
            List<State<T>> discoverdStates = new List<State<T>>();
            addToContainer(searchable.getInitialState());
            while (statesStack.Count() > 0)
            {
                State<T> n = popContainer();
                if (n.Equals(searchable.getGoalState()))
                {
                    State<T>.StatePool.clearDictionary();
                    return backTrace(n);
                }
                if (!discoverdStates.Contains(n))
                {
                    discoverdStates.Add(n);
                    List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                    foreach (State<T> s in succerssors)
                    {
                        s.CameFrom = n;
                        addToContainer(s);
                    }
                }
            }
            return null;
        }
    }
}