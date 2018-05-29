using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// the solvong problems interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>the initial state</returns>
        State<T> getInitialState();
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns>the goal state</returns>
        State<T> getGoalState();
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>the list of the neighbors of the state</returns>
        List<State<T>> getAllPossibleStates(State<T> s);
        /// <summary>
        /// Costs the of edge.
        /// </summary>
        /// <param name="s1">stateA</param>
        /// <param name="s2">stateB</param>
        /// <returns>the cost of the edge between the states</returns>
        double costOfEdge(State<T> s1, State<T> s2);
    }
}