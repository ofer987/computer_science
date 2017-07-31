using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;

class Solution
{
    public class Grid
    {
        private BitArray Bits;

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public Grid(int rows, int columns)
        {
            // Assume that the arguments are valid and within range
            Rows = rows;
            Columns = columns;
        }

        public BitArray ToBits()
        {
            if (Bits != null)
            {
                return Bits;
            }

            var length = Rows * Columns;
            var maxValue = (int)(Math.Pow((double)2, (double)length) - 1);

            Bits = new BitArray(new int[] { maxValue });
            return Bits;
        }

        public BitArray FreeCells(IEnumerable<Track> tracks)
        {
            var maxValue = ToBits();
            var result = new BitArray(maxValue);
            foreach (var track in tracks)
            {
                result.And(track.ToBits(maxValue, Columns));
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

        public BitArray ToBits(BitArray maxValue, int columnsPerRow)
        {
            var bits = new BitArray(maxValue);
            for (var i = Start; i <= End; i++)
            {
                var index = columnsPerRow * Row + i;
                bits.Set(index, false);
            }

            return bits;
        }
    }

    public static void Main(String[] args)
    {
        var grid = ReadGrid();
        var tracks = ReadTracks();

        var freeCells = grid
            .FreeCells(tracks)
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
}

