using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KnapSack.Extensions;

namespace KnapSack
{
    public class SpentCoins : ICoins
    {
        public static int Compare(ICoins first, ICoins second)
        {
            if (first.Sum > second.Sum)
            {
                return 1;
            }
            else if (first.Sum < second.Sum)
            {
                return -1;
            }

            if (first.Count < second.Count)
            {
                return 1;
            }
            else if (first.Count > second.Count)
            {
                return -1;
            }

            return 0;
        }

        public bool IsSuccess { get; private set; }

        public IEnumerable<int> Coins { get; private set; }

        public int Sum { get; private set; }

        public int Count { get; private set; }

        public SpentCoins(bool isSuccess, IEnumerable<int> coins)
        {
            IsSuccess = isSuccess;
            Coins = coins;
            Sum = GetSum(coins);
            Count = Coins.Count();
        }

        public override string ToString()
        {
            return Coins.Join(" ");
        }

        private int GetSum(IEnumerable<int> coins)
        {
            try
            {
                return Coins.Aggregate((sum, coin) => sum += coin);
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }
    }
}
