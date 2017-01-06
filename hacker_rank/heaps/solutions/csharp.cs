using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heaps
{
    public static class Extensions
    {
        public static string Join<T>(this IEnumerable<T> list, string middle = "")
        {
            var sb = new StringBuilder();

            sb.Append(list.FirstOrDefault().ToString());

            foreach (var item in list.Skip(1))
            {
                sb.Append(middle);
                sb.Append(item.ToString());
            }

            return sb.ToString();
        }
    }

    public interface IHeap
    {
        int Count { get; }

        void Add(int number);

        IEnumerable<int> SortedNumbers();

        int Peek();

        void Delete();
    }

    public abstract class Heap : IHeap, IEnumerable<int>
    {
        protected List<int> Storage { get; private set; }

        public int Count { get { return Storage.Count; } }

        public Heap()
        {
            Storage = new List<int>();
        }

        public Heap(IEnumerable<int> numbers) : this()
        {
            foreach(var number in numbers)
            {
                Add(number);
            }
        }

        public void Add(int number)
        {
            Storage.Add(number);
            // Console.WriteLine($"Storage has {Storage.Count} elements");

            var index = Storage.Count - 1;
            while (true)
            {
                if (index == 0)
                {
                    break;
                }

                var parentIndex = (Storage.Count % 2 == 0) ?
                    (index - 2) / 2 :
                    (index - 1) / 2;

                if (ShouldAdd(index, parentIndex))
                // if (Storage[index] <= Storage[parentIndex])
                {
                    break;
                }

                // Swap the elements
                var swap = Storage[parentIndex];
                Storage[parentIndex] = Storage[index];
                Storage[index] = swap;

                // Next iteration
                index = parentIndex;
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return Storage.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerable<int> SortedNumbers()
        {
            var heap = MakeHeap(Storage);
            while (heap.Count > 0)
            {
                yield return heap.Peek();

                heap.Delete();
            }
        }

        public int Peek()
        {
            return Storage[0];
        }

        public void Delete()
        {
            if (Storage.Count == 0)
            {
                return;
            }

            if (Storage.Count == 1)
            {
                Storage.RemoveAt(0);
                return;
            }

            var lastIndex = Storage.Count - 1;
            Storage[0] = Storage[lastIndex];
            Storage.RemoveAt(lastIndex);

            Heapify(0);
        }

        protected abstract Heap MakeHeap(IEnumerable<int> numbers);

        protected abstract bool ShouldAdd(int childIndex, int parentIndex);

        protected abstract bool ShouldRearrange(int childIndex, int parentIndex);

        private void Heapify(int startIndex)
        {
            var index = startIndex;
            while (true)
            {
                if (index >= Storage.Count)
                {
                    break;
                }

                var leftIndex = (index * 2) + 1;
                var rightIndex = (index * 2) + 2;
                if (leftIndex < Storage.Count && ShouldRearrange(leftIndex, index))
                {
                    if (rightIndex < Storage.Count && ShouldRearrange(rightIndex, leftIndex))
                    {
                        var swap = Storage[rightIndex];
                        Storage[rightIndex] = Storage[index];
                        Storage[index] = swap;

                        index = rightIndex;
                    }
                    else
                    {
                        var swap = Storage[leftIndex];
                        Storage[leftIndex] = Storage[index];
                        Storage[index] = swap;

                        index = leftIndex;
                    }
                }
                else if (rightIndex < Storage.Count && ShouldRearrange(rightIndex, index))
                {
                    var swap = Storage[rightIndex];
                    Storage[rightIndex] = Storage[index];
                    Storage[index] = swap;

                    index = rightIndex;
                }
                else
                {
                    // Nothing was swapped
                    break;
                }
            }
        }

        public override string ToString()
        {
            // Console.WriteLine($"printing tostring and has {Storage.Count} elements");
            return Storage.Join(" ");
        }
    }

    public class MaxHeap : Heap
    {
        public MaxHeap() : base()
        {
        }

        public MaxHeap(IEnumerable<int> numbers) : base(numbers)
        {
        }

        protected override Heap MakeHeap(IEnumerable<int> numbers)
        {
            return new MaxHeap(numbers);
        }

        protected override bool ShouldAdd(int childIndex, int parentIndex)
        {
            return Storage[childIndex] <= Storage[parentIndex];
        }

        protected override bool ShouldRearrange(int childIndex, int parentIndex)
        {
            return Storage[childIndex] > Storage[parentIndex];
        }
    }

    public class MinHeap : Heap
    {
        public MinHeap() : base()
        {
        }

        public MinHeap(IEnumerable<int> numbers) : base(numbers)
        {
        }

        protected override Heap MakeHeap(IEnumerable<int> numbers)
        {
            return new MinHeap(numbers);
        }

        protected override bool ShouldAdd(int childIndex, int parentIndex)
        {
            return Storage[childIndex] >= Storage[parentIndex];
        }

        protected override bool ShouldRearrange(int childIndex, int parentIndex)
        {
            return Storage[childIndex] < Storage[parentIndex];
        }
    }

    public class MedianHeap : IHeap
    {
        private MinHeap MinHeap = null;
        private MaxHeap MaxHeap = null;

        public int Count { get { return MaxHeap.Count + MinHeap.Count; } }

        public MedianHeap()
        {
            MinHeap = new MinHeap();
            MaxHeap = new MaxHeap();
        }

        public MedianHeap(IEnumerable<int> numbers)
        {
            foreach (var number in numbers)
            {
                Add(number);
            }
        }

        public void Add(int number)
        {
            // Console.WriteLine(number);

            if (MinHeap.Count > 0 && number >= MinHeap.Peek())
            {
                MinHeap.Add(number);

                if (MinHeap.Count >= MaxHeap.Count + 2)
                {
                    var median = MinHeap.Peek();
                    MinHeap.Delete();

                    MaxHeap.Add(median);
                }
            }
            else
            {
                MaxHeap.Add(number);

                if (MaxHeap.Count >= MinHeap.Count + 2)
                {
                    var median = MaxHeap.Peek();
                    MaxHeap.Delete();

                    MinHeap.Add(median);
                }
            }
        }

        public IEnumerable<int> SortedNumbers()
        {
            var firstHalf = MaxHeap.SortedNumbers().Reverse();
            var secondHalf = MinHeap.SortedNumbers();

            return firstHalf.Concat(secondHalf);
        }

        public int Peek()
        {
            return MinHeap.Peek();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public string Median()
        {
            double median;

            if (MinHeap.Count == 0 && MaxHeap.Count == 0)
            {
                return "0.0";
            }

            if (MinHeap.Count > MaxHeap.Count)
            {
                median = (double)MinHeap.Peek();
            }
            else if (MinHeap.Count < MaxHeap.Count)
            {
                median = (double)MaxHeap.Peek();
            }
            else
            {
                // Same number of elements
                median = (MinHeap.Peek() + MaxHeap.Peek()) / 2.0;
            }

            return median.ToString("F1");
        }
    }

    public static class Program
    {
        public static void Main(string[] argv)
        {
            var heap = new MedianHeap();
            ReadNumbers(number => {
                    heap.Add(number);
                    Console.WriteLine($"Add: {number}");
                    var sortedNumbers = heap.SortedNumbers().Join(" ");
                    Console.WriteLine($"Heap: {sortedNumbers}");
                    Console.WriteLine($"Median: heap.Median()");
                    });
        }

        private static void ReadNumbers(Action<int> func)
        {
            var count = int.Parse(Console.ReadLine());
            for (var i = 0; i < count; i++)
            {
                var input = Console.ReadLine();
                if (input == null)
                {
                    break;
                }

                func(int.Parse(input));
            }
        }
    }
}
