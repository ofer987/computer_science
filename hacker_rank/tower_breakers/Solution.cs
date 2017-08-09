using System;
using System.Collections.Generic;
using System.IO;

public static class Int32Extensions
{
    public static IEnumerable<int> ToIndices(this int count)
    {
        if (count <= 0)
        {
            return Enumerable.Empty<int>();
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
        var testCount = ReadTestCount();
        var games = ReadGames();

        foreach (var game in games)
        {
            var winner = PlayGame(game[0], game[1]);
            Console.WriteLine(winner);
        }
    }

    private static int ReadTestCount()
    {
        return int.Parse(Console.ReadLine());
    }

    private static IEnumerable<Tuple<int, int>> ReadGames()
    {
        while ((var line = Console.ReadLine()) != null)
        {
            var conditions = line
                .Split(' ')
                .Select(int.Parse)
                .ToList();

            yield return new Tuple<int, int>(line[0], line[1]);
        }
    }

    // Return the winner
    private static int Winner(IList<int> towers)
    {
        var player = 0;
        while ((var move = Move(towers)) != 0)

        return (player % 2) + 1;
    }

    private static int Move(IList<int> towers)
    {
        var move = towers
            .Count
            .ToIndices()
            .Select(index => new Tuple<int, int>(index, LargestDifference(towers[index])))
            .OrderByDifference(tuple => tuple.Item2)
            .First;

        towers[move.Item1] = towers[move.Item1] - move.Item2;

        return move.Item2;
    }


    private static int LargestDifference(int tower)
    {
        for (var i = 1; i < tower; i++)
        {
            if (i % tower == 0)
            {
                return i;
            }
        }

        return 0;
    }
}
