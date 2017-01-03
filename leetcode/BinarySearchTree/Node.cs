namespace BinarySearchTree
{
    public class Node : INode
    {
        private Tree Tree;
        private int Index;

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
            Index = index;
            Number = number;
            Left = left;
            Right = right
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
                Remove();

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

        private bool Remove()
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

                Tree[parent.

                return true;
            }

            return false;
        }
    }
}
