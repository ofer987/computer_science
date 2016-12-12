using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public static class Extensions
{
    public static IEnumerable<string> FindSubstrings(this string super)
    {
        var length = super.Length;
        for (var i = 0; i < length; i++)
        {
            for (var j = i; j < length; j++)
            {
                yield return super.Substring(i, j-i+1);
            }
        }
    }
}

public class Solution
{
    public static void Main(string[] args)
    {
        var super = Console.ReadLine();

        foreach (var sub in super.FindSubstrings().Distinct())
        {
            Console.WriteLine(sub);
        }
    }
}
