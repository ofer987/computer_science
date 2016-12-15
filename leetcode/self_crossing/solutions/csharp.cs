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

public class Coordinate : IEquatable<Coordinate>
{
    public int X { get; set; }

    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Coordinate other)
    {
        return X == other.X && Y == other.Y;
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
        return 1;
        // return coordinate.X.GetHashCode() + coordinate.Y.GetHashCode();
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

    public IEnumerable<Coordinate> GetCoordinates(IEnumerable<int> movements)
    {
        var sense = Senses.North;
        var prevCoordinate = new Coordinate(0, 0);
        foreach (var movement in movements)
        {
            var coordinate = new Coordinate(prevCoordinate.X, prevCoordinate.Y);
            Console.WriteLine("Moving {0} for {1} movement", sense.ToString(), movement);
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

    public void Play()
    {
        ResetHasPlayed();

        foreach (var coordinate in Coordinates)
        {
            Console.WriteLine("[x, y] = [{0}, {1}]", coordinate.X, coordinate.Y);
            if (HasPlayed.ContainsKey(coordinate))
            {
                Console.WriteLine("true");
                HasPlayed[coordinate] += 1;
            }
            else
            {
                // Default value
                HasPlayed[coordinate] = 1;
            }
        }
    }

    private void ResetHasPlayed()
    {
        HasPlayed = new Dictionary<Coordinate, int>();
        // HasPlayed = new Dictionary<Coordinate, int>(new CoodinateEqualityComparer());
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
