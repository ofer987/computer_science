namespace BinarySearchTree.Null
{
    public class Node : INode
    {
        public int Number { get { throw new NotImplementedException(); } }

        public INode Parent { get; set; }
        public INode Left { get { return new Null.Node(); } }
        public INode Right { get { return new Null.Node(); } }

        public bool IsEmpty { get { return true; } }
        public bool IsLeaf { get { throw new NotImplementedException(); } }

        public INode Search(int number)
        {
            return this;
        }

        public bool Delete(int number)
        {
            return false;
        }

        public bool Rotate(int number)
        {
            return false;
        }

        public override string ToString()
        {
            return "Empty";
        }
    }
}
