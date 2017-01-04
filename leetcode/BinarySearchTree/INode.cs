namespace BinarySearchTree
{
    public interface INode
    {
        int Number { get; }

        INode Left { get; }
        INode Right { get; }

        INode Parent { get; set; }
        bool IsEmpty { get; }
        bool IsLeaf { get; }

        INode Search(int number);

        bool Insert(int number);

        bool Delete(int number);

        bool Rotate();
    }
}
