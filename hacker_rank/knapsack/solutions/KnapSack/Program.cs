using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KnapSack.Extensions;

namespace KnapSack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            foreach (var testCase in ReadTestCases())
            {
                var result = testCase.Spend();


                Console.WriteLine($"Expected: {testCase.Sum}, Actual: {result.Sum}: {result.Coins.Join(" ")}");
            }
        }

        private static IEnumerable<TestCase> ReadTestCases()
        {
            var count = int.Parse(Console.ReadLine());

            for (var i = 0; i < count; i++)
            {
                var sum = Console
                    .ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .Last();
                var coins = Console
                    .ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToList();

                yield return new TestCase(coins, sum);
            }
        }
    }
}
