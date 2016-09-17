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
        // Console.WriteLine("to swap for ({0})", Index);
        // Console.WriteLine("\tLeft: ({0}), Right: ({1})", Left.Index, Right.Index);
        var swap = Left;
        Left = Right;
        Right = swap;
        // Console.WriteLine("\tLeft: ({0}), Right: ({1})", Left.Index, Right.Index);
        // Console.WriteLine("swapped");
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

public class Tree : List<INode>
{
    public static Tree BuildNewTree(INode root)
    {
        var tree = new Tree(10);
        // Use a FIFO stack
        var stack = new List<INode>();

        // Let's start with the root node
        stack.Add(root.Left);
        stack.Add(root.Right);

        // unwind the stack
        var index = 1;
        while (stack.Any(node => !(node is Leaf)))
        {
            // Stack of the nodes of the next height
            var nextStack = new List<INode>();
            foreach (var node in stack)
            {
                tree.Add(node);
                // this.Add([index] = node;
                index++;

                nextStack.Add(node.Left);
                nextStack.Add(node.Right);
            }
            // Iterate over the next stack in the next iteration
            stack = nextStack;
        }

        return tree;
    }

    public int NodeCount { get; private set; }

    public Tree(int numberOfNodes) : base(numberOfNodes)
    {
        // First node is the root node
        this.Add(new Root());

        NodeCount = numberOfNodes;
    }

    // public void AddNode(INode node)
    // {
    //     Add(node);
    //
    //     if (node is Node)
    //     {
    //         LastIndex
    //     }
    // }

    public IEnumerable<INode> InOrderTraversal()
    {
        return this[0].InOrderTraversal();
    }

    public string InOrderTraversalToString()
    {
        return
            InOrderTraversal().
            Select(node => node.Index).
            Select(index => index.ToString()).
            Aggregate((result, index) => result + " " + index);
    }

    public void Swap(int startHeight)
    {
        foreach (var height in HeightsToSwap(startHeight))
        {
            // Console.WriteLine("Swap this height " + height);
            // The roots of the subtrees that will be swapped
            foreach (var index in IndicesToSwap(height))
            {
                // Console.WriteLine("Swap this index " + index);
                // try
                // {
                    this[index].Swap();
                    // Console.WriteLine("Swap this index " + index);
                // }
                // catch (ArgumentOutOfRangeException)
                // {
                //     // Ignore the error
                // }

                // Start with the root
                // BuildNewTree(this[0]);
            }
        }
    }

    private IEnumerable<int> HeightsToSwap(int height)
    {
        // Console.WriteLine("Count is " + this.NodeCount);
        for (var multiple = 1;
                multiple * height < this.NodeCount;
                multiple++)
        {
            // Console.WriteLine("Height is {0}", multiple * height);
            yield return multiple * height;
        }

    }

    private IEnumerable<int> IndicesToSwap(int height)
    {
        // Console.WriteLine("Count is " + this.Count);
        for (var index = Math.Pow(2, height-1) - 1; index < this.Count && index < Math.Pow(2, height) - 1; index++)
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
        // Console.WriteLine(tree.InOrderTraversalToString());
        var traversalLocations = GetTraversalLocations();

        foreach (var traversalLocation in traversalLocations)
        {
            tree.Swap(traversalLocation);

            Console.WriteLine(tree.InOrderTraversalToString());
        }
    }

    private static Tree GetNodes()
    {
        var reverseLookup = new Dictionary<int, INode>();
        var numberOfNodes = int.Parse(Console.ReadLine());

        var tree = new Tree(numberOfNodes);
        reverseLookup.Add(1, tree[0]);

        // Count from the left child of the root (i.e., the second node)
        var treeIndex = 1;
        // Console.WriteLine("numberOfNodes = " + numberOfNodes);
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
                // Console.WriteLine("i is " + i);
                var parent = reverseLookup[i];
                // Console.WriteLine("Parent is " + parent);
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

                // Console.WriteLine("treeIndex = " + treeIndex);
                tree.Add(node);
                Console.WriteLine("Added node ({0}) to parent ({1})", node.Index, parent.Index);
                reverseLookup[index] = node;

                if (j == 0)
                {
                    reverseLookup[i].Left = node;
                }
                else
                {
                    reverseLookup[i].Right = node;
                }

                // Move to the next node
                treeIndex++;
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
