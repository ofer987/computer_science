using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MergeSortedLists.Extensions;

namespace MergeSortedLists
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var lists = ReadLists().ToList();
            var answer = new Lists(lists).Merge().Join(" ");

            Console.WriteLine(answer);
        }

        private static IEnumerable<List<int>> ReadLists()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == null)
                {
                    break;
                }

                yield return input
                    .Split(' ')
                    .Select(int.Parse)
                    .ToList();
            }
        }
    }
}
