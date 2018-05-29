using Priority_Queue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// the searcher class- using priority queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        /// <summary>
        /// The priority queue = open list
        /// </summary>
        private SimplePriorityQueue<State<T>> openList;
        /// <summary>
        /// Initializes a new instance of the <see cref="PrioritySearcher{T}"/> class.
        /// </summary>
        public PrioritySearcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
        }
        /// <summary>
        /// Pops the container.
        /// </summary>
        /// <returns>the last element</returns>
        public override State<T> popContainer()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }
        /// <summary>
        /// Adds to container.
        /// </summary>
        /// <param name="s">state to add to container</param>
        public override void addToContainer(State<T> s)
        {
            openList.Enqueue(s, (float)s.Cost); //check the conversion
        }
        /// <summary>
        /// Removes from container.
        /// </summary>
        /// <param name="s">state to remove from container</param>
        public void removeFromContainer(State<T> s)
        {
            openList.Remove(s);
        }
        /// <summary>
        /// Gets the size of the open list.
        /// </summary>
        /// <value>
        /// The size of the open list.
        /// </value>
        public int OpenListSize
        { 
            // it is a read-only property
            get { return openList.Count; }
        }
        /// <summary>
        /// check if the open list containes a state
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>true if contains, else- false</returns>
        public bool openContaines(State<T> s)
        {
            return openList.Contains(s);
        }
    }
}