using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BinarySearchTree.Null;

namespace BinarySearchTree
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var numbers = ReadLines().SelectMany(number => number);
            var tree = new Tree(numbers);

            // Do something
        }

        private IEnumerable<IEnumerable<int?>> ReadLines()
        {
            while (true)
            {
                var line = Console.ReadLine();
                if (line == null)
                {
                    yield break;
                }

                yield return ParseLine(line);
            }
        }

        private IEnumerable<int?> ParseLine(string line)
        {
            foreach (var item in line.Split(' '))
            {
                int val;
                if (int.TryParse(item, out val))
                {
                    yield return val;
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
