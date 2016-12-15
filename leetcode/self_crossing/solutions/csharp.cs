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

public class Coordinate
{
    public int X { get; set; }

    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public enum Senses { North = 0, West, South, East };

public class Grid
{
    private List<int> Movements { get; set; }

    private List<Coordinate> Coordinates { get; set; }

    public Grid(IEnumerable<int> movements)
    {
        Movements = new List<int>();
        Movements.AddRange(movements);
        Coordinates = GetCoordinates(Movements).ToList();
    }

    public IEnumerable<Coordinate> GetCoordinates(IEnumerable<int> movements)
    {
        var sense = Senses.North;
        var prevCoordinate = new Coordinate(0, 0);
        foreach (var movement in movements)
        {
            var coordinate = new Coordinate(prevCoordinate.X, prevCoordinate.Y);
            switch(sense)
            {
                case Senses.North:
                    coordinate.Y += movement;
                    break;
                case Senses.West:
                    coordinate.X -= movement;
                    break;
                case Senses.South:
                    coordinate.Y -= movement;
                    break;
                case Senses.East:
                    coordinate.X += movement;
                    break;
            }

            yield return coordinate;
            prevCoordinate = coordinate;

            sense = (Senses)(((int)sense + 1) % 4);
        }
    }
}

public static class Solution
{
    public static void Main(string[] argv)
    {
        var question = Console.ReadLine().Split(' ').Select(int.Parse);

        var answer = new Grid(question);
    }
}
