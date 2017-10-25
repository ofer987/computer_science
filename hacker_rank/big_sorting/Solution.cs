using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Solution
{
    public static void Main(string[] args)
    {
        var unsorted = ReadUnsorted();

        var sorted = Sort(unsorted);
    }

    private static string[] ReadUnsorted()
    {
        int n = Convert.ToInt32(Console.ReadLine());

        string[] unsorted = new string[n];
        for (int i = 0; i < n; i++) {
           unsorted[i] = Console.ReadLine();
        }
    }

    private static IEnumerable<string> Sort(IEnumerable<string> unsorted)
    {
        var results = Enumerable.Empty<string>();
        var groupedByLength = GroupByLength(unsorted);
        var grouped = groupedByLength
            .Select(group => GroupItemsOfEqualLength(group, group.Key - 1));

        foreach (var group in GroupByLength(unsorted))
        {
            results.Concat(group)
        }

        return results;
    }

    private static IEnumerable<T> OrderGroups<T>(
            IEnumerable<IGroup<int, T>> groups)
    {
        groups.OrderByDescending
    }

    private static IEnumerable<IGrouping<int, string>> GroupByLength(IEnumerable<string> unsorted)
    {
        return unsorted.GroupBy(item => item.Length);
    }

    private static IEnumerable<IGrouping<int, T>> GroupItemsOfEqualLength<T>(
            IEnumerable<T>) items,
            int index)
    {
        if (i < 0)
        {
            return items;
        }

        var sortedByIndex = items
            .GroupBy(item => item[i])
            .OrderByDescending(item => int.Parse(item.Key));

        return GroupItemsOfEqualLength(sortedByIndex, i--);
    }
}
