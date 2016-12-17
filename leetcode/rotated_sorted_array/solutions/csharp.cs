using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Extensions
{
    public static string Chain<T>(this IEnumerable<T> list, string middle = "")
    {
        var sb = new StringBuilder();

        sb.Append(list.Take(1).DefaultIfEmpty().FirstOrDefault().ToString());

        foreach(var item in list.Skip(1))
        {
            sb.Append(middle);
            sb.Append(item.ToString());
        }

        return sb.ToString();
    }
}

public class RotatedArray
{
    private List<int> Rotated { get; set; }

    public RotatedArray(IEnumerable<int> rotated)
    {
        Rotated = rotated.ToList();
    }

    public bool HasItem(int item)
    {
        try
        {
            Search(0, Rotated.Count - 1, item);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public int Search(int start, int end, int item)
    {
        var pivot = (end + start) / 2;

        median = Rotated[pivot];
        var startItem = Rotated[start];
        var endItem = Rotated[end];

        if (median == item)
        {
            return pivot;
        }

        if (startItem < median && median < endItem)
        {
            if (item < pivot)
            {
                return Search(start, pivot - 1);
            }
            else
            {
                // item > pivot
                return Search(pivot + 1, end);
            }
        }
        else if (startItem > median && median < endItem)
        {
            // Rotated array is to the left of the median
            if (item < pivot)
            {
                return Search(start, pivot - 1);
            }
            else
            {
                try
                {
                    // Search the non-rotated array
                    return Search(pivot + 1, end);
                }
                catch (Exception ex)
                {
                }

                try
                {
                    // Search the Rotated Array
                    return Search(start, pivot - 1);
                }
                catch (Exception ex)
                {
                }
            }
        }
        else if (startItem > median && median > endItem)
        {
            // Rotated array is to the right of the median
            if (item < pivot)
            {
                try
                {
                    // Search the non-rotated array
                    return Search(start, pivot - 1);
                }
                catch (Exception ex)
                {
                }

                try
                {
                    // Search the Rotated Array
                    return Search(pivot + 1, end);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                return Search(pivot + 1, end);
            }
        }

        // Could not find the item;
        throw new Exception(string.Format("Could not find the item = {0}", item));
}

public static class Solution
{
    public static void Main(string[] argv)
    {
        var nums = Console.ReadLine().Split(' ').Select(int.Parse);
        var target = Console.ReadLine().Split(' ').Select(int.Parse).First();

        var answer = new RotatedArray(question);
        answer.Play();

        var hasCrossed = answer.HasCrossed();

        Console.WriteLine(hasCrossed ? "YES" : "NO");
    }
}
