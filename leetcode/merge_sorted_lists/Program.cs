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
            var count = int.Parse(Console.ReadLine());

            for (var i = 0; i < count; i++)
            {
                yield return Console
                    .ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToList();
            }
        }
    }
}
