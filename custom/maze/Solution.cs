using System;
using System.Collections.Generic;
using System.Linq;

public class Maze : List<List<bool>>
{
    public List<Node> FirstPath(Tuple<int, int> start, Tuple<int, int> end)
    {
        var traversed = new Dictionary<Node, bool>();
        var node = new Node(this, start.Item1, start.Item2, traversed);

        if (!this[start.Item2][start.Item1] || !this[end.Item2][end.Item1])
        {
            return null;
        }

        return Next(Enumerable.Empty<Node>(), node, end);
    }

    private List<Node> Next(IEnumerable<Node> path, Node parent, Tuple<int, int> end)
    {
        if (parent.X == end.Item1 && parent.Y == end.Item2)
        {
            return path as List<Node>() ?? path.ToList();
        }
        else
        {
            foreach (var child in parent.Children)
            {
                var newPath = new List<Node>(path);
                return Next(newPath, child, end);
            }
        }

        // Path does not exist
        return null;
    }
}

public class Node
{
    public IEnumerable<Node> Children
    {
        get
        {
            var children = () => {
                yield return Up;
                yield return Down;
                yield return Left;
                yield return Right;
            }

            return children.
                Select(node => node != null).
                Select(node => {
                            bool isTraversed = false;
                            if (Traversed.TryGetValue(this, out isTraversed))
                            {
                                return false;
                            }

                            return true;
                        });
        }
    }

    public Dictionary<Node, bool> Traversed { get; }

    public Maze Maze { get; };

    public int X { get; }

    public int Y { get; }

    public Node Up
    {
        get
        {
            if (Y > 0 && Y < Maze.Count)
            {
                if (Maze[Y - 1][X])
                {
                    return new Node(Maze, X, Y - 1, Traversed);
                }
            }

            return null;
        }
    }

    public Node Down
    {
        get
        {
            if (Y >= 0 && Y < Maze.Count - 1)
            {
                if (Maze[Y + 1][X])
                {
                    return new Node(Maze, X, Y + 1, Traversed);
                }
            }

            return null;
        }
    }

    public Node Left
    {
        get
        {
            if (X > 0 && X < Maze[Y].Count)
            {
                if (Maze[Y][X - 1])
                {
                    return new Node(Maze, X - 1, Y, Traversed);
                }
            }

            return null;
        }
    }

    public Node Right
    {
        get
        {
            if (X >= 0 && X < Maze[Y].Count - 1)
            {
                if (Maze[Y][X + 1])
                {
                    return new Node(Maze, X + 1, Y, Traversed);
                }
            }

            return null;
        }
    }

    public Node(Maze maze, int x, int y, Dictionary<Node, bool> traversed)
    {
        Maze = maze;
        X = x;
        Y = y;
        Traversed = traversed;

        Traversed.Add(this, true);
    }
}

public static class Solution
{
    public static void Main(string[] argv)
    {
        var start = ReadPosition();
        var end = ReadPosition();
        var lines = ReadLines().ToList();

        var maze = new Maze(lines);
        var path = maze.FirstPath(start, end);

        if (path == null)
        {
            Console.WriteLine("No valid path exists");
        }
        else
        {
            foreach (var node in path)
            {
                Console.WriteLine("({0}, {1})", node.X, node.Y);
            }
        }
    }

    public static Tuple<int, int> ReadPosition()
    {
        var list = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

        return new Tuple<int, int>(list[0], list[1]);
    }

    public static IEnumerable<List<bool>> ReadLines()
    {
        var dimensions = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
        var width = dimensions[0];
        var length = dimensions[1];

        var i = 0;
        while (i < length)
        {
            // The width is ignored
            yield return Console.
                ReadLine().
                Split(' ').
                Select(bool.Parse).
                ToList();
        }
    }
}
