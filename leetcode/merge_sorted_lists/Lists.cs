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
            var tracker = SortedLists.Select(l => 0).ToList();

            // Infinite loop
            while(true)
            {
                // Exit condition
                if (ShouldExit(tracker, SortedLists))
                {
                    break;
                }

                // Find the smallest number
                var chosenList = -1;
                var min = int.MinValue;
                for (var i = 0; i < SortedLists.Count; i++)
                {
                    var list = SortedLists[i];
                    var trackedIndex = tracker[i];

                    if (trackedIndex >= list.Count)
                    {
                        continue;
                    }

                    var val = list[trackedIndex];
                    if (val <= min)
                    {
                        chosenList = i;
                        min = val;
                    }
                }

                yield return min;

                tracker[chosenList]++;
            }
        }

        public bool ShouldExit(
                IList<int> tracker,
                IEnumerable<IEnumerable<int>> lists)
        {
            var i = 0;
            foreach (var list in lists)
            {
                var trackedIndex = tracker[i];
                if (trackedIndex < tracker.Count)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
