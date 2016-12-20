using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

public class Node
{
    public List<Node> Parents { get; private set; }

    public List<Node> Children { get; private set; }

    public Node()
    {
        Parents = new List<Node>();
        Children = new List<Node>();
    }

    public int DegreesFrom(Node target)
    {
        var queue = new Queue<Node>();
        var nextQueue = new Queue<Node>();

        queue.Enqueue(this);
        var degrees = 0;
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node == target)
            {
                return degrees;
            }
            foreach (var child in node.Children)
            {
                nextQueue.Enqueue(child);
            }

            if (queue.Count == 0)
            {
                degrees++;
                queue = nextQueue;
                nextQueue = new Queue<Node>();
            }
        }

        // Could not find the target.
        return -1;
    }
}

public class Graph
{
    public List<Node> Nodes { get; private set; }

    public Node KevinBacon { get; private set; }

    public Graph(Node kevinBacon)
    {
        Nodes = new List<Node>();
        KevinBacon = kevinBacon;
    }

    public int DegreesFromKevinBacon(Node node)
    {
        node.DegreesFrom(KevinBacon)
    }
}

public class Solution
{
    public static void Main(string[] args)
    {
    }
}
