using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RangeSearcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var values = Console
                .ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .OrderBy(val => val)
                .ToList();

            var target = int.Parse(Console.ReadLine());

            var startAndEnd = new Searcher(values).FindStartAndEnd(target);

            Console.WriteLine($"[{startAndEnd.Item1}, {startAndEnd.Item2}]");
        }
    }
}
