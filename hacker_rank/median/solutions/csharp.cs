using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class QuickSort {
    public int FindMedian(List<int> unsorted) {
        var medianIndex = (unsorted.Count - 1) / 2;
        var median = unsorted[medianIndex];

        return Recursive(new[] { unsorted }, median, medianIndex);
    }

    public int Recursive(IEnumerable<List<int>> unsorted, int median, int medianIndex) {
        var results = unsorted
            .Where(list => list.Count > 0)
            .SelectMany(list => Sort(list));

        // Flatten it
        // And combine the sublists
        // To find the middle element of the entire list
        var flattened = results
            .SelectMany(list => list)
            .ToList();

        var medianSorted = flattened[medianIndex];

        if (medianSorted == median) {
            return median;
        }

        return Recursive(results, medianSorted, medianIndex);
    }

    private IEnumerable<List<int>> Sort(List<int> unsorted) {
        var medianIndex = (unsorted.Count - 1) / 2;
        var median = unsorted[medianIndex];

        var smaller = new List<int>();
        var larger = new List<int> { median };

        for (var i = 0; i < unsorted.Count; i++)
        {
            if (i == medianIndex)
            {
                continue;
            }
            var item = unsorted[i];

            if (item < median) {
                smaller.Add(item);
            } else if (item >= median) {
                larger.Add(item);
            }
        }

        yield return smaller;
        yield return larger;
    }
}

class Solution {
    static void Main(String[] args) {
        var count = Console.ReadLine().Split(' ').Select(int.Parse).First();

        var values = Console
            .ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .Take(count)
            .ToList();

        var median = new QuickSort().FindMedian(values);

        Console.WriteLine(median);
    }
}
