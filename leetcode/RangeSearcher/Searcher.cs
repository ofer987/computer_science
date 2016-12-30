using System;
using System.Linq;
using System.Collections.Generic;

namespace RangeSearcher
{
    public class Searcher
    {
        public List<int> Sorted { get; private set; }

        public Searcher(IList<int> sorted)
        {
            Sorted = sorted.ToList();
        }

        public Tuple<int, int> FindStartAndEnd(int target)
        {
            return FindMedian(target, 0, Sorted.Count - 1);
        }

        public Tuple<int, int> FindMedian(int target, int start, int end)
        {
            var index = (end + start) / 2;
            var median = Sorted[index];

            if (target < median)
            {
                if (start > index - 1)
                {
                    return new Tuple<int, int>(-1, -1);
                }
                return FindMedian(target, start, index-1);
            }
            else if (target > median)
            {
                if (end < index + 1)
                {
                    return new Tuple<int, int>(-1, -1);
                }
                return FindMedian(target, index+1, end);
            }

            // Great
            // Target acquired
            // Now find where it starts and where it ends

            // Find where it starts
            var startRange = index;
            while (startRange >= start)
            {
                if ((startRange - 1) < 0 || Sorted[startRange - 1] != target)
                {
                    break;
                }

                startRange--;
            }

            // Find where it ends
            var endRange = index;
            while (endRange <= end)
            {
                if ((endRange + 1) > end || Sorted[endRange + 1] != target)
                {
                    break;
                }

                endRange++;
            }

            return new Tuple<int, int>(startRange, endRange);
        }
    }
}
