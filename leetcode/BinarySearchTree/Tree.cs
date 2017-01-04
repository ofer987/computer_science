using System.Text;
using System.Collections.Generic;
using System.Linq;
using System;

using BinarySearchTree.Null;

namespace BinarySearchTree
{
    public class Tree
    {
        public INode Root { get; private set; }

        public Tree(IEnumerable<int?> numbers)
        {
            var nodes = GetNodes(numbers);
            Root = nodes.First();
        }

        public bool Delete(int number)
        {
            return true;
        }

        public INode Search(int number)
        {
            return Root.Search(number);
        }

        public bool Insert(int number)
        {
            return Root.Insert(number);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var action = (node) =>
            {
                sb.Append($"{node.ToString()} ");
            }

            BreadthFirstSearch(action);

            return sb.ToString();
        }

        public void BreadthFirstSearch(Action<Inode> action)
        {
            var level = new Queue<INode>();
            level.Enqueue(Root);

            while (true)
            {
                var nextLevel = new Queue<INode>();

                if (level.All(node => node.IsIsEmpty))
                {
                    break;
                }

                foreach (var node in level)
                {
                    action(node);
                    sb.Append($"{node.ToString()} ")

                    nextLevel.Enqueue(node.Left);
                    nextLevel.Enqueue(node.Right);
                }

                if (level.Empty)
                {
                    sb.AppendLine();
                    level = nextLevel;
                }
            }

            return sb.ToString();
        }

        private IList<INode> GetNodes(IEnumerable<int?> numbers)
        {
            var numbersList = (numbers as IList<int?>) ?? numbers.ToList();
            var nodes = new List<INode>(numbers.Count);

            for (var i = nodes.Count - 1; i >= 0; i -= 1)
            {
                if (!numbers[i].HasValue)
                {
                    continue;
                }

                var number = numbers[i].Value;
                var leftChildIndex = (2 * i) + 1;
                var rightChildIndex = (2 * i) + 2;

                var leftChild = leftChildIndex < nodes.Count ?
                    nodes[leftChildIndex] :
                    new Null.Node();
                var rightChild = rightChildIndex < nodes.Count ?
                    nodes[rightChildIndex] :
                    new Null.Node();

                var node = new Node(numbers[i], leftChild, rightChild);
                leftChild.Parent = node;
                rightChild.Parent = node;

                nodes[i] = node;
            }

            return nodes;
        }
    }
}

