using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KnapSack.Extensions;

namespace KnapSack
{
    public class TestCase
    {
        public int Sum { get; private set; }

        private List<int> Coins { get; set; }

        public TestCase(IList<int> coins, int sum)
        {
            Coins = coins.OrderByDescending(c => c).ToList();
            Sum = sum;
        }

        public SpentCoins Spend()
        {
            return Spend(Sum, Coins, new List<int>());
        }

        private SpentCoins Spend(
                int remaining,
                IEnumerable<int> coins,
                List<int> spent)
        {
            // Exit conditions
            if (remaining == 0)
            {
                return new SpentCoins(true, spent);
            }

            if (remaining < 0)
            {
                return new SpentCoins(false, new List<int>());
            }

            // Remainign coins
            var i = 0;
            foreach (var coin in coins)
            {
                var lessRemaining = remaining - coin;
                var moreSpent = new List<int>(spent);
                moreSpent.Add(coin);

                var result = Spend(lessRemaining, coins.Skip(i), moreSpent);

                if (result.IsSuccess)
                {
                    return result;
                }

                i++;
            }

            // No coins have been left
            return new SpentCoins(true, spent);
        }
    }
}
