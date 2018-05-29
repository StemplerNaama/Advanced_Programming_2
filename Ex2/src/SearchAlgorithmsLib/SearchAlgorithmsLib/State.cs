using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// the state class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T>
    {
        /// <summary>
        /// The state
        /// </summary>
        private T state;
        /// <summary>
        /// The cost
        /// </summary>
        private double cost;
        /// <summary>
        /// The came from
        /// </summary>
        private State<T> cameFrom;
        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        /// <summary>
        /// Gets or sets the came from.
        /// </summary>
        /// <value>
        /// The came from.
        /// </value>
        public State<T> CameFrom
        {
            get { return cameFrom; }
            set { cameFrom = value; }
        }
        /// <summary>
        /// Gets or sets the state description.
        /// </summary>
        /// <value>
        /// The state description.
        /// </value>
        public T StateDescription
        {
            get { return state; }
            set { state = value; }
        }
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="State{T}" /> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public State(T state) // CTOR
        {
            this.state = state;
        }
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) // we override Object's Equals method
        {
            return state.Equals((obj as State<T>).state);
        }

        /// <summary>
        /// statePool
        /// </summary>
        public static class StatePool
        {
            /// <summary>
            /// The state dictionary
            /// </summary>
            static Dictionary<T, State<T>> stateDictionary = new Dictionary<T, State<T>>();
            /// <summary>
            /// Gets the state.
            /// </summary>
            /// <param name="state">The state.</param>
            /// <returns>new state or existing state</returns>
            public static State<T> GetState(T state)
            {
                if (stateDictionary.ContainsKey(state))
                    return stateDictionary[state];
                else
                {
                    State<T> newState = new State<T>(state);
                    stateDictionary[state] = newState;
                    return newState;
                }
            }
            /// <summary>
            /// deletes the dictionary
            /// </summary>
            public static void clearDictionary()
            {
                stateDictionary.Clear();
            }
        }
    }
}