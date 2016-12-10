using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// It is 1-based
public class Grid {
    public int Rows { get; private set; }

    public int Columns { get; private set; }

    public Grid(int rows, int columns) {
        Rows = rows;
        Columns = columns;
    }

    public ulong AvailableSquares(IEnumerable<Train> trains) {
        var results = (ulong)Rows * (ulong)Columns;

        var taken = (ulong)0;
        foreach (var grp in trains.GroupBy(train => train.Row).OrderBy(grp => grp.Key)) {
            Console.WriteLine("Row = {0}", grp.Key);

            var previousStartColumn = 0;
            var previousEndColumn = 0;
            foreach (var train in SortTrains(grp)) {
                Console.WriteLine(train);

                if (train.StartColumn > previousEndColumn) {
                    taken = (ulong)train.EndColumn - (ulong)train.StartColumn + (ulong)1;
                } else if (train.EndColumn <= previousEndColumn) {
                    // Do nothing
                    taken = (ulong)0;
                } else {
                    taken = (ulong)train.EndColumn - (ulong)previousEndColumn;
                }

                previousStartColumn = train.StartColumn;
                previousEndColumn = train.EndColumn;
            }
        }

        return results - taken;
    }

    private IEnumerable<Train> SortTrains(IEnumerable<Train> unsorted) {
        return unsorted.OrderBy(train => train, new TrainComparer());
    }
}

// It is 1-based
public class Train {
    public int Row { get; private set; }

    public int StartColumn { get; private set; }

    public int EndColumn { get; private set; }

    public Train(int row, int startColumn, int endColumn) {
        Row = row;
        StartColumn = startColumn;
        EndColumn = endColumn;
    }

    public override string ToString() {
        return string.Format("Row = {0}, StartColumn = {1}, EndColumn = {2}",
                Row, StartColumn, EndColumn);
    }
}

public class TrainComparer : Comparer<Train> {
    public override int Compare(Train x, Train y) {
        if (x.Row > y.Row) {
            return 1;
        } else if (x.Row < y.Row) {
            return -1;
        } else {
            if (x.StartColumn == y.StartColumn && x.EndColumn == y.EndColumn) {
                return 0;
            } else if (y.StartColumn < x.StartColumn) {
                return 1;
            } else if (y.StartColumn > x.StartColumn) {
                return -1;
            } else if (y.EndColumn < x.EndColumn) {
                return 1;
            } else {
                return -1;
            }
        }
    }
}

public class Solution {
    public static void Main(String[] args) {
        var firstLine = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

        var rows = firstLine[0];
        var columns = firstLine[1];
        var trainCount = firstLine[2];

        var grid = new Grid(rows, columns);

        var trains = GetTrains(trainCount);

        Console.WriteLine(grid.AvailableSquares(trains).ToString());
    }

    private static IEnumerable<Train> GetTrains(int count) {
        for (var i = 0; i < count; i++) {
            var line = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            var row = line[0];
            var startColumn = line[1];
            var endColumn = line[2];

            yield return new Train(row, startColumn, endColumn);
        }
    }
}
