using System;
using System.Collections.Generic;
using System.Linq;

public enum Operation { None = 0, LessThan, GreaterThan }

public static class Solution
{
    public static void Main(String[] args)
    {
        var original = GetArray();

        foreach (var item in ToZigzag(original))
        {
            Console.Write("{0} ", item);
        }
    }

    private static IEnumerable<int> GetArray()
    {
        return Console.ReadLine().Split(' ').Select(int.Parse);
    }

    private static IEnumerable<int> ToZigzag(IEnumerable<int> array)
    {
        var i = int.MinValue;
        var operation = Operation.None;

        foreach (var item in array)
        {
            switch (operation)
            {
                case Operation.LessThan:
                    if (item < i)
                    {
                        yield return item;
                    }
                    else
                    {
                        yield return i;
                        i = item;
                    }

                    // Next operation
                    operation = Operation.GreaterThan;

                    break;
                case Operation.GreaterThan:
                    if (item > i)
                    {
                        yield return item;
                    }
                    else
                    {
                        yield return i;
                        i = item;
                    }

                    // Next operation
                    operation = Operation.LessThan;

                    break;
                case Operation.None:
                    i = item;
                    operation = Operation.LessThan;
                    break;
            }
        }

        yield return i;
    }
}
