using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// the solution of a problem class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Solution<T>
    {
        /// <summary>
        /// The solution list
        /// </summary>
        private List<T> solList;
        /// <summary>
        /// CTOR: Initializes a new instance of the <see cref="Solution{T}"/> class.
        /// </summary>
        public Solution()
        {
            solList = new List<T>();
        }
        /// <summary>
        /// Gets the solution list.
        /// </summary>
        /// <value>
        /// The solution list.
        /// </value>
        public List<T> SolList
        {
            get { return solList; }
        }
        /// <summary>
        /// Adds to solution list.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void addToSolList(T obj)
        {
            solList.Add(obj);
        }
        /// <summary>
        /// Prints the solution.
        /// </summary>
        public void printSol()
        {
            int listSize = solList.Count;
            Console.WriteLine(listSize);
            for (int i = 0; i < listSize; i++)
            {
                Console.WriteLine(solList[i].ToString());
            }
        }
        /// <summary>
        /// traversing the solution list in reverse order
        /// </summary>
        public void increasingSolOrder()
        {
            //traverse the list in reverse order
            solList.Reverse();
        }
        /// <summary>
        /// Sols the size of the list.
        /// </summary>
        /// <returns>the num of nodes of the solution</returns>
        public int solListSize()
        {
            return solList.Count;
        }
    }
}
