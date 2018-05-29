using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class CostComp //: IComparer<T>
    {
        public int Compare(IComparable x, IComparable y)
        {
            return x.CompareTo(y);
        }
    }
}
