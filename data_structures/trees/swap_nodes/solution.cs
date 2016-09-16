using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

public interface INode
{
    int Index { get; }

    INode Left { get; set; }

    INode Right { get; set; }

    IEnumerable<INode> InOrderTraversal();

    void Swap();
}

public class Leaf : INode
{
    public int Index
    {
        get { return -1; }
    }

    public INode Left
    {
        get { return null; }

        set { }
    }

    public INode Right
    {
        get { return null; }

        set { }
    }

    public IEnumerable<INode> InOrderTraversal()
    {
        return Enumerable.Empty<INode>();
    }

    public void Swap()
    {
    }
}

public class Node : INode
{
    public int Index { get; set; }

    public INode Left { get; set; }

    public INode Right { get; set; }

    public Node()
    {
        this.Left = new Leaf();
        this.Right = new Leaf();
    }

    public IEnumerable<INode> InOrderTraversal()
    {
        foreach (var node in this.Left.InOrderTraversal())
        {
            yield return node;
        }

        yield return this;

        foreach (var node in this.Right.InOrderTraversal())
        {
            yield return node;
        }
    }

    public void Swap()
    {
        var swap = Left;
        Left = Right;
        Right = swap;
    }
}

public class Tree : List<INode>
{
    public Tree(int numberOfNodes) : base(numberOfNodes)
    {
        // First node is the root node
        this.Add(new Node { Index = 1 });

        // The rest of the nodes are leaves (for now)
        for (var i = 1; i < numberOfNodes; i++)
        {
            this.Add(new Leaf());
        }
    }

    public IEnumerable<INode> InOrderTraversal()
    {
        return this[0].InOrderTraversal();
    }

    public void Swap(int height)
    {
        // The roots of the subtrees that will be swapped
        var swappedIndices = SwappedIndices(height);

        foreach (var index in swappedIndices)
        {
            this[index].Swap();
        }
    }

    private IEnumerable<int> SwappedIndices(int height)
    {
        for (var index = Math.Pow(2, height-1) - 1; index < Math.Pow(2, height); index++)
        {
            yield return Convert.ToInt32(index);
        }
    }
}

public class Solution
{
    public static void Main(String[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */

        var tree = GetNodes();
        var traversalLocations = GetTraversalLocations();

        foreach (var traversalLocation in traversalLocations)
        {
            tree.Swap(traversalLocation);

            var traversal = tree.
                InOrderTraversal().
                Select(node => node.Index).
                Select(index => index.ToString()).
                Aggregate((result, index) => result + " " + index);

            Console.WriteLine(traversal);
        }
    }

    private static Tree GetNodes()
    {
        var numberOfNodes = int.Parse(Console.ReadLine());

        var tree = new Tree(numberOfNodes);

        for (var i = 1; i <= numberOfNodes; i++)
        {
            var line = Console.ReadLine();

            // Get the two indices.
            // One for left node and the other for the right node
            var indices = line.
                Split(' ').
                Select(index => int.Parse(index)).
                ToList();

            for (var j = 0; j <= 1; j++)
            {
                var index = indices[j];
                INode node;

                if (index == -1)
                {
                    // Node is a leaf
                    node = new Leaf();
                }
                else
                {
                    node = new Node
                    {
                        Index = index
                    };

                    tree[index-1] = node;
                }

                if (j == 0)
                {
                    tree[i-1].Left = node;
                }
                else
                {
                    tree[i-1].Right = node;
                }
            }
        }

        return tree;
    }

    private static IEnumerable<int> GetTraversalLocations()
    {
        var total = int.Parse(Console.ReadLine());

        for (var i = 0; i < total; i++)
        {
            yield return int.Parse(Console.ReadLine());
        }
    }
}
