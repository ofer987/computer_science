using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Node
{
    public Node Left { get; set; }

    public Node Right { get; set; }

    public Int Value { get; private set; }

    public Node(int val)
    {
        Value = val;
    }
}

public static class Solution
{
    public static void Main(string[] args)
    {
    }

    private static Node LowestCommonAncestor(Node root, Node first, Node second)
    {
        if (root == null || first == null || second == null)
        {
            return null;
        }

        return Search(root, first, second).Item1;
    }

    private static Tuple<Node, int> Search(Node current, Node firstTarget, secondTarget)
    {
        var found = 0;
        if (current == firstTarget)
        {
            found++;
        }
        if (current == secondTarget)
        {
            found++;
        }

        if (found == 2)
        {
            return new Tuple<Node, int>(current, found);
        }

        if (current.Left != null)
        {
            var result == Search(current.Left, firstTarget, secondTarget);
            found += result.Item2;
        }

        if (found == 2)
        {
            return new Tuple<Node, int>(current, found);
        }
        if (current.Right != null)
        {
            var result = Search(current.Right, firstTarget, secondTarget);
            found += result.Item2;
        }
        if (found == 2)
        {
            return new Tuple<Node, int>(current, found);
        }

        return new Tuple<Node, int>(null, found);
    }
}
