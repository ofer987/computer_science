using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;

class Solution
{
    public class Grid
    {
        private int? Bits;

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public Grid(int rows, int columns)
        {
            // Assume that the arguments are valid and within range
            Rows = rows;
            Columns = columns;
        }

        public int ToBits()
        {
            if (Bits.HasValue)
            {
                return Bits.Value;
            }

            var length = Rows * Columns;

            Bits = (int)(Math.Pow((double)2, (double)length) - 1);
            return Bits.Value;
        }

        public int FreeCellsValue(IEnumerable<Track> tracks)
        {
            var maxValue = ToBits();
            var result = maxValue;
            foreach (var track in tracks)
            {
                result &= track.ToBits(maxValue, Columns);
            }

            return result;
        }
    }

    public class Track
    {
        public int Row { get; private set; }

        public int Start { get; private set; }

        public int End { get; private set; }

        public Track(int row, int start, int end)
        {
            // Assume the arguments are valid and within range
            Row = row - 1;
            Start = start - 1;
            End = end - 1;
        }

        public int ToBits(int maxValue, int columnsPerRow)
        {
            var result = maxValue;
            for (var i = Start; i <= End; i++)
            {
                var index = columnsPerRow * Row + i;
                result -= (int)Math.Pow((double)2, (double)index);
            }

            return (int)result;
        }
    }

    public static void Main(String[] args)
    {
        var grid = ReadGrid();
        var tracks = ReadTracks();

        var freeCells = ToBits(grid.FreeCellsValue(tracks))
            .Cast<bool>()
            .Where(bit => bit)
            .Count();

        Console.WriteLine(freeCells);
    }

    private static Grid ReadGrid()
    {
        var list = Console
            .ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToList();

        return new Grid(list[0], list[1]);
    }

    private static IEnumerable<Track> ReadTracks()
    {
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            var list = line.Split(' ').Select(int.Parse).ToList();
            yield return new Track(list[0], list[1], list[2]);
        }
    }

    private static BitArray ToBits(int val)
    {
        return new BitArray(new int[] { val });
    }
}

