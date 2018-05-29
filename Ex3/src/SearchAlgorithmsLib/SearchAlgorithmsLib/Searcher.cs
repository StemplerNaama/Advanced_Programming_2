namespace SearchAlgorithmsLib
{
    /// <summary>
    /// class of searching algorithm 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.ISearcher{T}" />
    public abstract class Searcher<T>: ISearcher<T>
    {
        /// <summary>
        /// The evaluated nodes
        /// </summary>
        protected int evaluatedNodes;
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="Searcher{T}"/> class.
        /// </summary>
        public Searcher() {
            evaluatedNodes = 0;
        }
        /// <summary>
        /// Pops the container.
        /// </summary>
        /// <returns>the last element in the container</returns>
        public abstract State<T> popContainer();
        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns>the number of nodes</returns>
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        /// <summary>
        /// solve the problem by the algorithm
        /// </summary>
        /// <param name="searchable">The problem to solve.</param>
        /// <returns>the solution of the problem</returns>
        public abstract Solution<T> search(ISearchable<T> searchable);
        /// <summary>
        /// Adds to container.
        /// </summary>
        /// <param name="s">state to add to container</param>
        public abstract void addToContainer(State<T> s);
        /// <summary>
        /// Gets the evaluated nodes.
        /// </summary>
        /// <value>
        /// The evaluated nodes.
        /// </value>
        public int EvaluatedNodes
        {
            get { return evaluatedNodes; }
        }
        /// <summary>
        /// extracting the nodes solution from the goal state
        /// </summary>
        /// <param name="goal">The goal.</param>
        /// <returns>the solution</returns>
        public Solution<T> backTrace(State<T> goal)
        {
            Solution<T> sol = new Solution<T>();
            State<T> s = goal;
            while(s != null)
            {
                sol.addToSolList(s.StateDescription);
                s = s.CameFrom;
            }
            //order the solution list from initial state, to goal state
            sol.increasingSolOrder();
            return sol;
        }
    }
}
