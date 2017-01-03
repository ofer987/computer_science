using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KnapSack.Extensions;

namespace KnapSack
{
    public class TestCase
    {
        private List<int> Coins;
        private Memory Memo = new Memory();

        public int Sum { get; private set; }

        public TestCase(IList<int> coins, int sum)
        {
            Coins = coins.OrderBy(c => c).ToList();
            Sum = sum;
        }

        public ICoins Spend()
        {
            return Spend(Sum, Coins, new List<int>());
        }

        private ICoins Spend(
                int remaining,
                IEnumerable<int> coins,
                List<int> spent)
        {
            // var coinsString = spent.Join(" ");
            // Console.WriteLine("Spent");
            // Console.WriteLine($"\t{coinsString}");

            var sum = spent.Sum();
            Memo[sum] = spent;

            // Exit conditions
            if (remaining == 0)
            {
                return new SpentCoins(SpendingTypes.All, spent);
            }

            if (remaining < 0)
            {
                return new NoCoins();
            }

            // Has remaining been memoized?
            // If so then use it!
            var memoizedSpent = Memo[remaining];
            if (memoizedSpent != null)
            {
                var totalSpent = new List<int>();
                totalSpent.AddRange(spent);
                totalSpent.AddRange(memoizedSpent);

                return new SpentCoins(SpendingTypes.All, totalSpent);
            }

            // Calculate the amount of coins to spend
            // for remaining
            var i = 0;
            ICoins bestResult = new NoCoins();
            foreach (var coin in coins)
            {
                Console.WriteLine($"Spending {coin}");
                var lessRemaining = remaining - coin;
                var moreSpent = new List<int>(spent);
                moreSpent.Add(coin);

                var result = Spend(lessRemaining, coins.Skip(i), moreSpent);

                if (result.Spending == SpendingTypes.All)
                {
                    bestResult = result;
                    break;
                }
                else if (result.Spending == SpendingTypes.Almost)
                {
                    bestResult = result;
                }

                i++;
            }

            // var coinsString = bestResult.Coins.Join(" ");
            // Console.WriteLine("Memoizing");
            // Console.WriteLine($"\t{bestResult.Sum}: {coinsString}");

            // the most amount of coins spent if could not properly spend coins
            // i.e., sum of spentCoins is greater than (expected) Sum
            return bestResult.IsSuccess ?
                bestResult :
                new SpentCoins(SpendingTypes.Almost, spent);
        }
    }
}
