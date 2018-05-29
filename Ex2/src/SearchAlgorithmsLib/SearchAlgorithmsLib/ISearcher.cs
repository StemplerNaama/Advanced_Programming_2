namespace SearchAlgorithmsLib
{
    /// <summary>
    /// the searcher interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// solve the problem by the algorithm
        /// </summary>
        /// <param name="searchable">The problem to solve.</param>
        /// <returns>the solution of the problem</returns>
        Solution<T> search(ISearchable<T> searchable);
        /// <summary>
        /// Gets the evaluated nodes.
        /// </summary>
        /// <value>
        /// The evaluated nodes.
        /// </value>
        int EvaluatedNodes
        {
            get;
        }
    }
}
