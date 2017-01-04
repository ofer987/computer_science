using System.Text;
using System.Collections.Generic;
using System.Linq;
using System;

using BinarySearchTree.Null;

namespace BinarySearchTree
{
    public class Node : INode, IDisposable
    {
        private Tree Tree;
        private int TreeIndex;

        public int Number { get; private set; }

        public INode Parent { get; set; }

        public INode Left { get; private set; }
        public INode Right { get; private set; }

        public bool IsEmpty { get { return false; } }
        public bool IsLeaf
        {
            get
            {
                return Left.IsEmpty && Right.IsEmpty;
            }
        }

        public Node(Tree tree, int index, int number, INode left, INode right)
        {
            Tree = tree;
            TreeIndex = index;
            Number = number;
            Left = left;
            Right = right
        }

        public Node(Tree tree, int number)
        {
            Tree = tree;
            TreeIndex = tree.Count;
            Number = number;
            Left = new Null.Node();
            Right = new Null.Node();
        }

        public INode Search(int number)
        {
            if (Number == number)
            {
                return this;
            }

            return number < Number ?
                Left.Search(number) :
                Right.Search(number);
        }

        public bool Delete(int number)
        {
            if (Number == number)
            {
                Rotate();
                Dispose();

                return true;
            }

            return number < Number ?
                Left.Delete(number) :
                Right.Delete(number);
        }

        public bool Rotate(int number)
        {
            if (Number == number)
            {
                // Action

                // Left.Left = Left.left;
                Left.Right = Right;

                Right.Left = this;
                // Right.Right = Right.Right;

                this.Left = Left.Right;
                this.Right = Right.Left;

                // Keep rotating till a Null.Node is reached
                this.Rotate(number);

                return true;
            }

            return number < Number ?
                Left.Rotate(number) :
                Right.Rotate(number);
        }

        public bool Insert(int number)
        {
            if (number == Number)
            {
                return false;
            }

            if (number < Number)
            {
                if (Left.IsEmpty)
                {
                    Left = new Node(Tree, number);
                    Left.Parent = this;
                    Tree.Add(Left);

                    return true;
                }

                return Left.Insert();
            }
            else
            {
                if (Right.IsEmpty)
                {
                    Right = new Node(Tree, number);
                    Right.Parent = this;
                    Tree.Add(Right);

                    return true;
                }

                return Right.Insert();
            }
        }

        public override string ToString()
        {
            return Number.ToString();
        }

        private override void Dispose()
        {
            if (isLeaf)
            {
                var parent = Parent;
                if (Index % 2 == 0)
                {
                    parent.Right = new Null.Node();
                }
                else
                {
                    parent.Left == new Null.Node();
                }

                // Remove from the tree
                // So that all reference to `this` are removed
                // And therefore this object can be collected
                // by the Garbage Collector
                Tree[OriginalIndex] = null;

                return true;
            }

            return false;
        }
    }
}
