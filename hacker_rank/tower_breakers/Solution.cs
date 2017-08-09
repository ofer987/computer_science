using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public static class Int32Extensions
{
    public static IEnumerable<int> ToIndices(this int count)
    {
        if (count <= 0)
        {
            yield break;
        }

        for (var i = 0; i < count; i++)
        {
            yield return i;
        }
    }
}

public class Solution
{
    public static void Main(String[] args)
    {
        ReadTestCount();
        var games = ReadGames();

        foreach (var towers in games)
        {
            var winner = Winner(towers);
            Console.WriteLine(winner);
        }
    }

    private static int ReadTestCount()
    {
        return int.Parse(Console.ReadLine());
    }

    private static IEnumerable<List<int>> ReadGames()
    {
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            var conditions = line
                .Split(' ')
                .Select(int.Parse)
                .ToList();

            var towers = new List<int>(conditions[0]);
            for (var i = 0; i < conditions[0]; i++)
            {
                towers.Add(conditions[1]);
            }

            yield return towers;
        }
    }

    // Return the winner
    private static int Winner(List<int> towers)
    {
        var winner = 1;
        while (Move(towers) != 0)
        {
            winner++;
        }

        return (winner % 2) + 1;
    }

    private static int Move(List<int> towers)
    {
        var move = towers
            .Count
            .ToIndices()
            .Select(index => new Tuple<int, int>(index, LargestDifference(towers[index])))
            .OrderByDescending(tuple => tuple.Item2)
            .First();

        if (move.Item2 > 0)
        {
            towers[move.Item1] = move.Item2;
        }

        return move.Item2;
    }


    private static int LargestDifference(int tower)
    {
        for (var i = 1; i < tower; i++)
        {
            if (tower % i == 0)
            {
                return i;
            }
        }

        return 0;
    }
}
