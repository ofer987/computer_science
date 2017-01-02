using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeSortedLists
{
    public class Lists
    {
        public List<List<int>> SortedLists { get; private set; }

        public Lists(IEnumerable<IEnumerable<int>> sortedLists)
        {
            // Assume that the lists are already sorted
            SortedLists = sortedLists.Select(l => l.ToList()).ToList();
        }

        public IEnumerable<int> Merge()
        {
            // The indices start at 0
            var indexTracker = SortedLists.Select(l => 0).ToList();

            // Infinite loop
            while(true)
            {
                // Find the smallest number
                var chosenList = -1;
                var min = int.MaxValue;
                for (var i = 0; i < SortedLists.Count; i++)
                {
                    var list = SortedLists[i];
                    var trackedIndex = indexTracker[i];

                    if (trackedIndex >= list.Count)
                    {
                        // Index is exhausted so do not use it
                        continue;
                    }

                    var val = list[trackedIndex];
                    if (val <= min)
                    {
                        chosenList = i;
                        min = val;
                    }
                }

                // Exit condition
                if (chosenList == -1)
                {
                    // No lists were chosen because all the indexTrackers are exhausted:
                    // i.e., all the indices are greater than the count of the SortedLists
                    break;
                }

                // Increment the indexTracker
                indexTracker[chosenList]++;

                yield return min;
            }
        }
    }
}
