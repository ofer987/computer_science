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

