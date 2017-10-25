using System;
using System.Linq;
using System.Collections.Generic;

namespace SutTheTree
{
    public class Node
    {
        public int Value { get; private set; }

        public int Sum { get; private set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node(int val)
        {
            Value = val;
        }

        public void AddChild(Node child)
        {
            if (Left == null)
            {
                Left = child;
            }
            else
            {
                Right = child;
            }
        }

        public void CalculateSums()
        {
            var result = 0;
            if (Left != null)
            {
                Left.CalculateSums();
                result += Left.Sum;
            }
            if (Right != null)
            {
                Right.CalculateSums();
                result += Right.Sum;
            }

            Sum = result + Value;
        }
    }

    public static class Solution
    {
        public static void Main(string[] argv)
        {
            ReadCount();
            var values = ReadValues().ToList();
            var root = ReadTree(values);

            root.CalculateSums();

            var smallestDiff = SubtreeDiffs(root, root)
                .OrderBy(diff => diff)
                .First();

            Console.WriteLine(smallestDiff);
        }

        private static int ReadCount()
        {
            return int.Parse(Console.ReadLine());
        }

        private static IEnumerable<int> ReadValues()
        {
            return Console
                .ReadLine()
                .Split(' ')
                .Select(int.Parse);
        }

        private static Node ReadTree(IList<int> values)
        {
            var nodes = new List<Node>(values.Count);
            foreach (var _ in values)
            {
                nodes.Add(null);
            }

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                var nodeIndices = line
                    .Split(' ')
                    .Select(int.Parse)
                    .ToList();

                var firstIndex = nodeIndices[0] - 1;
                var secondIndex = nodeIndices[1] - 1;

                var parentIsFirst = true;
                if (nodes[firstIndex] == null)
                {
                    nodes[firstIndex] = new Node(values[firstIndex]);
                    parentIsFirst = false;
                }

                if (nodes[secondIndex] == null)
                {
                    nodes[secondIndex] = new Node(values[secondIndex]);
                    parentIsFirst = true;
                }

                if (firstIndex == 0 || parentIsFirst)
                {
                    nodes[firstIndex].AddChild(nodes[secondIndex]);
                }
                else
                {
                    nodes[secondIndex].AddChild(nodes[firstIndex]);
                }
            }

            return nodes[0];
        }

        private static IEnumerable<int> SubtreeDiffs(Node root, Node node)
        {
            if (node != root)
            {
                var rootSum = root.Sum;
                var nodeSum = node.Sum;

                var diff = rootSum - 2 * nodeSum;
                if (diff >= 0)
                {
                    yield return diff;
                }
                else
                {
                    yield return -diff;
                }
            }

            if (node.Left != null)
            {
                foreach (var n in SubtreeDiffs(root, node.Left))
                {
                    yield return n;
                }
            }

            if (node.Right != null)
            {
                foreach (var n in SubtreeDiffs(root, node.Right))
                {
                    yield return n;
                }
            }
        }
    }
}

