using System;
using System.Linq;
using System.Collections.Generic;

namespace SearchOrInsert
{
    public class Searcher
    {
        public List<int> Sorted { get; private set; }

        public Searcher(IList<int> sorted)
        {
            Sorted = sorted.ToList();
        }

        public int FindTarget(int target)
        {
            return FindTarget(target, 0, Sorted.Count - 1);
        }

        public int FindTarget(int target, int start, int end)
        {
            var index = (end + start) / 2;
            var median = Sorted[index];

            if (target < median)
            {
                if (start > index - 1)
                {
                    // Could not find target so insert
                    var newSorted = new List<int>();
                    for (var i = 0; i < index; i++)
                    {
                        newSorted.Add(Sorted[i]);
                    }
                    newSorted.Add(target);
                    for (var i = index; i < Sorted.Count; i++)
                    {
                        newSorted.Add(Sorted[i]);
                    }

                    Sorted = newSorted;
                    return index;
                }

                return FindTarget(target, start, index-1);
            }
            else if (target > median)
            {
                if (end < index + 1)
                {
                    // Could not find target so insert
                    var newSorted = new List<int>();
                    for (var i = 0; i <= index; i++)
                    {
                        newSorted.Add(Sorted[i]);
                    }
                    newSorted.Add(target);
                    for (var i = index+1; i < Sorted.Count; i++)
                    {
                        newSorted.Add(Sorted[i]);
                    }

                    Sorted = newSorted;
                    return index;
                }

                return FindTarget(target, index+1, end);
            }

            // Great
            // Target acquired
            // Now find where it starts and where it ends

            return index;
        }
    }
}
