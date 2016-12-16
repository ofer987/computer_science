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

public class CoodinateEqualityComparer : EqualityComparer<Coordinate>
{
    public override bool Equals(Coordinate x, Coordinate y)
    {
        return x.X == y.X && x.Y == y.Y;
    }

    public override int GetHashCode(Coordinate coordinate)
    {
        return coordinate.X.GetHashCode() + coordinate.Y.GetHashCode();
    }
}

public enum Senses { North = 0, West, South, East };

public class Grid
{
    private List<int> Movements { get; set; }

    private List<Coordinate> Coordinates { get; set; }

    public Dictionary<Coordinate, int> HasPlayed { get; private set; }

    public Grid(IEnumerable<int> movements)
    {
        Movements = new List<int>();
        Movements.AddRange(movements);
        Coordinates = GetCoordinates(Movements).ToList();
        ResetHasPlayed();
    }

    public void Play()
    {
        ResetHasPlayed();

        foreach (var coordinate in Coordinates)
        {
            if (HasPlayed.ContainsKey(coordinate))
            {
                HasPlayed[coordinate] += 1;
            }
            else
            {
                HasPlayed.Add(coordinate, 1);
            }
        }
    }

    private IEnumerable<Coordinate> GetCoordinates(IEnumerable<int> movements)
    {
        var coordinate = new Coordinate(0, 0);
        yield return coordinate;

        var sense = Senses.North;
        foreach (var movement in movements)
        {
            switch(sense)
            {
                case Senses.North:
                    foreach (var unit in ReturnUnitsFrom(movement))
                    {
                        coordinate = new Coordinate(coordinate.X, coordinate.Y + unit);
                        yield return coordinate;
                    }
                    break;
                case Senses.West:
                    foreach (var unit in ReturnUnitsFrom(movement))
                    {
                        coordinate = new Coordinate(coordinate.X - unit, coordinate.Y);
                        yield return coordinate;
                    }
                    break;
                case Senses.South:
                    foreach (var unit in ReturnUnitsFrom(movement))
                    {
                        coordinate = new Coordinate(coordinate.X, coordinate.Y - unit);
                        yield return coordinate;
                    }
                    break;
                case Senses.East:
                    foreach (var unit in ReturnUnitsFrom(movement))
                    {
                        coordinate = new Coordinate(coordinate.X + unit, coordinate.Y);
                        yield return coordinate;
                    }
                    break;
            }

            sense = (Senses)(((int)sense + 1) % 4);
        }
    }

    private IEnumerable<int> ReturnUnitsFrom(int distance)
    {
        if (distance > 0)
        {
            for (var i = 0; i < distance; i++)
            {
                yield return 1;
            }
        }
        else if (distance < 0)
        {
            for (var i = 0; i > distance; i--)
            {
                yield return 1;
            }
        }
    }

    private void ResetHasPlayed()
    {
        HasPlayed = new Dictionary<Coordinate, int>(new CoodinateEqualityComparer());
    }

    public bool HasCrossed()
    {
        foreach (var pair in HasPlayed)
        {
            if (pair.Value > 1)
            {
                return true;
            }
        }

        return false;
    }
}

public static class Solution
{
    public static void Main(string[] argv)
    {
        var question = Console.ReadLine().Split(' ').Select(int.Parse);

        var answer = new Grid(question);
        answer.Play();

        var hasCrossed = answer.HasCrossed();

        Console.WriteLine(hasCrossed ? "YES" : "NO");
    }
}
