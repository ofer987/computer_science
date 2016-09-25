using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

public interface INode
{
    int Index { get; }

    INode Parent { get; set; }

    INode Left { get; set; }

    INode Right { get; set; }

    IEnumerable<INode> InOrderTraversal();

    void Swap();
}

public static class NodeExtensions
{
    public static string ToInOrderTraversalString(this INode root)
    {
        return root.
            InOrderTraversal().
            Select(node => node.Index).
            Select(index => index.ToString()).
            Aggregate((result, index) => result + " " + index);
    }
}

public class Leaf : INode
{
    public int Index
    {
        get { return -1; }
    }

    public INode Parent { get; set; }

    public INode Left
    {
        get { return new Leaf { Parent = this }; }

        set { }
    }

    public INode Right
    {
        get { return new Leaf { Parent = this }; }

        set { }
    }

    public Leaf()
    {
        Parent = null;
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
    public virtual int Index { get; set; }

    public virtual INode Parent { get; set; }

    public INode Left { get; set; }

    public INode Right { get; set; }

    public Node(INode parent)
    {
        this.Parent = parent;
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

public class Root : Node
{
    public Root() : base(null)
    {
    }

    public override INode Parent
    {
        get { return null; }

        set { }
    }

    public override int Index
    {
        get { return 1; }

        set { }
    }
}

public class Tranches : List<List<INode>>
{
    public INode Root { get; private set; }

    public Tranches(INode root)
    {
        Root = root;
        Set();
    }

    public void Swap(int height)
    {
        foreach (var node in this[height])
        {
            node.Swap();
        }
    }

    private void Set()
    {
        // Use a FIFO stack
        var stack = new List<INode>();
        // Let's start with the root node
        stack.Add(Root);

        while (stack.Any(node => !(node is Leaf)))
        {
            this.Add(stack);

            var nextStack = new List<INode>();
            foreach (var node in stack)
            {
                nextStack.Add(node.Left);
                nextStack.Add(node.Right);
            }

            // Iterate over the next stack in the next iteration
            stack = nextStack;
        }
    }
}

public class Swapper
{
    public INode Root { get; private set; }

    public Tranches Tranches { get; private set; }

    public Swapper(INode root)
    {
        this.Root = root;

        Tranches = new Tranches(root);
    }

    public void Swap(int startHeight)
    {
        foreach (var height in HeightsToSwap(startHeight))
        {
            // 0-based indexing
            Tranches.Swap(height - 1);
        }
    }

    private IEnumerable<int> HeightsToSwap(int height)
    {
        for (var multiple = 1;
                // 1-based indexing < 0-based indexing
                multiple * height <= Tranches.Count;
                multiple++)
        {
            yield return multiple * height;
        }
    }
}

public class Solution
{
    public static void Main(String[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */

        var root = GetTree();
        var traversals = GetTraversalHeights();

        var swapper = new Swapper(root);
        foreach (var traversal in traversals)
        {
            swapper.Swap(traversal);

            Console.WriteLine(root.ToInOrderTraversalString());
        }
    }

    private static Root GetTree()
    {
        var numberOfNodes = int.Parse(Console.ReadLine());

        var root = new Root();

        var reverseLookup = new Dictionary<int, INode>();
        reverseLookup.Add(1, root);

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
                var parent = reverseLookup[i];
                INode node;

                if (index == -1)
                {
                    // Node is a leaf
                    node = new Leaf
                    {
                        Parent = parent
                    };
                }
                else
                {
                    node = new Node(parent)
                    {
                        Index = index
                    };
                }

                reverseLookup[index] = node;

                if (j == 0)
                {
                    parent.Left = node;
                }
                else
                {
                    parent.Right = node;
                }
            }
        }

        return root;
    }

    private static IEnumerable<int> GetTraversalHeights()
    {
        var total = int.Parse(Console.ReadLine());

        for (var i = 0; i < total; i++)
        {
            yield return int.Parse(Console.ReadLine());
        }
    }
}
