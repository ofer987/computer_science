using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchOrInsert
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

            var searcher = new Searcher(values);
            searcher.FindTarget(target);

            foreach (var item in searcher.Sorted)
            {
                Console.WriteLine($"{item}");
            }
        }
    }
}
